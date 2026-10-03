# Controllers: Handling HTTP Requests

A **Controller** is a C# class inheriting from `ControllerBase` that groups related API endpoints.

---

In `PatientController.cs`:
```csharp
[ApiController]
[Route("api/patient")]
[Authorize(Roles = AppRoles.Patient)]
public class PatientController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public PatientController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("doctors")]
    [AllowAnonymous] // Anyone can view the doctor directory
    public async Task<ActionResult<List<DoctorDto>>> GetDoctors()
    {
        var doctors = await _appointmentService.GetActiveDoctorsAsync();
        return Ok(doctors);
    }
}
```
