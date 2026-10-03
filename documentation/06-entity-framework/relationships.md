# Modeling Relationships in EF Core

Understanding how EF Core navigates relationships using navigation properties.

---

## 1. Navigation Properties

When you query an appointment in C#, you often need the associated Doctor's full name and the Patient's medical phone number:

```csharp
var appointment = await _db.Appointments
    .Include(a => a.Doctor)
        .ThenInclude(d => d!.User)
    .Include(a => a.Patient)
        .ThenInclude(p => p!.User)
    .FirstOrDefaultAsync(a => a.Id == appointmentId);
```
EF Core's `.Include()` translates into SQL `LEFT JOIN` statements, populating the nested objects automatically in a single round-trip.
