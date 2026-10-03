# Controllers & API Design

Controllers handle incoming HTTP verbs (`GET`, `POST`, `PUT`, `DELETE`), parse request payloads, and return JSON responses.

---

## 1. Controller Example: `PatientController.cs`

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

    [HttpPost("appointments")]
    public async Task<ActionResult<AppointmentDto>> BookAppointment([FromBody] BookAppointmentRequest request)
    {
        var patientId = GetCurrentPatientId();
        var result = await _appointmentService.BookAppointmentAsync(patientId, request);
        return Ok(result);
    }
}
```

* `[ApiController]`: Enables automatic model validation and returns HTTP 400 with details if request fields are missing.
* `[Authorize(Roles = AppRoles.Patient)]`: Rejects requests from non-patients with HTTP 401/403.
