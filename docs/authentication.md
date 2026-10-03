# Sunshine Mental Health Portal — Authentication & Identity

## 1. Authentication Architecture

The portal leverages **ASP.NET Core Identity** coupled with standard **JSON Web Tokens (JWT)**. Passwords are never stored in plaintext and are securely hashed using ASP.NET Core Identity's PBKDF2 with HMAC-SHA256 and high work-factor salting.

```text
Client (Web Browser)                       ASP.NET Core API
       |                                          |
       |--- POST /api/auth/login ---------------->|
       |    { email, password }                   |
       |                                          |-- Verify PBKDF2 PasswordHash
       |                                          |-- Check IsActive status
       |                                          |-- Generate Claims (Id, Email, Role)
       |                                          |-- Sign with HMAC-SHA256 (32+ byte secret)
       |<-- 200 OK { token, user } ---------------|
       |                                          |
       |--- GET /api/patient/appointments ------->|
       |    Header: "Authorization: Bearer JWT"   |
       |                                          |-- Validate Signature & Expiry
       |                                          |-- Inject ClaimsPrincipal into HttpContext
       |<-- 200 OK [ Appointments ] --------------|
```

---

## 2. Password Security & Policies

- Minimum length: 6 characters.
- Multi-factor ready: `TwoFactorEnabled` flag in schema.
- Lockout protection: `LockoutEnd`, `LockoutEnabled`, and `AccessFailedCount` columns tracked on the `users` table.
- Default demo credentials for local development: `Password123!`.

---

## 3. JWT Claims Payload

Issued JWT tokens contain standard RFC 7519 claims:
- `sub` / `NameIdentifier`: User ID integer.
- `email`: Normalized email string.
- `name`: User full legal name.
- `role`: Role identifier (`ADMIN`, `DOCTOR`, `PATIENT`).
- `PatientId` / `DoctorId`: Entity foreign key for fast profile lookup without redundant database queries.
- `exp`: Expiration timestamp (default: 7 days).
- `iss` & `aud`: Issuer (`Sunshine.Api`) and Audience (`Sunshine.Client`).
