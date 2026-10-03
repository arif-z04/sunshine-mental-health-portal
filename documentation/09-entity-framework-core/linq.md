# LINQ: Querying Databases with C#

**LINQ** (Language Integrated Query) allows you to filter and sort data using C# syntax:
```csharp
var upcoming = await _db.Appointments
    .Where(a => a.DoctorId == 1 && a.AppointmentDate >= today)
    .OrderBy(a => a.AppointmentDate)
    .ToListAsync();
```
EF Core compiles this C# expression into an optimized SQL `SELECT` statement!
