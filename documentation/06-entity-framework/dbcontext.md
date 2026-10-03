# The `ApplicationDbContext` Deep Dive

`server/Sunshine.App/Data/ApplicationDbContext.cs` coordinates EF Core functionality.

---

## 1. DbSets Configuration

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    // ... all 16 tables
}
```
Each `DbSet<T>` corresponds to a PostgreSQL table and serves as the entrypoint for querying and persisting records.
