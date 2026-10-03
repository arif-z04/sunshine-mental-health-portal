using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Models;

namespace Sunshine.App.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ResourceCategory> ResourceCategories => Set<ResourceCategory>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<ResourceAccess> ResourceAccesses => Set<ResourceAccess>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Rename Identity tables
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("users");
            entity.HasIndex(u => u.Email).IsUnique();
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("roles");
        });

        builder.Entity<IdentityUserRole<int>>(entity =>
        {
            entity.ToTable("user_roles");
        });

        builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("user_claims"));
        builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("user_logins"));
        builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("role_claims"));
        builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("user_tokens"));

        // 1-to-1 User <-> Patient
        builder.Entity<Patient>(entity =>
        {
            entity.ToTable("patients");
            entity.HasOne(p => p.User)
                  .WithOne(u => u.PatientProfile)
                  .HasForeignKey<Patient>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // 1-to-1 User <-> Doctor
        builder.Entity<Doctor>(entity =>
        {
            entity.ToTable("doctors");
            entity.HasOne(d => d.User)
                  .WithOne(u => u.DoctorProfile)
                  .HasForeignKey<Doctor>(d => d.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Doctor Schedules
        builder.Entity<DoctorSchedule>(entity =>
        {
            entity.ToTable("doctor_schedules");
            entity.HasOne(s => s.Doctor)
                  .WithMany(d => d.Schedules)
                  .HasForeignKey(s => s.DoctorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Appointments & Partial Unique Index for double booking protection
        builder.Entity<Appointment>(entity =>
        {
            entity.ToTable("appointments");
            entity.HasIndex(a => new { a.DoctorId, a.AppointmentDate, a.StartTime })
                  .HasFilter("\"Status\" != 'CANCELLED'")
                  .IsUnique();

            entity.HasOne(a => a.Patient)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(a => a.PatientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Doctor)
                  .WithMany(d => d.Appointments)
                  .HasForeignKey(a => a.DoctorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Subscriptions & Plans
        builder.Entity<SubscriptionPlan>(entity =>
        {
            entity.ToTable("subscription_plans");
        });

        builder.Entity<Subscription>(entity =>
        {
            entity.ToTable("subscriptions");
            entity.HasOne(s => s.User)
                  .WithMany(u => u.Subscriptions)
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Plan)
                  .WithMany(p => p.Subscriptions)
                  .HasForeignKey(s => s.PlanId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.Payment)
                  .WithMany()
                  .HasForeignKey(s => s.PaymentId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Payments
        builder.Entity<Payment>(entity =>
        {
            entity.ToTable("payments");
            entity.HasIndex(p => p.TransactionId).IsUnique();

            entity.HasOne(p => p.User)
                  .WithMany(u => u.Payments)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Appointment)
                  .WithMany(a => a.Payments)
                  .HasForeignKey(p => p.AppointmentId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(p => p.Subscription)
                  .WithMany()
                  .HasForeignKey(p => p.SubscriptionId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Resource categories & resources
        builder.Entity<ResourceCategory>(entity =>
        {
            entity.ToTable("resource_categories");
            entity.HasIndex(c => c.Slug).IsUnique();
        });

        builder.Entity<Resource>(entity =>
        {
            entity.ToTable("resources");
            entity.HasOne(r => r.Category)
                  .WithMany(c => c.Resources)
                  .HasForeignKey(r => r.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Resource Access Log
        builder.Entity<ResourceAccess>(entity =>
        {
            entity.ToTable("resource_access");
            entity.HasOne(ra => ra.User)
                  .WithMany(u => u.ResourceAccesses)
                  .HasForeignKey(ra => ra.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ra => ra.Resource)
                  .WithMany(r => r.AccessLogs)
                  .HasForeignKey(ra => ra.ResourceId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Notifications
        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");
            entity.HasOne(n => n.User)
                  .WithMany(u => u.Notifications)
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Audit Logs
        builder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("audit_logs");
            entity.HasOne(a => a.Actor)
                  .WithMany()
                  .HasForeignKey(a => a.ActorId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
