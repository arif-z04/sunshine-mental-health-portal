# Entity Framework Core Error Guide

---

## 1. Concurrency Check Failure
* **Cause**: Attempting to book a slot that has already been reserved.
* **Fix**: Expected system behavior. Guide the user to select an alternate slot.

## 2. Invalid Cast Exception
* **Cause**: Data type mismatch between C# model and PostgreSQL column (e.g. `DateTime` vs `DateOnly`).
* **Fix**: Ensure `AppointmentDate` is typed as `DateOnly` and `StartTime` as `TimeOnly`.
