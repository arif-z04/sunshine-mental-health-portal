# Integration Testing

Integration tests verify that multiple components (e.g. Services, DbContext, and DTOs) work together seamlessly.

---

## 1. Test Suite Breakdown

* **`AppointmentSchedulingTests.cs`** (6 tests):
  - 45-minute slot generation between 16:00 and 20:30 BST.
  - Auto-cancellation after 15-minute countdown for unpaid advance bookings.
  - Slot conflict detection preventing overlapping sessions.
  - Appointment rescheduling to open slots and rejection when targeted slot is occupied.
* **`PaymentAndSubscriptionTests.cs`** (3 tests):
  - bKash payment simulation and status transition to `CONFIRMED`.
  - Subscription tier activation.
  - Digital vault paywall enforcement (open access for free items, 401 for premium items).
* **`BangladeshRequirementsTests.cs`** (12 tests):
  - Regex validation for Bangladesh phone numbers (`+8801[3-9]...`).
  - Asia/Dhaka (+06:00) timezone calculations.
  - BDT currency symbol formatting (`৳`).
* **`AuthenticationAndRbacTests.cs`** (4 tests):
  - JWT claim validation.
  - PBKDF2 password verification.
  - Rejection of non-admin users attempting to access admin controllers.
* **`AuditLoggingTests.cs`** (1 test):
  - Recording of actor IDs, IPs, and administrative events in `audit_logs`.
