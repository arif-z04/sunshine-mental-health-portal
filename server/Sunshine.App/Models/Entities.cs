using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Sunshine.App.Models;

public static class AppRoles
{
    public const string Admin = "ADMIN";
    public const string Doctor = "DOCTOR";
    public const string Patient = "PATIENT";
}

public static class PaymentPolicies
{
    public const string Advance = "ADVANCE";
    public const string PostPayment = "POST_PAYMENT";
}

public static class AppointmentStatuses
{
    public const string Pending = "PENDING";
    public const string Confirmed = "CONFIRMED";
    public const string Completed = "COMPLETED";
    public const string Cancelled = "CANCELLED";
    public const string Rejected = "REJECTED";
    public const string NoShow = "NO_SHOW";
}

public static class PaymentStatuses
{
    public const string Pending = "PENDING";
    public const string Success = "SUCCESS";
    public const string Failed = "FAILED";
    public const string Refunded = "REFUNDED";
}

public static class PaymentTypes
{
    public const string Appointment = "APPOINTMENT";
    public const string Subscription = "SUBSCRIPTION";
}

public static class PaymentMethods
{
    public const string Bkash = "BKASH";
    public const string Nagad = "NAGAD";
    public const string Rocket = "ROCKET";
    public const string Card = "CARD";
}

public static class SubscriptionStatuses
{
    public const string Active = "ACTIVE";
    public const string Expired = "EXPIRED";
    public const string Cancelled = "CANCELLED";
}

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole() : base() { }
    public ApplicationRole(string roleName) : base(roleName) { }
}

public class ApplicationUser : IdentityUser<int>
{
    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = AppRoles.Patient;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual Patient? PatientProfile { get; set; }
    public virtual Doctor? DoctorProfile { get; set; }
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public virtual ICollection<ResourceAccess> ResourceAccesses { get; set; } = new List<ResourceAccess>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

[Table("patients")]
public class Patient
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(20)]
    public string? Gender { get; set; }

    [MaxLength(100)]
    public string? EmergencyContact { get; set; }

    public string? MedicalHistoryNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

[Table("doctors")]
public class Doctor
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    [MaxLength(150)]
    public string Specialization { get; set; } = string.Empty;

    public string? Bio { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ConsultationFee { get; set; } // in BDT (৳)

    [Required]
    [MaxLength(20)]
    public string PaymentPolicy { get; set; } = PaymentPolicies.Advance;

    public bool IsAvailable { get; set; } = true;
    public int ExperienceYears { get; set; }

    [MaxLength(255)]
    public string? Qualification { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

[Table("doctor_schedules")]
public class DoctorSchedule
{
    [Key]
    public int Id { get; set; }

    public int DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    public int DayOfWeek { get; set; } // 0=Sunday, 1=Monday ... 6=Saturday
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int SlotDurationMinutes { get; set; } = 30;
    public bool IsActive { get; set; } = true;
}

[Table("appointments")]
public class Appointment
{
    [Key]
    public int Id { get; set; }

    public int PatientId { get; set; }
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    public int DoctorId { get; set; }
    [ForeignKey(nameof(DoctorId))]
    public virtual Doctor Doctor { get; set; } = null!;

    public DateOnly AppointmentDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = AppointmentStatuses.Pending;

    public string? Reason { get; set; }
    public string? Notes { get; set; }

    // 15-minute expiration timestamp for ADVANCE payment policy appointments
    public DateTime? BookingExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

[Table("subscription_plans")]
public class SubscriptionPlan
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string DurationType { get; set; } = "MONTHLY"; // MONTHLY, QUARTERLY, YEARLY

    public int DurationDays { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; } // in BDT (৳)

    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}

[Table("subscriptions")]
public class Subscription
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    public int PlanId { get; set; }
    [ForeignKey(nameof(PlanId))]
    public virtual SubscriptionPlan Plan { get; set; } = null!;

    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = SubscriptionStatuses.Active;

    public int? PaymentId { get; set; }
    [ForeignKey(nameof(PaymentId))]
    public virtual Payment? Payment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

[Table("payments")]
public class Payment
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [MaxLength(10)]
    public string Currency { get; set; } = "BDT"; // Bangladeshi Taka

    [Required]
    [MaxLength(20)]
    public string PaymentType { get; set; } = PaymentTypes.Appointment;

    public int? AppointmentId { get; set; }
    [ForeignKey(nameof(AppointmentId))]
    public virtual Appointment? Appointment { get; set; }

    public int? SubscriptionId { get; set; }
    [ForeignKey(nameof(SubscriptionId))]
    public virtual Subscription? Subscription { get; set; }

    [MaxLength(50)]
    public string PaymentMethod { get; set; } = PaymentMethods.Bkash; // BKASH, NAGAD, ROCKET, CARD

    [MaxLength(100)]
    public string? TransactionId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = PaymentStatuses.Pending;

    public DateTime? PaymentDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

[Table("resource_categories")]
public class ResourceCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}

[Table("resources")]
public class Resource
{
    [Key]
    public int Id { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual ResourceCategory Category { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Author { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [MaxLength(20)]
    public string ResourceType { get; set; } = "BOOK";

    [Required]
    public string ContentUrl { get; set; } = string.Empty;

    public bool IsPremium { get; set; } = false;
    public string? ThumbnailUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<ResourceAccess> AccessLogs { get; set; } = new List<ResourceAccess>();
}

[Table("resource_access")]
public class ResourceAccess
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    public int ResourceId { get; set; }
    [ForeignKey(nameof(ResourceId))]
    public virtual Resource Resource { get; set; } = null!;

    public DateTime AccessedAt { get; set; } = DateTime.UtcNow;
}

[Table("notifications")]
public class Notification
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Type { get; set; } = "SYSTEM";

    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

[Table("audit_logs")]
public class AuditLog
{
    [Key]
    public int Id { get; set; }

    public int? ActorId { get; set; }
    [ForeignKey(nameof(ActorId))]
    public virtual ApplicationUser? Actor { get; set; }

    [MaxLength(256)]
    public string? ActorEmail { get; set; }

    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? TargetType { get; set; }

    [MaxLength(100)]
    public string? TargetId { get; set; }

    public string? Details { get; set; }

    [MaxLength(50)]
    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

