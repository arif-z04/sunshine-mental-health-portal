# Service Layer Architecture

Business logic in Sunshine is isolated inside domain services rather than being placed directly in controllers.

---

## 1. Why Use Services?
* **Clean Code**: Controllers remain small (10-20 lines per action).
* **Transaction Management**: Database transactions spanning multiple queries (booking + slot checking + audit log) are coordinated inside services.
* **Reusability**: An appointment can be booked from the web portal, an automated script, or a mobile client without duplicating scheduling rules.

---

## 2. Core Services in Sunshine
* **`AppointmentService`**: Calculates 45-minute BST consultation slots, executes serializable transactions to prevent double-booking, and handles appointment rescheduling.
* **`AuthService`**: Hashes passwords with PBKDF2, signs JWT claims, and authenticates credentials.
* **`PaymentService`**: Simulates bKash/Nagad transactions and updates appointment/subscription status.
* **`AuditService`**: Appends immutable records to the `audit_logs` table.
