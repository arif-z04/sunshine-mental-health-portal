# Change Tracking & `AsNoTracking()`

EF Core tracks changes to objects in memory. For read-only queries, disable tracking to conserve memory:
```csharp
var doctors = await _db.Doctors.AsNoTracking().ToListAsync();
```
