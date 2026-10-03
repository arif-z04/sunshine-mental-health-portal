# Dependency Injection in Sunshine

ASP.NET Core features built-in **Dependency Injection (DI)**.

---

## 1. What is Dependency Injection?

Instead of a class creating its own dependencies using `new MyService()`, dependencies are provided ("injected") from the outside through constructor parameters:

```csharp
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    // The DI container automatically supplies the implementation
    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
}
```

---

## 2. Service Lifetimes

In `Program.cs`, services are registered with specific lifetimes:
* **Transient**: Created each time they are requested.
* **Scoped** (Used by Sunshine): Created once per client HTTP request. `ApplicationDbContext` and all domain services (`IAppointmentService`, `IPaymentService`) are registered as Scoped.
* **Singleton**: Created once and shared across all requests throughout the application's entire lifetime.
