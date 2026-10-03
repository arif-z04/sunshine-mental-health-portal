# Persisting Data with `SaveChangesAsync()`

```csharp
var appointment = new Appointment { DoctorId = 1, PatientId = 2, Status = "PENDING" };
_db.Appointments.Add(appointment);
await _db.SaveChangesAsync(); // Executes SQL INSERT!
```
