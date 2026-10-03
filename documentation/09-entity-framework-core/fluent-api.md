# The Fluent API (`OnModelCreating`)

Configuring composite keys and partial unique indexes in `ApplicationDbContext.cs`:
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<DoctorSpecialization>()
        .HasKey(ds => new { ds.DoctorId, ds.SpecializationId });
}
```
