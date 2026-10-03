# What is a `DbSet<T>`?

A **`DbSet<T>`** represents the collection of all entities of type `T` in the database. You query it like a list:
```csharp
var activeDoctors = await _db.Doctors
    .Where(d => d.IsVerified == true)
    .ToListAsync();
```
