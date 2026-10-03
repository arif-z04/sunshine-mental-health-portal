# Interfaces: Contracts for Code

## 1. What is an Interface?
An **interface** defines a list of method signatures that a class promises to implement, without providing the actual code inside those methods.

## 2. Why Use Interfaces?
In `server/Sunshine.App/Services/IAppointmentService.cs`:
```csharp
public interface IAppointmentService
{
    Task<List<TimeSlotDto>> GetAvailableSlotsAsync(int doctorId, DateOnly date);
    Task<AppointmentDto> BookAppointmentAsync(int patientId, BookAppointmentRequest request);
}
```
* The controller only cares that `IAppointmentService` *can* book an appointment; it doesn't care whether the appointment is saved to PostgreSQL, an in-memory test database, or a cloud service.
* This allows us to swap real services for mock test services easily!
