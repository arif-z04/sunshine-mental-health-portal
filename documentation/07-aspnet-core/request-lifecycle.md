# The Complete ASP.NET Core Request Lifecycle

Step-by-step path of a single HTTP request:

```text
1. Client Browser sends HTTP POST /api/patient/appointments
                           │
                           ▼
2. Kestrel Web Server accepts TCP socket connection
                           │
                           ▼
3. StaticFiles Middleware checks if file exists in wwwroot (No -> passes through)
                           │
                           ▼
4. Authentication Middleware checks sunshine_token cookie / Bearer JWT
                           │
                           ▼
5. Authorization Middleware checks [Authorize(Roles = "PATIENT")] (Passes!)
                           │
                           ▼
6. Model Binding parses JSON into C# BookAppointmentRequest object
                           │
                           ▼
7. Data Annotations validate fields (All valid!)
                           │
                           ▼
8. PatientController calls AppointmentService.BookAppointmentAsync()
                           │
                           ▼
9. EF Core opens Serializable Transaction and queries PostgreSQL
                           │
                           ▼
10. Controller wraps result into HTTP 200 OK JSON response
                           │
                           ▼
11. Browser receives response and updates booking interface
```
