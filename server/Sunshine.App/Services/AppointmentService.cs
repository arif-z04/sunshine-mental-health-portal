using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface IAppointmentService
{
    Task<List<AvailableSlotDto>> GetAvailableSlotsAsync(int doctorId, DateOnly date);
    Task<AppointmentDto> BookAppointmentAsync(int patientUserId, BookAppointmentRequest request);
    Task<List<AppointmentDto>> GetPatientAppointmentsAsync(int patientUserId);
    Task<List<AppointmentDto>> GetDoctorAppointmentsAsync(int doctorUserId);
    Task<List<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto> UpdateAppointmentStatusAsync(int userId, string role, int appointmentId, UpdateAppointmentStatusRequest request);
    Task<AppointmentDto> GetAppointmentByIdAsync(int userId, string role, int appointmentId);
    Task<AppointmentDto> RescheduleAppointmentAsync(int userId, string role, int appointmentId, RescheduleAppointmentRequest request);
    Task<AppointmentDto> CancelAppointmentAsync(int userId, string role, int appointmentId);
    Task ReleaseExpiredPendingAppointmentsAsync();
}

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _db;
    private readonly INotificationService _notifications;

    public AppointmentService(ApplicationDbContext db, INotificationService notifications)
    {
        _db = db;
        _notifications = notifications;
    }

    public async Task ReleaseExpiredPendingAppointmentsAsync()
    {
        var now = DateTime.UtcNow;
        var expiredAppointments = await _db.Appointments
            .Where(a => a.Status == AppointmentStatuses.Pending && a.BookingExpiresAt.HasValue && a.BookingExpiresAt.Value < now)
            .ToListAsync();

        foreach (var apt in expiredAppointments)
        {
            apt.Status = AppointmentStatuses.Cancelled;
            apt.Notes = string.IsNullOrWhiteSpace(apt.Notes) 
                ? "Auto-cancelled: Advance payment was not completed within the 15-minute window."
                : apt.Notes + " [Auto-cancelled: 15-min payment window expired]";
            apt.UpdatedAt = now;
        }

        if (expiredAppointments.Count > 0)
        {
            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<AvailableSlotDto>> GetAvailableSlotsAsync(int doctorId, DateOnly date)
    {
        // First release any pending appointments that timed out
        await ReleaseExpiredPendingAppointmentsAsync();

        var doctor = await _db.Doctors
            .Include(d => d.Schedules)
            .FirstOrDefaultAsync(d => d.Id == doctorId);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }

        var dayOfWeek = (int)date.DayOfWeek;
        var schedule = doctor.Schedules.FirstOrDefault(s => s.DayOfWeek == dayOfWeek && s.IsActive);

        if (schedule == null)
        {
            return new List<AvailableSlotDto>();
        }

        var bookedStarts = await _db.Appointments
            .Where(a => a.DoctorId == doctorId && a.AppointmentDate == date && a.Status != AppointmentStatuses.Cancelled)
            .Select(a => a.StartTime)
            .ToListAsync();

        var slots = new List<AvailableSlotDto>();
        var duration = TimeSpan.FromMinutes(schedule.SlotDurationMinutes);
        var current = schedule.StartTime.ToTimeSpan();
        var end = schedule.EndTime.ToTimeSpan();

        while (current + duration <= end)
        {
            var slotStart = TimeOnly.FromTimeSpan(current);
            var slotEnd = TimeOnly.FromTimeSpan(current + duration);

            var isBooked = bookedStarts.Any(b => b == slotStart);

            slots.Add(new AvailableSlotDto(
                date,
                slotStart,
                slotEnd,
                isBooked
            ));

            current += duration;
        }

        return slots;
    }

    public async Task<AppointmentDto> BookAppointmentAsync(int patientUserId, BookAppointmentRequest request)
    {
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? transaction = null;
        if (_db.Database.IsRelational())
        {
            transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }

        try
        {
            await ReleaseExpiredPendingAppointmentsAsync();

            var patient = await _db.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == patientUserId);

            if (patient == null)
            {
                throw new InvalidOperationException("Patient profile not found.");
            }

            var doctor = await _db.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == request.DoctorId);

            if (doctor == null || !doctor.IsAvailable)
            {
                throw new InvalidOperationException("Selected therapist is not currently available.");
            }

            // Check if slot is already occupied
            var conflict = await _db.Appointments.AnyAsync(a =>
                a.DoctorId == request.DoctorId &&
                a.AppointmentDate == request.AppointmentDate &&
                a.StartTime == request.StartTime &&
                a.Status != AppointmentStatuses.Cancelled
            );

            if (conflict)
            {
                throw new InvalidOperationException("This time slot has already been booked. Please select a different time.");
            }

            var isAdvance = doctor.PaymentPolicy == PaymentPolicies.Advance;
            var initialStatus = isAdvance ? AppointmentStatuses.Pending : AppointmentStatuses.Confirmed;
            var expiresAt = isAdvance ? DateTime.UtcNow.AddMinutes(15) : (DateTime?)null;

            var endTime = request.EndTime > request.StartTime ? request.EndTime : request.StartTime.AddMinutes(45);

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                AppointmentDate = request.AppointmentDate,
                StartTime = request.StartTime,
                EndTime = endTime,
                Status = initialStatus,
                Reason = request.Reason?.Trim(),
                BookingExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();

            if (transaction != null)
            {
                await transaction.CommitAsync();
            }

            // Send In-app Notifications
            if (isAdvance)
            {
                await _notifications.CreateNotificationAsync(
                    patient.UserId,
                    "Action Required: Complete Advance Payment",
                    $"Your session with {doctor.User.FullName} for {request.AppointmentDate:yyyy-MM-dd} at {request.StartTime:HH:mm} is held for 15 minutes. Please complete payment to confirm.",
                    "APPOINTMENT"
                );
            }
            else
            {
                await _notifications.CreateNotificationAsync(
                    patient.UserId,
                    "Appointment Confirmed",
                    $"Your session with {doctor.User.FullName} on {request.AppointmentDate:yyyy-MM-dd} at {request.StartTime:HH:mm} has been confirmed.",
                    "APPOINTMENT"
                );

                await _notifications.CreateNotificationAsync(
                    doctor.UserId,
                    "New Appointment Booked",
                    $"Patient {patient.User.FullName} has scheduled a session for {request.AppointmentDate:yyyy-MM-dd} at {request.StartTime:HH:mm}.",
                    "APPOINTMENT"
                );
            }

            return MapToDto(appointment, patient, doctor);
        }
        catch
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            throw;
        }
        finally
        {
            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }
        }
    }

    public async Task<List<AppointmentDto>> GetPatientAppointmentsAsync(int patientUserId)
    {
        await ReleaseExpiredPendingAppointmentsAsync();

        var patient = await _db.Patients.FirstOrDefaultAsync(p => p.UserId == patientUserId);
        if (patient == null) return new List<AppointmentDto>();

        return await _db.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Payments)
            .Where(a => a.PatientId == patient.Id)
            .OrderByDescending(a => a.AppointmentDate)
            .ThenByDescending(a => a.StartTime)
            .Select(a => MapToDto(a, a.Patient, a.Doctor))
            .ToListAsync();
    }

    public async Task<List<AppointmentDto>> GetDoctorAppointmentsAsync(int doctorUserId)
    {
        await ReleaseExpiredPendingAppointmentsAsync();

        var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.UserId == doctorUserId);
        if (doctor == null) return new List<AppointmentDto>();

        return await _db.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Payments)
            .Where(a => a.DoctorId == doctor.Id)
            .OrderBy(a => a.AppointmentDate)
            .ThenBy(a => a.StartTime)
            .Select(a => MapToDto(a, a.Patient, a.Doctor))
            .ToListAsync();
    }

    public async Task<List<AppointmentDto>> GetAllAppointmentsAsync()
    {
        await ReleaseExpiredPendingAppointmentsAsync();

        return await _db.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Payments)
            .OrderByDescending(a => a.AppointmentDate)
            .ThenByDescending(a => a.StartTime)
            .Select(a => MapToDto(a, a.Patient, a.Doctor))
            .ToListAsync();
    }

    public async Task<AppointmentDto> UpdateAppointmentStatusAsync(int userId, string role, int appointmentId, UpdateAppointmentStatusRequest request)
    {
        var apt = await _db.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Payments)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        if (apt == null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        if (role == AppRoles.Doctor && apt.Doctor.UserId != userId)
        {
            throw new UnauthorizedAccessException("Unauthorized to modify this appointment.");
        }
        if (role == AppRoles.Patient && apt.Patient.UserId != userId)
        {
            throw new UnauthorizedAccessException("Unauthorized to modify this appointment.");
        }

        apt.Status = request.Status.ToUpperInvariant();
        if (!string.IsNullOrWhiteSpace(request.Notes))
        {
            apt.Notes = request.Notes.Trim();
        }
        apt.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        await _notifications.CreateNotificationAsync(
            apt.Patient.UserId,
            $"Appointment Status: {apt.Status}",
            $"Your appointment on {apt.AppointmentDate:yyyy-MM-dd} status was updated to {apt.Status}.",
            "APPOINTMENT"
        );

        return MapToDto(apt, apt.Patient, apt.Doctor);
    }

    public async Task<AppointmentDto> GetAppointmentByIdAsync(int userId, string role, int appointmentId)
    {
        await ReleaseExpiredPendingAppointmentsAsync();

        var apt = await _db.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Payments)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        if (apt == null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        if (role == AppRoles.Patient && apt.Patient.UserId != userId)
        {
            throw new UnauthorizedAccessException("Unauthorized to access this appointment.");
        }

        if (role == AppRoles.Doctor && apt.Doctor.UserId != userId)
        {
            throw new UnauthorizedAccessException("Unauthorized to access this appointment.");
        }

        return MapToDto(apt, apt.Patient, apt.Doctor);
    }

    public async Task<AppointmentDto> RescheduleAppointmentAsync(int userId, string role, int appointmentId, RescheduleAppointmentRequest request)
    {
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? transaction = null;
        if (_db.Database.IsRelational())
        {
            transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }

        try
        {
            await ReleaseExpiredPendingAppointmentsAsync();

            var apt = await _db.Appointments
                .Include(a => a.Patient).ThenInclude(p => p.User)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.Payments)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (apt == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            if (role == AppRoles.Patient && apt.Patient.UserId != userId)
            {
                throw new UnauthorizedAccessException("Unauthorized to reschedule this appointment.");
            }

            if (role == AppRoles.Doctor && apt.Doctor.UserId != userId)
            {
                throw new UnauthorizedAccessException("Unauthorized to reschedule this appointment.");
            }

            if (apt.Status == AppointmentStatuses.Completed)
            {
                throw new InvalidOperationException("Completed appointments cannot be rescheduled.");
            }

            if (apt.Status == AppointmentStatuses.Cancelled || apt.Status == AppointmentStatuses.Rejected)
            {
                throw new InvalidOperationException("Cancelled or rejected appointments cannot be rescheduled. Please book a new session.");
            }

            // Conflict check
            var conflict = await _db.Appointments.AnyAsync(a =>
                a.Id != appointmentId &&
                a.DoctorId == apt.DoctorId &&
                a.AppointmentDate == request.NewDate &&
                a.StartTime == request.NewStartTime &&
                a.Status != AppointmentStatuses.Cancelled &&
                a.Status != AppointmentStatuses.Rejected
            );

            if (conflict)
            {
                throw new InvalidOperationException("The requested rescheduled time slot is already booked. Please choose another time.");
            }

            var oldDate = apt.AppointmentDate;
            var oldTime = apt.StartTime;
            apt.AppointmentDate = request.NewDate;
            apt.StartTime = request.NewStartTime;
            apt.EndTime = request.NewEndTime > request.NewStartTime ? request.NewEndTime : request.NewStartTime.AddMinutes(45);
            apt.UpdatedAt = DateTime.UtcNow;

            var reasonText = string.IsNullOrWhiteSpace(request.Reason) ? "" : $" (Reason: {request.Reason.Trim()})";
            apt.Notes = string.IsNullOrWhiteSpace(apt.Notes)
                ? $"Rescheduled from {oldDate:yyyy-MM-dd} {oldTime:HH:mm}{reasonText}"
                : $"{apt.Notes} [Rescheduled from {oldDate:yyyy-MM-dd} {oldTime:HH:mm}{reasonText}]";

            await _db.SaveChangesAsync();

            if (transaction != null)
            {
                await transaction.CommitAsync();
            }

            await _notifications.CreateNotificationAsync(
                apt.Patient.UserId,
                "Appointment Rescheduled",
                $"Your appointment with {apt.Doctor.User.FullName} was rescheduled to {apt.AppointmentDate:yyyy-MM-dd} at {apt.StartTime:HH:mm}.",
                "APPOINTMENT"
            );

            await _notifications.CreateNotificationAsync(
                apt.Doctor.UserId,
                "Appointment Rescheduled",
                $"Appointment #{apt.Id} with {apt.Patient.User.FullName} was rescheduled to {apt.AppointmentDate:yyyy-MM-dd} at {apt.StartTime:HH:mm}.",
                "APPOINTMENT"
            );

            return MapToDto(apt, apt.Patient, apt.Doctor);
        }
        catch
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            throw;
        }
        finally
        {
            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }
        }
    }

    public async Task<AppointmentDto> CancelAppointmentAsync(int userId, string role, int appointmentId)
    {
        return await UpdateAppointmentStatusAsync(userId, role, appointmentId, new UpdateAppointmentStatusRequest(AppointmentStatuses.Cancelled, "Cancelled by user"));
    }

    private static AppointmentDto MapToDto(Appointment a, Patient p, Doctor d)
    {
        var latestPay = a.Payments?.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        var isExpired = a.Status == AppointmentStatuses.Pending && a.BookingExpiresAt.HasValue && a.BookingExpiresAt.Value < DateTime.UtcNow;

        return new AppointmentDto(
            a.Id,
            p.Id,
            p.User?.FullName ?? "Unknown",
            p.User?.Email ?? "",
            p.User?.PhoneNumber,
            p.EmergencyContact,
            d.Id,
            d.User?.FullName ?? "Unknown Therapist",
            d.Specialization,
            d.ConsultationFee,
            d.PaymentPolicy,
            a.AppointmentDate,
            a.StartTime,
            a.EndTime,
            a.Status,
            a.Reason,
            a.Notes,
            a.BookingExpiresAt,
            isExpired,
            latestPay?.Id,
            latestPay?.Status,
            a.CreatedAt
        );
    }
}
