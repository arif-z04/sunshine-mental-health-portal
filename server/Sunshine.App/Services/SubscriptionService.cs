using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface ISubscriptionService
{
    Task<List<SubscriptionPlanDto>> GetPlansAsync();
    Task<List<SubscriptionDto>> GetUserSubscriptionsAsync(int userId);
    Task<SubscriptionDto?> GetUserActiveSubscriptionAsync(int userId);
    Task<bool> HasActiveSubscriptionAsync(int userId);
    Task<bool> CancelSubscriptionAsync(int userId, int subscriptionId);
    Task<SubscriptionPlanDto> CreateOrUpdatePlanAsync(SubscriptionPlanDto dto);
}

public class SubscriptionService : ISubscriptionService
{
    private readonly ApplicationDbContext _db;
    private readonly INotificationService _notifications;

    public SubscriptionService(ApplicationDbContext db, INotificationService notifications)
    {
        _db = db;
        _notifications = notifications;
    }

    public async Task<List<SubscriptionPlanDto>> GetPlansAsync()
    {
        return await _db.SubscriptionPlans
            .Where(p => p.IsActive)
            .OrderBy(p => p.Price)
            .Select(p => new SubscriptionPlanDto(
                p.Id,
                p.Name,
                p.DurationType,
                p.DurationDays,
                p.Price,
                p.Description,
                p.IsActive
            ))
            .ToListAsync();
    }

    public async Task<List<SubscriptionDto>> GetUserSubscriptionsAsync(int userId)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var list = await _db.Subscriptions
            .Include(s => s.User)
            .Include(s => s.Plan)
            .Include(s => s.Payment)
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();

        return list.Select(s => MapToDto(s, today)).ToList();
    }

    public async Task<SubscriptionDto?> GetUserActiveSubscriptionAsync(int userId)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var sub = await _db.Subscriptions
            .Include(s => s.User)
            .Include(s => s.Plan)
            .Include(s => s.Payment)
            .Where(s => s.UserId == userId && s.Status == SubscriptionStatuses.Active && s.StartDate <= today && today <= s.EndDate)
            .OrderByDescending(s => s.EndDate)
            .FirstOrDefaultAsync();

        return sub != null ? MapToDto(sub, today) : null;
    }

    public async Task<bool> HasActiveSubscriptionAsync(int userId)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return await _db.Subscriptions.AnyAsync(s =>
            s.UserId == userId &&
            s.Status == SubscriptionStatuses.Active &&
            s.StartDate <= today &&
            today <= s.EndDate
        );
    }

    public async Task<bool> CancelSubscriptionAsync(int userId, int subscriptionId)
    {
        var sub = await _db.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionId && s.UserId == userId);
        if (sub == null) return false;

        sub.Status = SubscriptionStatuses.Cancelled;
        sub.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        await _notifications.CreateNotificationAsync(
            userId,
            "Subscription Cancelled",
            "Your subscription has been cancelled. Access will remain valid until the end of your prepaid period.",
            "SUBSCRIPTION"
        );

        return true;
    }

    public async Task<SubscriptionPlanDto> CreateOrUpdatePlanAsync(SubscriptionPlanDto dto)
    {
        SubscriptionPlan plan;
        if (dto.Id > 0)
        {
            plan = await _db.SubscriptionPlans.FirstOrDefaultAsync(p => p.Id == dto.Id)
                ?? throw new KeyNotFoundException("Subscription plan not found.");

            plan.Name = dto.Name.Trim();
            plan.DurationType = dto.DurationType.ToUpperInvariant();
            plan.DurationDays = dto.DurationDays;
            plan.Price = dto.Price;
            plan.Description = dto.Description?.Trim();
            plan.IsActive = dto.IsActive;
        }
        else
        {
            plan = new SubscriptionPlan
            {
                Name = dto.Name.Trim(),
                DurationType = dto.DurationType.ToUpperInvariant(),
                DurationDays = dto.DurationDays,
                Price = dto.Price,
                Description = dto.Description?.Trim(),
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow
            };
            _db.SubscriptionPlans.Add(plan);
        }

        await _db.SaveChangesAsync();

        return new SubscriptionPlanDto(
            plan.Id,
            plan.Name,
            plan.DurationType,
            plan.DurationDays,
            plan.Price,
            plan.Description,
            plan.IsActive
        );
    }

    private static SubscriptionDto MapToDto(Subscription s, DateOnly today)
    {
        var isValid = s.Status == SubscriptionStatuses.Active && s.StartDate <= today && today <= s.EndDate;

        return new SubscriptionDto(
            s.Id,
            s.UserId,
            s.User?.Email ?? "",
            s.User?.FullName ?? "",
            s.PlanId,
            s.Plan?.Name ?? "",
            s.Plan?.DurationType ?? "",
            s.Plan?.Price ?? 0,
            s.StartDate,
            s.EndDate,
            s.Status,
            isValid,
            s.PaymentId,
            s.Payment?.TransactionId,
            s.CreatedAt
        );
    }
}
