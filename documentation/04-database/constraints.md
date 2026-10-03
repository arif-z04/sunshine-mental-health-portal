# Database Constraints & Business Rule Validation

Constraints ensure invalid data is rejected at the database engine level.

---

## 1. Check Constraints in `sql/05_constraints.sql`

1. **Role Enumeration**:
   ```sql
   ALTER TABLE users ADD CONSTRAINT chk_users_role
   CHECK ("Role" IN ('PATIENT', 'DOCTOR', 'ADMIN'));
   ```
2. **Appointment Status Invariants**:
   ```sql
   ALTER TABLE appointments ADD CONSTRAINT chk_appointments_status
   CHECK ("Status" IN ('PENDING', 'CONFIRMED', 'IN_PROGRESS', 'COMPLETED', 'CANCELLED', 'REJECTED'));
   ```
3. **Monetary Non-Negativity & Currency Invariants**:
   ```sql
   ALTER TABLE doctors ADD CONSTRAINT chk_doctors_fee CHECK ("ConsultationFee" >= 0);
   ALTER TABLE payments ADD CONSTRAINT chk_payments_currency CHECK ("Currency" = 'BDT');
   ALTER TABLE payments ADD CONSTRAINT chk_payments_amount CHECK ("Amount" > 0);
   ```
4. **Bangladesh Phone Number Format**:
   ```sql
   ALTER TABLE users ADD CONSTRAINT chk_users_phone
   CHECK ("PhoneNumber" IS NULL OR "PhoneNumber" ~ '^\+?8801[3-9][0-9]{8}$');
   ```
