# Application Logging & Audit Logs

Sunshine uses two complementary logging mechanisms:

---

## 1. Console / Systemd Application Logging
Built-in ASP.NET Core logging records runtime diagnostic events:
```csharp
_logger.LogInformation("Appointment {Id} confirmed for Patient {PatientId}", apt.Id, apt.PatientId);
```
On Omarchy, these logs are captured directly by `systemd` and can be inspected via `journalctl`.

---

## 2. Relational Security Audit Trail (`audit_logs`)
High-value clinical, financial, and administrative operations are recorded in the PostgreSQL database table `audit_logs`:
* Admin logins.
* Doctor BMDC credential approvals or rejections.
* User account suspensions (`IsActive` toggles).
* Appointment status transitions.
Each record includes: `ActorId`, `ActorEmail`, `Action`, `TargetType`, `TargetId`, `IpAddress`, `Details`, and `CreatedAt`.
