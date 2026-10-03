# Serializable Transactions & Concurrency

How Sunshine protects against race conditions when booking appointments.

---

## 1. The Race Condition Problem

If two patients click "Book Appointment" at the exact same millisecond for the same doctor and slot:
1. Thread A checks if slot is taken: `false`.
2. Thread B checks if slot is taken: `false`.
3. Thread A inserts appointment.
4. Thread B inserts appointment.
Result: The doctor is double-booked!

---

## 2. The Solution: Serializable Transaction in C#

In `AppointmentService.cs`:

```csharp
using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
try
{
    var conflict = await _db.Appointments
        .AnyAsync(a => a.DoctorId == doctorId 
                    && a.AppointmentDate == date 
                    && a.StartTime == time 
                    && a.Status != AppointmentStatuses.Cancelled);

    if (conflict)
    {
        throw new InvalidOperationException("This time slot has already been booked.");
    }

    var appointment = new Appointment { ... };
    _db.Appointments.Add(appointment);
    await _db.SaveChangesAsync();

    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```
Combined with the PostgreSQL partial unique index, double-booking is mathematically impossible.
