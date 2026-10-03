# Serializable Isolation & Concurrency Control

In `AppointmentService.cs`:
```csharp
using var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);
// Checks for slot conflict, inserts appointment, and commits transaction safely!
```
