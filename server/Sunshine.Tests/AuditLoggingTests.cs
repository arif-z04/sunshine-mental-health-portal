using System;
using System.Threading.Tasks;
using Sunshine.App.Services;
using Xunit;

namespace Sunshine.Tests;

public class AuditLoggingTests
{
    [Fact]
    public async Task LogAsync_RecordsActivity_AndRetrievesCorrectly()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(LogAsync_RecordsActivity_AndRetrievesCorrectly));
        var auditService = new AuditService(db);

        await auditService.LogAsync(
            actorId: 1,
            actorEmail: "admin@sunshine.org",
            action: "DOCTOR_VERIFIED",
            targetType: "DOCTOR",
            targetId: "1",
            details: "Verified BMDC credentials for Dr. Tanvir Ahmed (A-54892).",
            ipAddress: "127.0.0.1"
        );

        var logs = await auditService.GetRecentLogsAsync(10);
        Assert.Single(logs);
        Assert.Equal("DOCTOR_VERIFIED", logs[0].Action);
        Assert.Equal("admin@sunshine.org", logs[0].ActorEmail);
        Assert.Equal("1", logs[0].TargetId);
        Assert.Equal("127.0.0.1", logs[0].IpAddress);
    }
}
