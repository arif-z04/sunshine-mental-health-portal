# Database Constraints: The Rules of the Road

Constraints guarantee that invalid data can never enter the database, even if someone runs raw SQL commands.

---

From `sql/05_constraints.sql`:
1. **Role Constraint**:
   ```sql
   CHECK ("Role" IN ('PATIENT', 'DOCTOR', 'ADMIN'))
   ```
2. **Currency Invariant**:
   ```sql
   CHECK ("Currency" = 'BDT')
   ```
3. **Non-Negative Fees**:
   ```sql
   CHECK ("ConsultationFee" >= 0)
   ```
4. **Bangladesh Mobile Phone Regex**:
   ```sql
   CHECK ("PhoneNumber" IS NULL OR "PhoneNumber" ~ '^\+?8801[3-9][0-9]{8}$')
   ```
