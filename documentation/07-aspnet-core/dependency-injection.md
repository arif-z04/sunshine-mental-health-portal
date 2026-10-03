# Service Lifetimes in ASP.NET Core

When you register a service in `Program.cs`, you specify its lifetime:

---

1. **`AddScoped`** (Most Common in Sunshine):
   - Created **once per HTTP request** and shared across all classes participating in that request.
   - Disposed automatically when the HTTP response is sent.
   - Example: `ApplicationDbContext`, `AppointmentService`, `PaymentService`.
2. **`AddTransient`**:
   - Created every single time it is requested.
3. **`AddSingleton`**:
   - Created once when the server boots and lives forever until the server shuts down.
