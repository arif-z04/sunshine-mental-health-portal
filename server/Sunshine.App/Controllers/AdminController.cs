using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;
using Sunshine.App.Services;

namespace Sunshine.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IAppointmentService _appointmentService;
    private readonly IPaymentService _paymentService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IResourceService _resourceService;
    private readonly IDoctorService _doctorService;
    private readonly IAuditService _auditService;
    private readonly IConfiguration _config;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        IAppointmentService appointmentService,
        IPaymentService paymentService,
        ISubscriptionService subscriptionService,
        IResourceService resourceService,
        IDoctorService doctorService,
        IAuditService auditService,
        IConfiguration config)
    {
        _userManager = userManager;
        _db = db;
        _appointmentService = appointmentService;
        _paymentService = paymentService;
        _subscriptionService = subscriptionService;
        _resourceService = resourceService;
        _doctorService = doctorService;
        _auditService = auditService;
        _config = config;
    }

    /// <summary>
    /// Isolated Admin Login endpoint at /api/admin/login.
    /// Strictly rejects any credentials that do not belong to the ADMIN role.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> AdminLogin([FromBody] LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized(new AuthResponse(false, "Invalid administrator credentials.", null, null));
        }

        // Strict role validation
        if (user.Role != AppRoles.Admin || !await _userManager.IsInRoleAsync(user, AppRoles.Admin))
        {
            return StatusCode(403, new AuthResponse(false, "Access Denied: You do not have administrator permissions to access the Admin Portal.", null, null));
        }

        if (!user.IsActive)
        {
            return StatusCode(403, new AuthResponse(false, "Administrator account has been deactivated.", null, null));
        }

        await _auditService.LogAsync(
            user.Id,
            user.Email,
            "ADMIN_LOGIN",
            "AUTH",
            user.Id.ToString(),
            $"Administrator {user.FullName} logged into Admin Portal.",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        var token = GenerateAdminJwtToken(user);
        return Ok(new AuthResponse(true, "Admin authentication successful.", token, new UserDto(
            user.Id, user.Email ?? "", user.FullName, user.PhoneNumber, user.Role, user.IsActive, null, null, user.CreatedAt
        )));
    }

    [HttpGet("metrics")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<AdminDashboardMetricsDto>> GetDashboardMetrics()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var totalRev = await _db.Payments
            .Where(p => p.Status == PaymentStatuses.Success)
            .SumAsync(p => p.Amount);

        var activeApts = await _db.Appointments
            .CountAsync(a => a.Status == AppointmentStatuses.Confirmed && a.AppointmentDate >= today);

        var activeSubs = await _db.Subscriptions
            .CountAsync(s => s.Status == SubscriptionStatuses.Active && s.StartDate <= today && today <= s.EndDate);

        var totalDocs = await _db.Doctors.CountAsync();
        var totalPatients = await _db.Patients.CountAsync();
        var totalResources = await _db.Resources.CountAsync();

        return Ok(new AdminDashboardMetricsDto(
            totalRev,
            activeApts,
            activeSubs,
            totalDocs,
            totalPatients,
            totalResources
        ));
    }

    [HttpGet("users")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<UserDto>>> GetUsers([FromQuery] string? role, [FromQuery] string? search)
    {
        var query = _userManager.Users
            .Include(u => u.PatientProfile)
            .Include(u => u.DoctorProfile)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(role))
        {
            var r = role.Trim().ToUpperInvariant();
            query = query.Where(u => u.Role == r);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim().ToLowerInvariant();
            query = query.Where(u =>
                (u.FullName != null && u.FullName.ToLower().Contains(s)) ||
                (u.Email != null && u.Email.ToLower().Contains(s)) ||
                (u.PhoneNumber != null && u.PhoneNumber.Contains(s))
            );
        }

        var list = await query
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new UserDto(
                u.Id,
                u.Email ?? "",
                u.FullName,
                u.PhoneNumber,
                u.Role,
                u.IsActive,
                u.PatientProfile != null ? u.PatientProfile.Id : null,
                u.DoctorProfile != null ? u.DoctorProfile.Id : null,
                u.CreatedAt
            ))
            .ToListAsync();

        return Ok(list);
    }

    [HttpPatch("users/{id:int}/status")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult> UpdateUserStatus(int id, [FromBody] UpdateUserStatusRequest req)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return NotFound(new { message = "User not found." });

        user.IsActive = req.IsActive;
        user.UpdatedAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            req.IsActive ? "USER_ACTIVATED" : "USER_DEACTIVATED",
            "USER",
            user.Id.ToString(),
            $"User {user.Email} status changed to IsActive={req.IsActive}.",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(new { message = $"User status updated to {(req.IsActive ? "Active" : "Inactive")}." });
    }

    [HttpGet("doctors")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<DoctorDto>>> GetDoctors()
    {
        var doctors = await _doctorService.GetDoctorsAsync(null, null, null);
        return Ok(doctors);
    }

    [HttpPatch("doctors/{id:int}/verify")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult> VerifyDoctor(int id, [FromBody] DoctorVerificationRequest req)
    {
        var ok = await _doctorService.VerifyDoctorStatusAsync(id, req.IsAvailable);
        if (!ok) return NotFound(new { message = "Doctor not found." });

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            req.IsAvailable ? "DOCTOR_VERIFIED" : "DOCTOR_UNAVAILABLE",
            "DOCTOR",
            id.ToString(),
            $"Doctor ID {id} availability and BMDC verification set to {req.IsAvailable}.",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(new { message = $"Doctor availability set to {req.IsAvailable}." });
    }

    [HttpGet("appointments")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<AppointmentDto>>> GetAppointments()
    {
        var list = await _appointmentService.GetAllAppointmentsAsync();
        return Ok(list);
    }

    [HttpPatch("appointments/{id:int}/status")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointmentStatus(int id, [FromBody] UpdateAppointmentStatusRequest req)
    {
        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        var updated = await _appointmentService.UpdateAppointmentStatusAsync(currentAdminId, AppRoles.Admin, id, req);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            "APPOINTMENT_STATUS_UPDATE",
            "APPOINTMENT",
            id.ToString(),
            $"Admin changed appointment #{id} status to {req.Status}. Note: {req.Notes}",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(updated);
    }

    [HttpGet("plans")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<SubscriptionPlanDto>>> GetPlans()
    {
        var plans = await _subscriptionService.GetPlansAsync();
        return Ok(plans);
    }

    [HttpPost("plans")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<SubscriptionPlanDto>> CreatePlan([FromBody] SubscriptionPlanDto dto)
    {
        var created = await _subscriptionService.CreateOrUpdatePlanAsync(dto);

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            "PLAN_CREATED",
            "SUBSCRIPTION_PLAN",
            created.Id.ToString(),
            $"Created subscription plan: {created.Name} ({created.Price} BDT)",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(created);
    }

    [HttpPut("plans/{id:int}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<SubscriptionPlanDto>> UpdatePlan(int id, [FromBody] SubscriptionPlanDto dto)
    {
        var updated = await _subscriptionService.CreateOrUpdatePlanAsync(dto with { Id = id });

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            "PLAN_UPDATED",
            "SUBSCRIPTION_PLAN",
            id.ToString(),
            $"Updated subscription plan #{id}: {updated.Name} ({updated.Price} BDT)",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(updated);
    }

    [HttpGet("resources")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<ResourceDto>>> GetResources()
    {
        var resources = await _resourceService.GetResourcesAsync(null, null, null);
        return Ok(resources);
    }

    [HttpPost("resources")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<ResourceDto>> CreateResource([FromBody] CreateResourceRequest request)
    {
        var res = await _resourceService.CreateOrUpdateResourceAsync(request, null);

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            "RESOURCE_CREATED",
            "RESOURCE",
            res.Id.ToString(),
            $"Published new resource: {res.Title} (Premium={res.IsPremium})",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(res);
    }

    [HttpPut("resources/{id:int}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<ResourceDto>> UpdateResource(int id, [FromBody] CreateResourceRequest request)
    {
        var res = await _resourceService.CreateOrUpdateResourceAsync(request, id);

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            "RESOURCE_UPDATED",
            "RESOURCE",
            id.ToString(),
            $"Updated resource #{id}: {res.Title}",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(res);
    }

    [HttpDelete("resources/{id:int}")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult> DeleteResource(int id)
    {
        var ok = await _resourceService.DeleteResourceAsync(id);
        if (!ok) return NotFound();

        var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

        await _auditService.LogAsync(
            currentAdminId,
            currentAdminEmail,
            "RESOURCE_DELETED",
            "RESOURCE",
            id.ToString(),
            $"Deleted resource #{id}.",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        return Ok(new { message = "Resource deleted successfully." });
    }

    [HttpGet("payments")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<PaymentDto>>> GetPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpPost("payments/{id:int}/refund")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<PaymentResultDto>> RefundPayment(int id)
    {
        var result = await _paymentService.RefundPaymentAsync(id);

        if (result.Success)
        {
            var currentAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var currentAdminEmail = User.FindFirstValue(ClaimTypes.Email);

            await _auditService.LogAsync(
                currentAdminId,
                currentAdminEmail,
                "PAYMENT_REFUNDED",
                "PAYMENT",
                id.ToString(),
                $"Processed refund for payment #{id}. Result: {result.Message}",
                HttpContext.Connection.RemoteIpAddress?.ToString()
            );
        }

        return Ok(result);
    }

    [HttpGet("audit-logs")]
    [Authorize(Roles = AppRoles.Admin)]
    public async Task<ActionResult<List<AuditLogDto>>> GetAuditLogs([FromQuery] int limit = 100)
    {
        var logs = await _auditService.GetRecentLogsAsync(limit);
        return Ok(logs);
    }

    private string GenerateAdminJwtToken(ApplicationUser user)
    {
        var jwtKey = _config["Jwt:Key"] ?? "SunshineCounselingSecretKey2026!MustBeVerySecureAnd32BytesLong";
        var jwtIssuer = _config["Jwt:Issuer"] ?? "Sunshine.Api";
        var jwtAudience = _config["Jwt:Audience"] ?? "Sunshine.Client";

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, AppRoles.Admin)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(7);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record DoctorVerificationRequest(bool IsAvailable);
