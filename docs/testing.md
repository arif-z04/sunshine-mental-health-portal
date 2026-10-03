# Sunshine Mental Health Portal — Testing Strategy & Verification

Testing is a first-class requirement. The project includes automated xUnit unit/integration tests, database sanity test queries, bash integration verification scripts, and concurrency stress tests.

---

## 1. Automated Test Suites (`server/Sunshine.Tests`)

The test project is built with **xUnit** and **Microsoft.EntityFrameworkCore.InMemory**:

| Test Class | Focus Areas | Tests | Status |
| :--- | :--- | :--- | :--- |
| `AppointmentSchedulingTests` | 45-min slot generation, advance payment 15m expiration, duplicate booking rejection, auto-cancellation, slot rescheduling and conflict validation | 6 | ✅ Passed |
| `PaymentAndSubscriptionTests` | bKash sandbox payment, plan activation, free resource access, premium paywall enforcement | 3 | ✅ Passed |
| `AuthenticationAndRbacTests` | JWT signature and expiration, role claims, non-admin escalation rejection | 4 | ✅ Passed |
| `BangladeshRequirementsTests` | Mobile operator regex (+8801[3-9]...), Asia/Dhaka UTC+6 offset, BDT formatting (৳) | 12 | ✅ Passed |
| `AuditLoggingTests` | Admin operational trail, actor ID/email tracking, IP recording | 1 | ✅ Passed |

**Total Automated Tests:** 26 Passed, 0 Failed, 0 Skipped.

### Execution Command:
```bash
dotnet test
```

---

## 2. Bash Verification & E2E Scripts

Automated bash scripts are provided at the root of the repository:

1. **`test_e2e_full.sh`** *(Comprehensive Master Suite)*:
   - Verifies all portal interfaces (Public Landing, Patient Portal, Clinician Portal, Admin Console, and Admin Login).
   - Tests patient registration, profile retrieval, and profile updates.
   - Tests specialist directory and dynamic slot generation.
   - Tests appointment advance booking and bKash sandbox transaction resolution.
   - Tests appointment rescheduling and conflict protection.
   - Tests strict concurrency double-booking prevention.
   - Tests digital vault and subscription paywall gating.
   - Tests clinician consultation flow and completion with clinical notes.
   - Tests admin login gate (rejects non-admin HTTP 403), live BDT revenue metrics, and immutable audit logs.

2. **`test_verification.sh`**:
   - Verifies static frontend portal routes (`/`, `/admin/login`, `/doctor`, `/patient`).
   - Verifies public doctor directory endpoint.
   - Tests valid Admin login and verifies that a non-admin gets HTTP `403 Forbidden`.
   - Tests patient login and paywall verification (subscribed user allowed, free user denied with HTTP 401).
   - Tests dynamic doctor slot generation.

3. **`test_bangladesh.sh`**:
   - Tests doctor directory in Bangladesh.
   - Tests subscription plans in BDT (৳).
   - Simulates patient Anika booking a session with Dr. Tanvir Ahmed for tomorrow at 16:00 BST.
   - Simulates authorizing a ৳1,500 BDT payment via bKash sandbox (`TRX-BKS-...`).

4. **`test_double_booking.sh`**:
   - Tests concurrency and double-booking protection.
   - Patient 1 (Anika) books slot `17:30:00` with Dr. Tanvir.
   - Patient 2 (Rahat) simultaneously attempts to book the identical slot.
   - Proves that Attempt 2 is strictly rejected by the server with error `"This time slot has already been booked"`.

### Execution Commands:
```bash
bash test_e2e_full.sh
bash test_verification.sh
bash test_bangladesh.sh
bash test_double_booking.sh
```

---

## 3. Database Integrity Tests (`sql/07_test_queries.sql`)

Validates table row counts, foreign key joins, active clinical hours, appointment ledgers, double-booking partial unique indexes, total revenue in BDT, and audit logs.
```bash
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/07_test_queries.sql
```
