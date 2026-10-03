using System;
using System.Threading.Tasks;
using Sunshine.App.DTOs;
using Sunshine.App.Models;
using Sunshine.App.Services;
using Xunit;

namespace Sunshine.Tests;

public class AppointmentSchedulingTests
{
    [Fact]
    public async Task GetAvailableSlotsAsync_GeneratesCorrect45MinuteSlots()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(GetAvailableSlotsAsync_GeneratesCorrect45MinuteSlots));
        var (_, doc, _, _) = TestHelpers.SeedDoctorAndPatient(db);
        var notifService = new NotificationService(db);
        var aptService = new AppointmentService(db, notifService);

        // Monday date (DayOfWeek = 1)
        var mondayDate = new DateOnly(2026, 10, 5); // 2026-10-05 is Monday
        var slots = await aptService.GetAvailableSlotsAsync(doc.Id, mondayDate);

        // Schedule is 16:00 - 18:15 with 45-min duration -> exactly 3 slots
        Assert.Equal(3, slots.Count);
        Assert.Equal(new TimeOnly(16, 0), slots[0].StartTime);
        Assert.Equal(new TimeOnly(16, 45), slots[0].EndTime);
        Assert.False(slots[0].IsBooked);

        Assert.Equal(new TimeOnly(16, 45), slots[1].StartTime);
        Assert.Equal(new TimeOnly(17, 30), slots[1].EndTime);
        Assert.False(slots[1].IsBooked);

        Assert.Equal(new TimeOnly(17, 30), slots[2].StartTime);
        Assert.Equal(new TimeOnly(18, 15), slots[2].EndTime);
        Assert.False(slots[2].IsBooked);
    }

    [Fact]
    public async Task BookAppointmentAsync_AdvancePaymentPolicy_SetsPendingAnd15MinuteExpiration()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(BookAppointmentAsync_AdvancePaymentPolicy_SetsPendingAnd15MinuteExpiration));
        var (_, doc, patUser, _) = TestHelpers.SeedDoctorAndPatient(db);
        var notifService = new NotificationService(db);
        var aptService = new AppointmentService(db, notifService);

        var mondayDate = new DateOnly(2026, 10, 5);
        var request = new BookAppointmentRequest(
            doc.Id,
            mondayDate,
            new TimeOnly(16, 0),
            new TimeOnly(16, 45),
            "Anxiety and stress consultation"
        );

        var apt = await aptService.BookAppointmentAsync(patUser.Id, request);

        Assert.NotNull(apt);
        Assert.Equal(AppointmentStatuses.Pending, apt.Status);
        Assert.NotNull(apt.BookingExpiresAt);
        Assert.True(apt.BookingExpiresAt > DateTime.UtcNow.AddMinutes(14));
        Assert.True(apt.BookingExpiresAt <= DateTime.UtcNow.AddMinutes(16));
    }

    [Fact]
    public async Task BookAppointmentAsync_DuplicateSlot_ThrowsInvalidOperationException()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(BookAppointmentAsync_DuplicateSlot_ThrowsInvalidOperationException));
        var (_, doc, patUser, _) = TestHelpers.SeedDoctorAndPatient(db);
        var notifService = new NotificationService(db);
        var aptService = new AppointmentService(db, notifService);

        var mondayDate = new DateOnly(2026, 10, 5);
        var request1 = new BookAppointmentRequest(
            doc.Id,
            mondayDate,
            new TimeOnly(16, 0),
            new TimeOnly(16, 45),
            "Patient 1 booking"
        );

        // First booking succeeds
        await aptService.BookAppointmentAsync(patUser.Id, request1);

        // Second booking for exact same doctor, date, and slot
        var request2 = new BookAppointmentRequest(
            doc.Id,
            mondayDate,
            new TimeOnly(16, 0),
            new TimeOnly(16, 45),
            "Patient 2 conflicting booking attempt"
        );

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            aptService.BookAppointmentAsync(patUser.Id, request2)
        );

        Assert.Contains("already been booked", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ReleaseExpiredPendingAppointmentsAsync_CancelsExpiredBookings()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(ReleaseExpiredPendingAppointmentsAsync_CancelsExpiredBookings));
        var (_, doc, _, pat) = TestHelpers.SeedDoctorAndPatient(db);
        var notifService = new NotificationService(db);
        var aptService = new AppointmentService(db, notifService);

        // Insert an expired pending appointment (expired 5 minutes ago)
        var expiredApt = new Appointment
        {
            PatientId = pat.Id,
            DoctorId = doc.Id,
            AppointmentDate = new DateOnly(2026, 10, 5),
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(16, 45),
            Status = AppointmentStatuses.Pending,
            BookingExpiresAt = DateTime.UtcNow.AddMinutes(-5),
            CreatedAt = DateTime.UtcNow.AddMinutes(-20)
        };
        db.Appointments.Add(expiredApt);
        await db.SaveChangesAsync();

        await aptService.ReleaseExpiredPendingAppointmentsAsync();

        var refreshed = await db.Appointments.FindAsync(expiredApt.Id);
        Assert.NotNull(refreshed);
        Assert.Equal(AppointmentStatuses.Cancelled, refreshed.Status);
        Assert.Contains("Auto-cancelled", refreshed.Notes);
    }

    [Fact]
    public async Task RescheduleAppointmentAsync_ValidSlot_Succeeds()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(RescheduleAppointmentAsync_ValidSlot_Succeeds));
        var (_, doc, patUser, pat) = TestHelpers.SeedDoctorAndPatient(db);
        var notifService = new NotificationService(db);
        var aptService = new AppointmentService(db, notifService);

        var apt = new Appointment
        {
            PatientId = pat.Id,
            DoctorId = doc.Id,
            AppointmentDate = new DateOnly(2026, 10, 5),
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(16, 45),
            Status = AppointmentStatuses.Confirmed
        };
        db.Appointments.Add(apt);
        await db.SaveChangesAsync();

        var newDate = new DateOnly(2026, 10, 6);
        var request = new RescheduleAppointmentRequest(newDate, new TimeOnly(17, 30), new TimeOnly(18, 15), "Client schedule change");
        var updated = await aptService.RescheduleAppointmentAsync(patUser.Id, AppRoles.Patient, apt.Id, request);

        Assert.NotNull(updated);
        Assert.Equal(newDate, updated.AppointmentDate);
        Assert.Equal(new TimeOnly(17, 30), updated.StartTime);
        Assert.Contains("Rescheduled from", updated.Notes);
    }

    [Fact]
    public async Task RescheduleAppointmentAsync_ConflictingSlot_ThrowsException()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(RescheduleAppointmentAsync_ConflictingSlot_ThrowsException));
        var (_, doc, patUser, pat) = TestHelpers.SeedDoctorAndPatient(db);
        var notifService = new NotificationService(db);
        var aptService = new AppointmentService(db, notifService);

        var apt1 = new Appointment
        {
            PatientId = pat.Id,
            DoctorId = doc.Id,
            AppointmentDate = new DateOnly(2026, 10, 5),
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(16, 45),
            Status = AppointmentStatuses.Confirmed
        };
        var apt2 = new Appointment
        {
            PatientId = pat.Id,
            DoctorId = doc.Id,
            AppointmentDate = new DateOnly(2026, 10, 6),
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(16, 45),
            Status = AppointmentStatuses.Confirmed
        };
        db.Appointments.AddRange(apt1, apt2);
        await db.SaveChangesAsync();

        // Attempt to reschedule apt1 to apt2's slot
        var request = new RescheduleAppointmentRequest(new DateOnly(2026, 10, 6), new TimeOnly(16, 0), new TimeOnly(16, 45), "Conflict test");
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            aptService.RescheduleAppointmentAsync(patUser.Id, AppRoles.Patient, apt1.Id, request)
        );

        Assert.Contains("already booked", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}
