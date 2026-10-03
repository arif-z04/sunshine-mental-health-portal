# Appointment Booking & Rescheduling Lifecycle

The appointment booking engine is a core component of the Sunshine platform.

---

## 1. The Booking Lifecycle

```text
[ Patient Selects Slot ]
          │
          ▼
[ POST /api/patient/appointments ]
          │
          ▼
[ Serializable Concurrency Check ] ---> Conflict detected? ---> Return HTTP 400
          │
          ▼
[ Create Appointment (Status = PENDING) ]
          │
          ├── Doctor Policy = ADVANCE? ---> Start 15-Minute Countdown Hold
          │                                  Patient pays via bKash ---> Status = CONFIRMED
          │                                  Timer expires without payment ---> Status = CANCELLED
          │
          └── Doctor Policy = POST_PAYMENT? -> Status = CONFIRMED immediately
```

---

## 2. Rescheduling Flow
Patients can reschedule upcoming appointments via `POST /api/patient/appointments/{id}/reschedule`:
1. Patient chooses new Date and StartTime.
2. System checks if the new slot is available under a database transaction.
3. Appointment record is updated to the new date and time.
4. Status is preserved or set to `CONFIRMED`.
