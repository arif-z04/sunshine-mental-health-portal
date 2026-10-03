# Dependency Injection from Scratch

## 1. What is Dependency Injection?
Instead of a class creating its own dependencies using `new SpecificService()`, the system provides (injects) the service from the outside through constructor parameters.

## 2. Real-World Analogy
Instead of every new doctor buying their own MRI machine and carrying it to work, the hospital supplies the MRI machine to the doctor.

## 3. In `AppointmentController.cs`:
```csharp
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    // ASP.NET Core automatically supplies the service here!
    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }
}
```
