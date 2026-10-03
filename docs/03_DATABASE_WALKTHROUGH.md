# Sunshine Mental Health Counseling System
## 03: PostgreSQL Database Architecture & SQL Execution Walkthrough

This document explains the pure SQL-first database architecture for the **Sunshine Mental Health Counseling System (Bangladesh Edition)**.

In accordance with strict architectural requirements:
- **No EF Core Code-First auto-migrations** are used to create the schema.
- All database entities, relations, check constraints, and partial unique indexes are created using plain `.sql` scripts located in the `/sql/` directory.
- The database is created, seeded, and verified first; the ASP.NET Core Web API backend then maps directly to these existing tables using Database-First EF Core configuration.

---

## 1. Directory Structure of SQL Scripts

| File | Purpose | Execution User |
| :--- | :--- | :--- |
| [`sql/01_create_database.sql`](file:///home/noir/Desktop/Sunshine-project/sql/01_create_database.sql) | Creates `sunshine_user` role and `sunshine_db` database with proper encoding and schema permissions. | `postgres` superuser |
| [`sql/02_create_tables.sql`](file:///home/noir/Desktop/Sunshine-project/sql/02_create_tables.sql) | Creates all 15 relational tables, foreign keys, CHECK constraints, and partial unique indexes. | `sunshine_user` |
| [`sql/03_seed_dummy_data.sql`](file:///home/noir/Desktop/Sunshine-project/sql/03_seed_dummy_data.sql) | Inserts Bangladesh-specific seed data (BSMMU, DU, NIMH doctors, BDT pricing, bKash payments, psychoeducational resources, and ASP.NET Core Identity password hashes for `Password123!`). | `sunshine_user` |
| [`sql/04_test_queries.sql`](file:///home/noir/Desktop/Sunshine-project/sql/04_test_queries.sql) | Executes 7 verification sanity check queries (record audits, double-booking prevention, subscription date validity, revenue totals). | `sunshine_user` |
| [`sql/05_useful_queries.sql`](file:///home/noir/Desktop/Sunshine-project/sql/05_useful_queries.sql) | Provides production queries for daily clinic schedules, slot availability, MFS revenue breakdowns, and clinical histories. | `sunshine_user` |

---

## 2. Step-by-Step Manual Execution Pipeline

### Step 1: Create Database & Role
Connect to PostgreSQL container (port `5433` or host port `5432`) and run:
```bash
PGPASSWORD='postgres' psql -h localhost -p 5433 -U postgres -f sql/01_create_database.sql
```

### Step 2: Create Tables & Constraints
Run as the dedicated `sunshine_user`:
```bash
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/02_create_tables.sql
```

### Step 3: Seed Dummy Data for Bangladesh
Populate licensed clinicians, BDT subscription plans, resources, and demo accounts:
```bash
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/03_seed_dummy_data.sql
```

### Step 4: Run Verification Sanity Tests
Audit table record counts, fee tables, double-booking audit, and revenue:
```bash
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/04_test_queries.sql
```

### Step 5: Test Useful Operational Queries
Test clinic daily agenda, dynamic slot checker, and MFS aggregations:
```bash
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/05_useful_queries.sql
```

---

## 3. Schema Highlights & Business Rule Enforcement

### A. Double-Booking Prevention via Partial Unique Index
```sql
CREATE UNIQUE INDEX IF NOT EXISTS "IX_appointments_DoctorId_AppointmentDate_StartTime"
ON appointments ("DoctorId", "AppointmentDate", "StartTime")
WHERE "Status" != 'CANCELLED';
```
- Prevents two patients from reserving the same doctor at the same date and time slot simultaneously at the database engine level.
- Cancelled appointments do not block the slot because of the `WHERE "Status" != 'CANCELLED'` condition.

### B. Advance vs. Post-Payment Policy Constraint
```sql
ALTER TABLE doctors 
ADD CONSTRAINT chk_payment_policy CHECK ("PaymentPolicy" IN ('ADVANCE', 'POST_PAYMENT'));
```
- Clinicians configure whether patients must pay in advance to confirm the booking or can pay post-consultation.
- Advance bookings hold the slot in `PENDING` state with a 15-minute countdown (`BookingExpiresAt`).

### C. Mobile Financial Services (MFS) & BDT Payments
```sql
ALTER TABLE payments 
ADD CONSTRAINT chk_payment_method CHECK ("PaymentMethod" IN ('BKASH', 'NAGAD', 'ROCKET', 'CARD'));
```
- Stores transaction IDs (`TRX-BKS-...`, `TRX-NGD-...`, `TRX-RKT-...`) in Bangladeshi Taka (`BDT`).

### D. Subscription Date Validity Constraint
```sql
ALTER TABLE subscriptions 
ADD CONSTRAINT chk_subscription_dates CHECK ("StartDate" <= "EndDate");
```
- Guarantees subscription validity dates cannot be inverted.
