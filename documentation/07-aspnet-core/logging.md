# Application & Diagnostic Logging

Capturing runtime diagnostic data:

---

```csharp
_logger.LogInformation("Patient {PatientId} booked Appointment {AptId} with Doctor {DocId}", 
    patientId, appointment.Id, request.DoctorId);
```
* In development, these messages print directly to your terminal.
* In production, systemd captures them and writes them to the journal log (`journalctl`).
