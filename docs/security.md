# Security Architecture & Data Protection

This document outlines the security controls, data confidentiality safeguards, access models, and threat mitigations implemented in the Sunshine Mental Health Portal.

---

## 1. Authentication & Identity Management

### 1.1 Password Hashing & Key Derivation
Passwords are never stored in plaintext. Passwords use **PBKDF2 (Password-Based Key Derivation Function 2)** with HMAC-SHA256:
- **Salt**: 128-bit cryptographically secure pseudorandom salt generated per user via `RandomNumberGenerator`.
- **Iteration Count**: 100,000 iterations minimum.
- **Output Key**: 256-bit derived key.
- **Storage Format**: `pbkdf2$iterations$saltBase64$hashBase64` stored in `"users"."PasswordHash"`.

### 1.2 Session Token & JWT Security
- Authentication relies on signed JSON Web Tokens (JWT) using `HMAC-SHA256` (`HS256`).
- **Secret Key**: Min 256-bit symmetric key configured via environment variable `JWT_SECRET` (fallback in `appsettings.json` restricted to development).
- **Token Lifetime**: Configured to 2 hours for user safety, preventing lingering sessions on public/shared devices.
- **Claims Stored**: Minimal claims:
  - `sub` (User ID / UUID)
  - `email` (User email)
  - `role` (`PATIENT`, `DOCTOR`, `ADMIN`)
  - `jti` (Unique Token ID)
- Sensitive medical records, phone numbers, and full clinical histories are **never** embedded in JWT payloads.

---

## 2. Role-Based Access Control (RBAC) & Authorization

Access is governed strictly by ASP.NET Core Policy and Role Authorization:

| Role | Permitted Actions | Restricted Actions |
| :--- | :--- | :--- |
| **PATIENT** | Book appointments, view personal appointment history, view personal health records, self-assessment tests, purchase subscription via bKash/Nagad/Rocket. | Cannot view other patients' appointments, cannot view or modify clinical notes written by doctors, cannot access admin panel. |
| **DOCTOR** | View appointments booked with themselves, write clinical session notes for their assigned patients, toggle availability slots. | Cannot access other doctors' clinical notes or appointments, cannot modify subscription billing tiers, cannot grant admin rights. |
| **ADMIN** | Approve/reject doctor clinical verifications, manage subscription tiers, monitor platform metrics, inspect immutable audit logs, deactivate malicious accounts. | Cannot read private doctor-patient clinical consultation notes without explicit medical oversight reason (logged in audit trail). |

---

## 3. IDOR (Insecure Direct Object Reference) Prevention

All endpoints handling patient records, appointments, and prescriptions enforce ownership validation at the service level:
- In `AppointmentController` and `AppointmentService`, any request to retrieve or update `/api/appointments/{id}` verifies that:
  ```csharp
  if (userRole == "PATIENT" && appointment.PatientId != currentUserId)
  {
      return Forbid();
  }
  if (userRole == "DOCTOR" && appointment.DoctorId != currentDoctorId)
  {
      return Forbid();
  }
  ```
- No client-supplied ID is trusted blindly. All queries join against the authenticated user's claim ID.

---

## 4. Concurrency & Double-Booking Protection

Double-booking poses severe clinical and financial risks. Sunshine implements defense-in-depth:
1. **Application-Level Concurrency Check**:
   Within `AppointmentService.cs`, slot availability is checked inside a serializable/repeatable read database transaction:
   ```csharp
   var isBooked = await _db.Appointments.AnyAsync(a => 
       a.DoctorId == doctorId && 
       a.AppointmentDate == date && 
       a.StartTime == startTime && 
       a.Status != "CANCELLED");
   ```
2. **Database Engine Enforcement**:
   A partial unique index at the PostgreSQL storage engine level physically prevents overlapping bookings:
   ```sql
   CREATE UNIQUE INDEX "IX_appointments_DoctorId_AppointmentDate_StartTime"
   ON "appointments" ("DoctorId", "AppointmentDate", "StartTime")
   WHERE "Status" != 'CANCELLED';
   ```
   Even if two concurrent requests bypass application logic simultaneously, PostgreSQL raises a unique constraint violation (`23505`), preventing duplicate records.

---

## 5. Medical Data Confidentiality & Audit Logging

Mental health records in Bangladesh are sensitive due to societal stigma. The platform enforces strict medical confidentiality:
- **Audit Trails**: Every critical event (Logins, Failed Logins, Appointment Status Transitions, Clinical Verification Changes, Subscription Activations) is written to the immutable `audit_logs` table.
- **Audit Columns**:
  - `UserId`, `Action`, `EntityName`, `EntityId`, `OldValues`, `NewValues`, `IpAddress`, `UserAgent`, `Timestamp`.
- **Data Minimization**:
  - Payment credentials (bKash/Nagad PINs or OTPs) are never captured, logged, or stored.
  - Emergency hotline interactions connect directly via tel URIs or local device dialers without storing intermediary metadata.

---

## 6. Network & Web Application Security

- **CORS Configuration**: Restricts origins in production to designated frontend hosts.
- **SQL Injection Prevention**: 100% of database access is mediated through EF Core parameterized queries and PostgreSQL type-safe binders. Zero raw string concatenation in SQL queries.
- **Cross-Site Scripting (XSS)**: All frontend renderings escape dynamic user inputs via `textContent` or sanitized DOM node creation.
- **Input Validation**: ASP.NET Core Fluent/DataAnnotation model validation checks telephone numbers against Bangladeshi format (`+8801[3-9]\d{8}` or `01[3-9]\d{8}`), monetary values (`Amount >= 0`), and dates (`AppointmentDate >= today`).
