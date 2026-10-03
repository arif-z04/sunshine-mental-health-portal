using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface INotificationService
{
    Task<List<Notification>> GetUserNotificationsAsync(int userId);
    Task<bool> MarkAsReadAsync(int userId, int notificationId);
    Task CreateNotificationAsync(int userId, string title, string message, string type = "SYSTEM");
}

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _db;

    public NotificationService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
    {
        return await _db.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(30)
            .ToListAsync();
    }

    public async Task<bool> MarkAsReadAsync(int userId, int notificationId)
    {
        var item = await _db.Notifications.FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        if (item == null) return false;

        item.IsRead = true;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task CreateNotificationAsync(int userId, string title, string message, string type = "SYSTEM")
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = type,
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };
        _db.Notifications.Add(notification);
        await _db.SaveChangesAsync();
    }
}
