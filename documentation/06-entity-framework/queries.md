# Querying with LINQ & AsNoTracking

Writing high-performance database queries using Language Integrated Query (LINQ).

---

## 1. What is LINQ?

LINQ enables writing strongly typed queries directly in C#:
```csharp
var upcomingAppointments = await _db.Appointments
    .AsNoTracking()
    .Where(a => a.DoctorId == doctorId && a.AppointmentDate >= today)
    .OrderBy(a => a.AppointmentDate)
    .ThenBy(a => a.StartTime)
    .ToListAsync();
```

---

## 2. Performance: `AsNoTracking()`

By default, EF Core tracks changes to all queried entities so that calling `SaveChanges()` updates them.
For read-only API requests (like fetching the doctor directory or listing articles), calling `.AsNoTracking()` disables internal tracking, reducing memory consumption by up to 50% and improving query throughput.
