# Indexing Strategy & Concurrency Protection

Database indexes dramatically speed up queries and enforce business invariants.

---

## 1. Foreign Key Performance Indexes

Foreign key columns are indexed in `sql/04_indexes.sql` to avoid full-table sequential scans during JOIN operations:
```sql
CREATE INDEX idx_patients_user_id ON patients("UserId");
CREATE INDEX idx_doctors_user_id ON doctors("UserId");
CREATE INDEX idx_appointments_doctor_date ON appointments("DoctorId", "AppointmentDate");
CREATE INDEX idx_appointments_patient_id ON appointments("PatientId");
CREATE INDEX idx_payments_user_id ON payments("UserId");
CREATE INDEX idx_audit_logs_created_at ON audit_logs("CreatedAt");
```

---

## 2. The Double-Booking Partial Unique Index

A vital requirement of telehealth scheduling is that **no doctor can ever have two simultaneous active appointments**:

```sql
CREATE UNIQUE INDEX idx_unique_active_appointment
ON appointments ("DoctorId", "AppointmentDate", "StartTime")
WHERE "Status" != 'CANCELLED';
```

### Why a "Partial" Unique Index?
* If patient A books 16:00 on October 22nd, then cancels it (`Status = 'CANCELLED'`), patient B **must be allowed** to book that slot.
* A standard unique index on `(DoctorId, Date, StartTime)` would permanently block that time slot even after cancellation.
* The `WHERE "Status" != 'CANCELLED'` clause ensures uniqueness is enforced **only across active or pending bookings**.
