using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface IDoctorService
{
    Task<List<DoctorDto>> GetDoctorsAsync(string? specialization, string? paymentPolicy, bool? availableOnly);
    Task<DoctorDto> GetDoctorByIdAsync(int id);
    Task<List<DoctorScheduleDto>> GetDoctorSchedulesAsync(int doctorId);
    Task<List<DoctorScheduleDto>> SaveDoctorSchedulesAsync(int doctorUserId, List<SaveDoctorScheduleRequest> schedules);
    Task<DoctorDto> UpdateDoctorProfileAsync(int doctorUserId, UpdateDoctorProfileRequest request);
    Task<bool> VerifyDoctorStatusAsync(int doctorId, bool isAvailable);
}

public class DoctorService : IDoctorService
{
    private readonly ApplicationDbContext _db;

    public DoctorService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<DoctorDto>> GetDoctorsAsync(string? specialization, string? paymentPolicy, bool? availableOnly)
    {
        var query = _db.Doctors
            .Include(d => d.User)
            .Include(d => d.Schedules)
            .Where(d => d.User.IsActive)
            .AsQueryable();

        if (availableOnly.HasValue && availableOnly.Value)
        {
            query = query.Where(d => d.IsAvailable);
        }

        if (!string.IsNullOrWhiteSpace(specialization))
        {
            var spec = specialization.Trim().ToLower();
            query = query.Where(d => d.Specialization.ToLower().Contains(spec));
        }

        if (!string.IsNullOrWhiteSpace(paymentPolicy))
        {
            var pol = paymentPolicy.Trim().ToUpperInvariant();
            query = query.Where(d => d.PaymentPolicy == pol);
        }

        var list = await query.OrderBy(d => d.User.FullName).ToListAsync();
        return list.Select(MapToDto).ToList();
    }

    public async Task<DoctorDto> GetDoctorByIdAsync(int id)
    {
        var doctor = await _db.Doctors
            .Include(d => d.User)
            .Include(d => d.Schedules)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }

        return MapToDto(doctor);
    }

    public async Task<List<DoctorScheduleDto>> GetDoctorSchedulesAsync(int doctorId)
    {
        return await _db.DoctorSchedules
            .Where(s => s.DoctorId == doctorId)
            .OrderBy(s => s.DayOfWeek)
            .ThenBy(s => s.StartTime)
            .Select(s => new DoctorScheduleDto(
                s.Id, s.DoctorId, s.DayOfWeek, s.StartTime, s.EndTime, s.SlotDurationMinutes, s.IsActive
            ))
            .ToListAsync();
    }

    public async Task<List<DoctorScheduleDto>> SaveDoctorSchedulesAsync(int doctorUserId, List<SaveDoctorScheduleRequest> schedules)
    {
        var doctor = await _db.Doctors
            .Include(d => d.Schedules)
            .FirstOrDefaultAsync(d => d.UserId == doctorUserId);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor profile not found.");
        }

        _db.DoctorSchedules.RemoveRange(doctor.Schedules);

        foreach (var s in schedules)
        {
            if (s.StartTime >= s.EndTime)
            {
                throw new ArgumentException($"Start time {s.StartTime} must be before end time {s.EndTime}.");
            }

            _db.DoctorSchedules.Add(new DoctorSchedule
            {
                DoctorId = doctor.Id,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                SlotDurationMinutes = s.SlotDurationMinutes > 0 ? s.SlotDurationMinutes : 30,
                IsActive = s.IsActive
            });
        }

        await _db.SaveChangesAsync();

        return await GetDoctorSchedulesAsync(doctor.Id);
    }

    public async Task<DoctorDto> UpdateDoctorProfileAsync(int doctorUserId, UpdateDoctorProfileRequest request)
    {
        var doctor = await _db.Doctors
            .Include(d => d.User)
            .Include(d => d.Schedules)
            .FirstOrDefaultAsync(d => d.UserId == doctorUserId);

        if (doctor == null)
        {
            throw new KeyNotFoundException("Doctor profile not found.");
        }

        doctor.User.FullName = request.FullName.Trim();
        doctor.User.PhoneNumber = request.Phone?.Trim();
        doctor.User.UpdatedAt = DateTime.UtcNow;

        doctor.Specialization = request.Specialization.Trim();
        doctor.Bio = request.Bio?.Trim();
        doctor.ConsultationFee = request.ConsultationFee;
        doctor.PaymentPolicy = request.PaymentPolicy.ToUpperInvariant() == PaymentPolicies.PostPayment
            ? PaymentPolicies.PostPayment
            : PaymentPolicies.Advance;
        doctor.IsAvailable = request.IsAvailable;
        doctor.ExperienceYears = request.ExperienceYears;
        doctor.Qualification = request.Qualification?.Trim();
        doctor.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return MapToDto(doctor);
    }

    public async Task<bool> VerifyDoctorStatusAsync(int doctorId, bool isAvailable)
    {
        var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
        if (doctor == null) return false;

        doctor.IsAvailable = isAvailable;
        doctor.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }

    private static DoctorDto MapToDto(Doctor d)
    {
        return new DoctorDto(
            d.Id,
            d.UserId,
            d.User?.Email ?? "",
            d.User?.FullName ?? "Unknown Therapist",
            d.User?.PhoneNumber,
            d.Specialization,
            d.Bio,
            d.ConsultationFee,
            d.PaymentPolicy,
            d.IsAvailable,
            d.ExperienceYears,
            d.Qualification,
            d.Schedules?.Select(s => new DoctorScheduleDto(
                s.Id, s.DoctorId, s.DayOfWeek, s.StartTime, s.EndTime, s.SlotDurationMinutes, s.IsActive
            )).ToList() ?? new List<DoctorScheduleDto>()
        );
    }
}
