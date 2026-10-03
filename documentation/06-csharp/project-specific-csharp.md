# C# 13 Features Used in Sunshine

Sunshine takes advantage of modern C# idioms:

---

1. **Records**: Lightweight immutable data structures used for DTOs:
   ```csharp
   public record BookAppointmentRequest(int DoctorId, DateOnly AppointmentDate, TimeOnly StartTime);
   ```
2. **Top-Level Statements**: Streamlined `Program.cs` without boilerplate `namespace` or `class Program` wrappers.
3. **Nullable Reference Types (`string?`)**: Compiler warns you if a variable might be null, preventing infamous `NullReferenceException` crashes.
