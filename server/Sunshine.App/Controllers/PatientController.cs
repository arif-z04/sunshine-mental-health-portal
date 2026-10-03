using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;
using Sunshine.App.Services;

namespace Sunshine.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentService _appointmentService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IResourceService _resourceService;
    private readonly IPaymentService _paymentService;
    private readonly INotificationService _notificationService;
    private readonly ApplicationDbContext _db;

    public PatientController(
        IDoctorService doctorService,
        IAppointmentService appointmentService,
        ISubscriptionService subscriptionService,
        IResourceService resourceService,
        IPaymentService paymentService,
        INotificationService notificationService,
        ApplicationDbContext db)
    {
        _doctorService = doctorService;
        _appointmentService = appointmentService;
        _subscriptionService = subscriptionService;
        _resourceService = resourceService;
        _paymentService = paymentService;
        _notificationService = notificationService;
        _db = db;
    }

    // Public / Open Doctor search
    [HttpGet("doctors")]
    public async Task<ActionResult<List<DoctorDto>>> GetDoctors(
        [FromQuery] string? specialization,
        [FromQuery] string? paymentPolicy,
        [FromQuery] bool? availableOnly)
    {
        var doctors = await _doctorService.GetDoctorsAsync(specialization, paymentPolicy, availableOnly);
        return Ok(doctors);
    }

    [HttpGet("doctors/{id:int}")]
    public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        return Ok(doctor);
    }

    [HttpGet("doctors/{id:int}/slots")]
    public async Task<ActionResult<List<AvailableSlotDto>>> GetSlots(int id, [FromQuery] string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
        {
            return BadRequest(new { message = "Invalid date format. Expected YYYY-MM-DD." });
        }
        var slots = await _appointmentService.GetAvailableSlotsAsync(id, parsedDate);
        return Ok(slots);
    }

    // Authenticated Patient Endpoints
    [HttpPost("appointments/book")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<AppointmentDto>> BookAppointment([FromBody] BookAppointmentRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var appointment = await _appointmentService.BookAppointmentAsync(userId, request);
        return Ok(appointment);
    }

    [HttpGet("appointments")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<List<AppointmentDto>>> GetMyAppointments()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var list = await _appointmentService.GetPatientAppointmentsAsync(userId);
        return Ok(list);
    }

    [HttpGet("appointments/{id:int}")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var appointment = await _appointmentService.GetAppointmentByIdAsync(userId, AppRoles.Patient, id);
        return Ok(appointment);
    }

    [HttpPost("appointments/{id:int}/reschedule")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<AppointmentDto>> RescheduleAppointment(int id, [FromBody] RescheduleAppointmentRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var updated = await _appointmentService.RescheduleAppointmentAsync(userId, AppRoles.Patient, id, request);
        return Ok(updated);
    }

    [HttpPost("appointments/{id:int}/cancel")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<AppointmentDto>> CancelAppointment(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var updated = await _appointmentService.CancelAppointmentAsync(userId, AppRoles.Patient, id);
        return Ok(updated);
    }

    [HttpGet("profile")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<PatientProfileDto>> GetProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var patient = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
            _db.Patients.Include(p => p.User),
            p => p.UserId == userId
        );

        if (patient == null)
        {
            return NotFound(new { message = "Patient profile not found." });
        }

        return Ok(new PatientProfileDto(
            patient.Id,
            patient.UserId,
            patient.User.Email ?? "",
            patient.User.FullName,
            patient.User.PhoneNumber,
            patient.DateOfBirth,
            patient.Gender,
            patient.EmergencyContact,
            patient.MedicalHistoryNotes,
            patient.CreatedAt
        ));
    }

    [HttpPut("profile")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<PatientProfileDto>> UpdateProfile([FromBody] UpdatePatientProfileRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var patient = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
            _db.Patients.Include(p => p.User),
            p => p.UserId == userId
        );

        if (patient == null)
        {
            return NotFound(new { message = "Patient profile not found." });
        }

        patient.User.FullName = request.FullName.Trim();
        patient.User.PhoneNumber = request.Phone?.Trim();
        patient.User.UpdatedAt = DateTime.UtcNow;

        patient.DateOfBirth = request.DateOfBirth;
        patient.Gender = request.Gender?.Trim();
        patient.EmergencyContact = request.EmergencyContact?.Trim();
        patient.MedicalHistoryNotes = request.MedicalHistoryNotes?.Trim();
        patient.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(new PatientProfileDto(
            patient.Id,
            patient.UserId,
            patient.User.Email ?? "",
            patient.User.FullName,
            patient.User.PhoneNumber,
            patient.DateOfBirth,
            patient.Gender,
            patient.EmergencyContact,
            patient.MedicalHistoryNotes,
            patient.CreatedAt
        ));
    }

    [HttpGet("subscriptions")]
    public async Task<ActionResult<object>> GetSubscriptions()
    {
        var plans = await _subscriptionService.GetPlansAsync();
        SubscriptionDto? activeSub = null;

        if (User.Identity?.IsAuthenticated == true)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(claim, out var userId))
            {
                activeSub = await _subscriptionService.GetUserActiveSubscriptionAsync(userId);
            }
        }

        return Ok(new { plans, activeSubscription = activeSub });
    }

    [HttpPost("payments/process")]
    [Authorize(Roles = AppRoles.Patient)]
    public async Task<ActionResult<PaymentResultDto>> ProcessPayment([FromBody] ProcessPaymentRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _paymentService.ProcessPaymentAsync(userId, request);
        return Ok(result);
    }

    [HttpGet("resources/categories")]
    public async Task<ActionResult<List<ResourceCategoryDto>>> GetCategories()
    {
        var categories = await _resourceService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("resources")]
    public async Task<ActionResult<List<ResourceDto>>> GetResources(
        [FromQuery] int? categoryId,
        [FromQuery] string? search)
    {
        int? userId = null;
        if (User.Identity?.IsAuthenticated == true)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(claim, out var parsed))
            {
                userId = parsed;
            }
        }

        var resources = await _resourceService.GetResourcesAsync(categoryId, search, userId);
        return Ok(resources);
    }

    [HttpPost("resources/{id:int}/access")]
    [Authorize]
    public async Task<ActionResult<ResourceDto>> AccessResource(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var resource = await _resourceService.AccessResourceAsync(userId, id);
        return Ok(resource);
    }

    [HttpGet("notifications")]
    [Authorize]
    public async Task<ActionResult<List<Notification>>> GetNotifications()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var notifications = await _notificationService.GetUserNotificationsAsync(userId);
        return Ok(notifications);
    }

    [HttpPatch("notifications/{id:int}/read")]
    [Authorize]
    public async Task<ActionResult> MarkNotificationRead(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _notificationService.MarkAsReadAsync(userId, id);
        return Ok(new { message = "Marked as read." });
    }
}
