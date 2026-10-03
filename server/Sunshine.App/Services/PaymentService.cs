using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface IPaymentService
{
    Task<PaymentResultDto> ProcessPaymentAsync(int userId, ProcessPaymentRequest request);
    Task<List<PaymentDto>> GetUserPaymentsAsync(int userId);
    Task<List<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentResultDto> RefundPaymentAsync(int paymentId);
}

public class PaymentService : IPaymentService
{
    private readonly ApplicationDbContext _db;
    private readonly IPaymentGatewayService _gateway;
    private readonly INotificationService _notifications;

    public PaymentService(ApplicationDbContext db, IPaymentGatewayService gateway, INotificationService notifications)
    {
        _db = db;
        _gateway = gateway;
        _notifications = notifications;
    }

    public async Task<PaymentResultDto> ProcessPaymentAsync(int userId, ProcessPaymentRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        var pType = request.PaymentType.ToUpperInvariant();
        var pMethod = string.IsNullOrWhiteSpace(request.PaymentMethod) ? PaymentMethods.Bkash : request.PaymentMethod.ToUpperInvariant();

        if (pType == PaymentTypes.Appointment)
        {
            if (!request.AppointmentId.HasValue)
            {
                throw new ArgumentException("AppointmentId is required for appointment payments.");
            }

            var appointment = await _db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .FirstOrDefaultAsync(a => a.Id == request.AppointmentId.Value);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            if (appointment.Patient.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only pay for your own appointments.");
            }

            var amount = appointment.Doctor.ConsultationFee;
            var chargeResult = await _gateway.ChargeAsync(new PaymentChargeRequest(
                amount,
                "BDT",
                pMethod,
                request.AccountNumberOrCard,
                request.AccountHolderName,
                request.OtpOrPin,
                $"Session with {appointment.Doctor.User.FullName}"
            ));

            var payment = new Payment
            {
                UserId = userId,
                Amount = amount,
                Currency = "BDT",
                PaymentType = PaymentTypes.Appointment,
                AppointmentId = appointment.Id,
                PaymentMethod = pMethod,
                TransactionId = chargeResult.TransactionId,
                Status = chargeResult.Success ? PaymentStatuses.Success : PaymentStatuses.Failed,
                PaymentDate = chargeResult.Success ? DateTime.UtcNow : null,
                CreatedAt = DateTime.UtcNow
            };

            _db.Payments.Add(payment);

            if (chargeResult.Success)
            {
                // Advance payment confirmed
                if (appointment.Status == AppointmentStatuses.Pending)
                {
                    appointment.Status = AppointmentStatuses.Confirmed;
                    appointment.BookingExpiresAt = null; // Clear expiration
                    appointment.UpdatedAt = DateTime.UtcNow;
                }

                await _notifications.CreateNotificationAsync(
                    userId,
                    "Payment Successful",
                    $"Your payment of ৳{amount:N0} BDT via {pMethod} for your session with {appointment.Doctor.User.FullName} was successful. Transaction ID: {chargeResult.TransactionId}",
                    "PAYMENT"
                );

                await _notifications.CreateNotificationAsync(
                    appointment.Doctor.UserId,
                    "Payment Received",
                    $"Payment of ৳{amount:N0} BDT received from {user.FullName} for appointment on {appointment.AppointmentDate:yyyy-MM-dd}.",
                    "PAYMENT"
                );
            }
            else
            {
                await _notifications.CreateNotificationAsync(
                    userId,
                    "Payment Failed",
                    $"Payment of ৳{amount:N0} BDT failed: {chargeResult.Message}",
                    "PAYMENT"
                );
            }

            await _db.SaveChangesAsync();

            return new PaymentResultDto(
                chargeResult.Success,
                chargeResult.Message,
                chargeResult.TransactionId,
                MapToDto(payment, user)
            );
        }
        else if (pType == PaymentTypes.Subscription)
        {
            if (!request.PlanId.HasValue)
            {
                throw new ArgumentException("PlanId is required for subscription payments.");
            }

            var plan = await _db.SubscriptionPlans.FirstOrDefaultAsync(p => p.Id == request.PlanId.Value && p.IsActive);
            if (plan == null)
            {
                throw new KeyNotFoundException("Subscription plan not found or inactive.");
            }

            var amount = plan.Price;
            var chargeResult = await _gateway.ChargeAsync(new PaymentChargeRequest(
                amount,
                "BDT",
                pMethod,
                request.AccountNumberOrCard,
                request.AccountHolderName,
                request.OtpOrPin,
                $"Membership Plan: {plan.Name}"
            ));

            var payment = new Payment
            {
                UserId = userId,
                Amount = amount,
                Currency = "BDT",
                PaymentType = PaymentTypes.Subscription,
                PaymentMethod = pMethod,
                TransactionId = chargeResult.TransactionId,
                Status = chargeResult.Success ? PaymentStatuses.Success : PaymentStatuses.Failed,
                PaymentDate = chargeResult.Success ? DateTime.UtcNow : null,
                CreatedAt = DateTime.UtcNow
            };

            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            if (chargeResult.Success)
            {
                var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
                var endDate = startDate.AddDays(plan.DurationDays);

                var subscription = new Subscription
                {
                    UserId = userId,
                    PlanId = plan.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = SubscriptionStatuses.Active,
                    PaymentId = payment.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _db.Subscriptions.Add(subscription);
                await _db.SaveChangesAsync();

                payment.SubscriptionId = subscription.Id;
                await _db.SaveChangesAsync();

                await _notifications.CreateNotificationAsync(
                    userId,
                    "Subscription Activated!",
                    $"Welcome to '{plan.Name}'. You now have unlimited access to our digital self-help library until {endDate:yyyy-MM-dd}.",
                    "SUBSCRIPTION"
                );
            }

            return new PaymentResultDto(
                chargeResult.Success,
                chargeResult.Message,
                chargeResult.TransactionId,
                MapToDto(payment, user)
            );
        }
        else
        {
            throw new ArgumentException($"Invalid payment type '{request.PaymentType}'.");
        }
    }

    public async Task<List<PaymentDto>> GetUserPaymentsAsync(int userId)
    {
        return await _db.Payments
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => MapToDto(p, p.User))
            .ToListAsync();
    }

