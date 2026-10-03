# Dynamic Slot Engine & Bangladesh Standard Time (BST)

Calculating available consultation intervals in Asia/Dhaka time.

---

## 1. BST Evening Clinic Configuration
* Standard clinical hours: **16:00:00 to 20:30:00 BST**.
* Slot duration: **45 minutes**.
* Intervals:
  - 16:00 to 16:45
  - 16:45 to 17:30
  - 17:30 to 18:15
  - 18:15 to 19:00
  - 19:00 to 19:45
  - 19:45 to 20:30

---

## 2. Dynamic Calculation in `AppointmentService.cs`
The engine retrieves:
1. The doctor's working hours for the chosen day of the week.
2. All existing appointments for that doctor on that date where `Status != 'CANCELLED'`.
3. Iterates in 45-minute steps and marks `isBooked = true` for slots that conflict with existing records.
