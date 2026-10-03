# Fluent API Configuration

For complex constraints and composite keys that cannot be expressed with data annotations, EF Core uses the **Fluent API** inside `OnModelCreating`.

---

## 1. Example: Composite Key in `DoctorSpecialization`

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Many-to-Many junction composite primary key
    modelBuilder.Entity<DoctorSpecialization>()
        .HasKey(ds => new { ds.DoctorId, ds.SpecializationId });

    // Partial unique index configuration matching PostgreSQL index
    modelBuilder.Entity<Appointment>()
        .HasIndex(a => new { a.DoctorId, a.AppointmentDate, a.StartTime })
        .HasFilter(""Status" != 'CANCELLED'")
        .IsUnique();
}
```