    public async Task<List<PaymentDto>> GetAllPaymentsAsync()
    {
        return await _db.Payments
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => MapToDto(p, p.User))
            .ToListAsync();
    }

    public async Task<PaymentResultDto> RefundPaymentAsync(int paymentId)
    {
        var payment = await _db.Payments.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == paymentId);
        if (payment == null)
        {
            throw new KeyNotFoundException("Payment not found.");
        }

        if (payment.Status != PaymentStatuses.Success || string.IsNullOrEmpty(payment.TransactionId))
        {
            throw new InvalidOperationException("Only completed successful transactions can be refunded.");
        }

        var refundResult = await _gateway.RefundAsync(payment.TransactionId, payment.Amount);
        if (refundResult.Success)
        {
            payment.Status = PaymentStatuses.Refunded;
            await _db.SaveChangesAsync();

            await _notifications.CreateNotificationAsync(
                payment.UserId,
                "Payment Refunded",
                $"Your payment of ৳{payment.Amount:N0} BDT for transaction {payment.TransactionId} has been refunded.",
                "PAYMENT"
            );
        }

        return new PaymentResultDto(refundResult.Success, refundResult.Message, refundResult.TransactionId, MapToDto(payment, payment.User));
    }

    private static PaymentDto MapToDto(Payment p, ApplicationUser u)
    {
        return new PaymentDto(
            p.Id,
            p.UserId,
            u?.Email ?? "",
            u?.FullName ?? "Unknown",
            p.Amount,
            p.Currency,
            p.PaymentType,
            p.AppointmentId,
            p.SubscriptionId,
            p.PaymentMethod,
            p.TransactionId,
            p.Status,
            p.PaymentDate,
            p.CreatedAt
        );
    }
}
