using System;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.Models;

namespace Sunshine.Tests;

public static class TestHelpers
{
    public static ApplicationDbContext CreateInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var db = new ApplicationDbContext(options);
        db.Database.EnsureCreated();
        return db;
    }

    public static (ApplicationUser docUser, Doctor doc, ApplicationUser patUser, Patient pat) SeedDoctorAndPatient(ApplicationDbContext db)
    {
        var docUser = new ApplicationUser
        {
            Id = 10,
            UserName = "dr.test@sunshine.org",
            Email = "dr.test@sunshine.org",
            FullName = "Dr. Test Clinician, MBBS",
            Role = AppRoles.Doctor,
            PhoneNumber = "+880 1711-123456",
            IsActive = true
        };
        db.Users.Add(docUser);

        var doc = new Doctor
        {
            Id = 10,
            UserId = docUser.Id,
            Specialization = "Psychiatry & CBT",
            ConsultationFee = 1500m,
            PaymentPolicy = PaymentPolicies.Advance,
            IsAvailable = true,
            ExperienceYears = 12,
            Qualification = "MBBS, MD (Psychiatry)"
        };
        db.Doctors.Add(doc);

        // Schedule: Monday (Day 1) 16:00 to 18:15 (three 45-min slots: 16:00, 16:45, 17:30)
        db.DoctorSchedules.Add(new DoctorSchedule
        {
            Id = 10,
            DoctorId = doc.Id,
            DayOfWeek = 1,
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(18, 15),
            SlotDurationMinutes = 45,
            IsActive = true
        });

        var patUser = new ApplicationUser
        {
            Id = 20,
            UserName = "patient.test@example.com",
            Email = "patient.test@example.com",
            FullName = "Test Patient",
            Role = AppRoles.Patient,
            PhoneNumber = "+880 1819-123456",
            IsActive = true
        };
        db.Users.Add(patUser);

        var pat = new Patient
        {
            Id = 20,
            UserId = patUser.Id,
            Gender = "Female",
            DateOfBirth = new DateOnly(1995, 5, 10),
            EmergencyContact = "+880 1711-999999"
        };
        db.Patients.Add(pat);

        db.SaveChanges();
        return (docUser, doc, patUser, pat);
    }
}
