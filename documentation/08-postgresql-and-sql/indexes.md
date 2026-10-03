# Indexes & Concurrency Protection

## 1. What is an Index?
Think of the index at the back of a 1,000-page medical textbook. Instead of reading all 1,000 pages to find "Schizophrenia", you look up the word in the alphabetical index, which tells you: *Page 482*.
An index in PostgreSQL speeds up queries from seconds to microseconds.

## 2. Partial Unique Index in Sunshine (`sql/04_indexes.sql`)
```sql
CREATE UNIQUE INDEX idx_unique_active_appointment
ON appointments ("DoctorId", "AppointmentDate", "StartTime")
WHERE "Status" != 'CANCELLED';
```
* **Why Partial?**: If an appointment is cancelled, the doctor should be able to accept another patient for that time slot. By adding `WHERE "Status" != 'CANCELLED'`, the database permits new bookings only after the previous one was cancelled!
