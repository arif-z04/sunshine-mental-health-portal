using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sunshine.App.DTOs;

// --- AUTH DTOs ---
public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password
);

public record RegisterPatientRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    [Required, MaxLength(150)] string FullName,
    string? Phone,
    DateOnly? DateOfBirth,
    string? Gender,
    string? EmergencyContact
);

public record RegisterDoctorRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    [Required, MaxLength(150)] string FullName,
    string? Phone,
    [Required] string Specialization,
    decimal ConsultationFee,
    string PaymentPolicy, // ADVANCE or POST_PAYMENT
    string? Bio,
    int ExperienceYears,
    string? Qualification
);

public record AuthResponse(
    bool Success,
    string Message,
    string? Token,
    UserDto? User
);

public record UserDto(
    int Id,
    string Email,
    string FullName,
    string? Phone,
    string Role,
    bool IsActive,
    int? PatientId,
    int? DoctorId,
    DateTime CreatedAt
);

// --- DOCTOR DTOs ---
public record DoctorDto(
    int Id,
    int UserId,
    string Email,
    string FullName,
    string? Phone,
    string Specialization,
    string? Bio,
    decimal ConsultationFee,
    string PaymentPolicy,
    bool IsAvailable,
    int ExperienceYears,
    string? Qualification,
    List<DoctorScheduleDto> Schedules
);

public record DoctorScheduleDto(
    int Id,
    int DoctorId,
    int DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SlotDurationMinutes,
    bool IsActive
);

public record SaveDoctorScheduleRequest(
    int DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SlotDurationMinutes,
    bool IsActive
);

public record UpdateDoctorProfileRequest(
    string FullName,
    string? Phone,
    string Specialization,
    string? Bio,
    decimal ConsultationFee,
    string PaymentPolicy,
    bool IsAvailable,
    int ExperienceYears,
    string? Qualification
);

// --- APPOINTMENT DTOs ---
public record BookAppointmentRequest(
    [Required] int DoctorId,
    [Required] DateOnly AppointmentDate,
    [Required] TimeOnly StartTime,
    [Required] TimeOnly EndTime,
    string? Reason
);

public record AvailableSlotDto(
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsBooked
);

public record AppointmentDto(
    int Id,
    int PatientId,
    string PatientName,
    string PatientEmail,
    string? PatientPhone,
    string? PatientEmergencyContact,
    int DoctorId,
    string DoctorName,
    string DoctorSpecialization,
    decimal ConsultationFee,
    string PaymentPolicy,
    DateOnly AppointmentDate,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Status,
    string? Reason,
    string? Notes,
    DateTime? BookingExpiresAt,
    bool IsExpired,
    int? PaymentId,
    string? PaymentStatus,
    DateTime CreatedAt
);

public record UpdateAppointmentStatusRequest(
    [Required] string Status,
    string? Notes
);

public record RescheduleAppointmentRequest(
    [Required] DateOnly NewDate,
    [Required] TimeOnly NewStartTime,
    [Required] TimeOnly NewEndTime,
    string? Reason
);

// --- PATIENT PROFILE DTOs ---
public record PatientProfileDto(
    int Id,
    int UserId,
    string Email,
    string FullName,
    string? Phone,
    DateOnly? DateOfBirth,
    string? Gender,
    string? EmergencyContact,
    string? MedicalHistoryNotes,
    DateTime CreatedAt
);

public record UpdatePatientProfileRequest(
    [Required, MaxLength(150)] string FullName,
    string? Phone,
    DateOnly? DateOfBirth,
    string? Gender,
    string? EmergencyContact,
    string? MedicalHistoryNotes
);

// --- SUBSCRIPTION DTOs ---
public record SubscriptionPlanDto(
    int Id,
    string Name,
    string DurationType,
    int DurationDays,
    decimal Price,
    string? Description,
    bool IsActive
);

public record SubscriptionDto(
    int Id,
    int UserId,
    string UserEmail,
    string UserFullName,
    int PlanId,
    string PlanName,
    string DurationType,
    decimal Price,
    DateOnly StartDate,
    DateOnly EndDate,
    string Status,
    bool IsCurrentlyValid,
    int? PaymentId,
    string? TransactionId,
    DateTime CreatedAt
);

// --- RESOURCE DTOs ---
public record ResourceCategoryDto(
    int Id,
    string Name,
    string Slug,
    string? Description,
    int ResourceCount
);

public record ResourceDto(
    int Id,
    int CategoryId,
    string CategoryName,
    string CategorySlug,
    string Title,
    string Author,
    string? Description,
    string ResourceType,
    string? ContentUrl,
    bool IsPremium,
    string? ThumbnailUrl,
    bool HasAccess,
    DateTime CreatedAt
);

public record CreateResourceRequest(
    int CategoryId,
    string Title,
    string Author,
    string? Description,
    string ResourceType,
    string ContentUrl,
    bool IsPremium,
    string? ThumbnailUrl
);

// --- PAYMENT DTOs ---
public record ProcessPaymentRequest(
    [Required] string PaymentType, // APPOINTMENT or SUBSCRIPTION
    int? AppointmentId,
    int? PlanId,
    [Required] string PaymentMethod, // BKASH, NAGAD, ROCKET, CARD
    [Required] string AccountNumberOrCard, // 017xxxxxxxx or Card Number
    string? AccountHolderName,
    string? OtpOrPin
);

public record PaymentDto(
    int Id,
    int UserId,
    string UserEmail,
    string UserFullName,
    decimal Amount,
    string Currency,
    string PaymentType,
    int? AppointmentId,
    int? SubscriptionId,
    string PaymentMethod,
    string? TransactionId,
    string Status,
    DateTime? PaymentDate,
    DateTime CreatedAt
);

public record PaymentResultDto(
    bool Success,
    string Message,
    string? TransactionId,
    PaymentDto? Payment
);

// --- ADMIN DTOs ---
public record AdminDashboardMetricsDto(
    decimal TotalRevenue,
    int ActiveAppointments,
    int ActiveSubscriptions,
    int TotalDoctors,
    int TotalPatients,
    int TotalResources
);

public record AuditLogDto(
    int Id,
    int? ActorId,
    string? ActorEmail,
    string Action,
    string? TargetType,
    string? TargetId,
    string? Details,
    string? IpAddress,
    DateTime CreatedAt
);

public record UpdateUserStatusRequest(bool IsActive);

