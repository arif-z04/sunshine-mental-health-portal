using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface IAuditService
{
    Task LogAsync(int? actorId, string? actorEmail, string action, string? targetType, string? targetId, string? details, string? ipAddress);
    Task<List<AuditLogDto>> GetRecentLogsAsync(int limit = 100);
}

public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _db;

    public AuditService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task LogAsync(int? actorId, string? actorEmail, string action, string? targetType, string? targetId, string? details, string? ipAddress)
    {
        try
        {
            var log = new AuditLog
            {
                ActorId = actorId,
                ActorEmail = actorEmail,
                Action = action,
                TargetType = targetType,
                TargetId = targetId,
                Details = details,
                IpAddress = ipAddress,
                CreatedAt = DateTime.UtcNow
            };

            _db.AuditLogs.Add(log);
            await _db.SaveChangesAsync();
        }
        catch
        {
            // Fail-safe: Audit logging should not crash business transactions
        }
    }

    public async Task<List<AuditLogDto>> GetRecentLogsAsync(int limit = 100)
    {
        return await _db.AuditLogs
            .OrderByDescending(a => a.CreatedAt)
            .Take(limit)
            .Select(a => new AuditLogDto(
                a.Id,
                a.ActorId,
                a.ActorEmail,
                a.Action,
                a.TargetType,
                a.TargetId,
                a.Details,
                a.IpAddress,
                a.CreatedAt
            ))
            .ToListAsync();
    }
}
