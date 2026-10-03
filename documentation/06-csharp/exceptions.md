# Exception Handling: `try` / `catch`

Preventing server crashes when unexpected errors occur:

---

```csharp
try
{
    var slot = await _appointmentService.BookAppointmentAsync(patientId, request);
    return Ok(slot);
}
catch (InvalidOperationException ex)
{
    // Slot conflict error
    return BadRequest(new { message = ex.Message });
}
catch (Exception ex)
{
    // Unexpected error
    _logger.LogError(ex, "Unexpected error booking appointment.");
    return StatusCode(500, new { message = "An internal error occurred." });
}
```
