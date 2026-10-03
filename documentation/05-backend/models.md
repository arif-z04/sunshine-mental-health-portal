# Domain Models (`Entities.cs`)

Domain models are C# classes directly representing PostgreSQL tables.

---

## 1. Inspecting `server/Sunshine.App/Models/Entities.cs`

```csharp
[Table("appointments")]
public class Appointment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [ForeignKey(nameof(DoctorId))]
    public Doctor? Doctor { get; set; }

    [Required]
    public int PatientId { get; set; }

    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }

    public DateOnly AppointmentDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = AppointmentStatuses.Pending;

    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

* Attributes like `[Table("appointments")]` and `[ForeignKey]` instruct EF Core how to bind C# properties to PostgreSQL schema definitions.
