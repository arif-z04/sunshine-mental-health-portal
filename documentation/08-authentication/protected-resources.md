# Protecting Clinical Data & IDOR Prevention

Healthcare information requires strict authorization checks to prevent Insecure Direct Object Reference (IDOR) vulnerabilities.

---

## 1. What is an IDOR Vulnerability?

An IDOR vulnerability occurs if Patient A can view Patient B's medical history simply by changing the ID in the URL:
`GET /api/patient/appointments/42`

---

## 2. How Sunshine Mitigates IDOR

In `PatientController.cs`, endpoints **never rely blindly on client-supplied user IDs**:
```csharp
[HttpGet("appointments/{id:int}")]
public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
{
    // 1. Extract authenticated patient ID securely from JWT claims
    var patientId = GetCurrentPatientId();

    // 2. Query appointment matching BOTH the appointment ID AND the patient ID
    var apt = await _appointmentService.GetAppointmentByIdAsync(id, patientId);
    if (apt == null)
    {
        return NotFound(new { message = "Appointment not found or access denied." });
    }

    return Ok(apt);
}
```
If Patient A queries an appointment belonging to Patient B, the service returns `404 Not Found`, completely blocking unauthorized clinical record access.
