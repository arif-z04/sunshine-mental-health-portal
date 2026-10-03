# Role-Based Access Control (RBAC)

Enforcing authorization across system endpoints.

---

## 1. Application Roles

Sunshine defines three explicit roles:
1. **`PATIENT`**: Can browse doctors, view public resources, book/reschedule appointments, pay via bKash, and manage personal profile.
2. **`DOCTOR`**: Can view booked consultations, add clinical progress notes, update session status, and configure payment policy.
3. **`ADMIN`**: Can access the isolated admin portal, inspect financial revenue in BDT, approve/reject doctor credentials, manage users, and review audit logs.

---

## 2. Declarative Role Enforcement in Controllers

```csharp
// Accessible only to certified clinicians
[Authorize(Roles = AppRoles.Doctor)]
[HttpGet("appointments")]
public async Task<ActionResult<List<DoctorAppointmentDto>>> GetDoctorAppointments() { ... }

// Accessible only to administrators
[Authorize(Roles = AppRoles.Admin)]
[HttpGet("metrics")]
public async Task<ActionResult<AdminDashboardMetricsDto>> GetDashboardMetrics() { ... }
```
