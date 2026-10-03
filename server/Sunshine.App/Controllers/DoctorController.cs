using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sunshine.App.DTOs;
using Sunshine.App.Models;
using Sunshine.App.Services;

namespace Sunshine.App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = AppRoles.Doctor)]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IAppointmentService _appointmentService;

    public DoctorController(IDoctorService doctorService, IAppointmentService appointmentService)
    {
        _doctorService = doctorService;
        _appointmentService = appointmentService;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<DoctorDto>> GetProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var doctor = (await _doctorService.GetDoctorsAsync(null, null, null))
            .FirstOrDefault(d => d.UserId == userId);

        if (doctor == null) return NotFound(new { message = "Doctor profile not found." });
        return Ok(doctor);
    }

    [HttpPut("profile")]
    public async Task<ActionResult<DoctorDto>> UpdateProfile([FromBody] UpdateDoctorProfileRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var updated = await _doctorService.UpdateDoctorProfileAsync(userId, request);
        return Ok(updated);
    }

    /// <summary>
    /// Fast Policy Switch: Toggle between ADVANCE and POST_PAYMENT
    /// </summary>
    [HttpPatch("policy")]
    public async Task<ActionResult<DoctorDto>> UpdatePaymentPolicy([FromBody] UpdatePaymentPolicyRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var doctor = (await _doctorService.GetDoctorsAsync(null, null, null))
            .FirstOrDefault(d => d.UserId == userId);

        if (doctor == null) return NotFound();

        var updated = await _doctorService.UpdateDoctorProfileAsync(userId, new UpdateDoctorProfileRequest(
            doctor.FullName,
            doctor.Phone,
            doctor.Specialization,
            doctor.Bio,
            doctor.ConsultationFee,
            request.PaymentPolicy,
            doctor.IsAvailable,
            doctor.ExperienceYears,
            doctor.Qualification
        ));

        return Ok(updated);
    }

    [HttpGet("schedules")]
    public async Task<ActionResult<List<DoctorScheduleDto>>> GetSchedules()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var doctor = (await _doctorService.GetDoctorsAsync(null, null, null))
            .FirstOrDefault(d => d.UserId == userId);

        if (doctor == null) return NotFound();
        var schedules = await _doctorService.GetDoctorSchedulesAsync(doctor.Id);
        return Ok(schedules);
    }

    [HttpPost("schedules")]
    public async Task<ActionResult<List<DoctorScheduleDto>>> SaveSchedules([FromBody] List<SaveDoctorScheduleRequest> schedules)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var updated = await _doctorService.SaveDoctorSchedulesAsync(userId, schedules);
        return Ok(updated);
    }

    [HttpGet("appointments")]
    public async Task<ActionResult<List<AppointmentDto>>> GetAppointments()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var list = await _appointmentService.GetDoctorAppointmentsAsync(userId);
        return Ok(list);
    }

    [HttpPatch("appointments/{id:int}/status")]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointmentStatus(int id, [FromBody] UpdateAppointmentStatusRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var updated = await _appointmentService.UpdateAppointmentStatusAsync(userId, AppRoles.Doctor, id, request);
        return Ok(updated);
    }
}

public record UpdatePaymentPolicyRequest(string PaymentPolicy);
