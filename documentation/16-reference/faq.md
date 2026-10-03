# Frequently Asked Questions (FAQ)

Answers to common developer questions:

---

### Q1: Why does Sunshine use HTML5/CSS3/JS instead of React or Angular?
**A**: To eliminate build dependencies (`node_modules`), ensure instant page rendering on mobile networks across Bangladesh, and allow any beginner developer to modify the system with standard web fundamentals.

### Q2: How is double-booking prevented if two users book at the same second?
**A**: Sunshine uses a two-tier defense: (1) an EF Core serializable database transaction in `AppointmentService.cs` and (2) a PostgreSQL partial unique index on `appointments("DoctorId", "AppointmentDate", "StartTime") WHERE "Status" != 'CANCELLED'`.

### Q3: Why does `sudo -u postgres psql` work, but `psql -U sunshine_user` fails?
**A**: The `postgres` administrative user uses Linux `peer` authentication (matches your terminal user), whereas `sunshine_user` requires the password `SunshinePass123!`. Prefix the command with `PGPASSWORD='SunshinePass123!'`.

### Q4: How do I test the MFS bKash payment sandbox?
**A**: When booking an appointment or subscription, enter any 11-digit mobile number starting with `017` (e.g. `01700000000`) and PIN `12345`. The system generates a realistic transaction ID (`TRX-BKS-...`) and completes the booking immediately.
