# Asynchronous Programming (`async` / `await`)

Why modern web servers must be asynchronous.

---

## 1. The Synchronous Problem
Imagine a bank teller. If a customer asks to verify a document that takes 10 minutes to retrieve from the basement, a synchronous teller stands completely frozen for 10 minutes, making all 50 people in line wait.

## 2. The Asynchronous Solution
An asynchronous teller says: *"Please take a seat; I will retrieve your file in the background while I serve the next customer in line."*

In ASP.NET Core:
```csharp
public async Task<List<Doctor>> GetDoctorsAsync()
{
    // Thread is released to serve other users while PostgreSQL executes the query!
    return await _db.Doctors.ToListAsync();
}
```
