# Sunshine Mental Health Portal — SQL Database Scripts

This directory contains the production-ready PostgreSQL SQL scripts for the **Sunshine Mental Health Portal (Bangladesh)**.

The database is built **Database-First** with raw PostgreSQL scripts, complete with relational normalization, partial unique indexes for concurrency safety, domain check constraints, and authentic Bangladesh seed data in BDT (৳).

---

## Script Execution Order

Execute the scripts sequentially using `psql`:

```bash
# 1. Database and Role Creation (Run as superuser 'postgres')
PGPASSWORD='postgres' psql -h localhost -p 5432 -U postgres -f sql/01_create_database.sql

# 2. Enable PostgreSQL Extensions (uuid-ossp, pgcrypto)
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/02_extensions.sql

# 3. Create Relational Tables & Schema
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/03_schema.sql

# 4. Create Indexes & Critical Partial Unique Concurrency Constraints
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/04_indexes.sql

# 5. Apply Foreign Key & Domain Check Constraints
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/05_constraints.sql

# 6. Insert Realistic Bangladesh Seed Data (Doctors, Patients, Plans in BDT, Resources)
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql

# 7. Execute Verification & Sanity Check Test Queries
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/07_test_queries.sql
```

---

## File Overview

| Script | Purpose |
| :--- | :--- |
| `01_create_database.sql` | Configures `sunshine_user` login role, creates `sunshine_db` with UTF-8 encoding, and sets schema privileges. |
| `02_extensions.sql` | Enables PostgreSQL cryptographic and UUID extension modules. |
| `03_schema.sql` | Defines tables for ASP.NET Identity, patients, doctors, weekly recurring schedules, appointments, subscriptions, payments, resources, notifications, and audit logs. |
| `04_indexes.sql` | Creates performance indexes and the critical partial unique index on `appointments("DoctorId", "AppointmentDate", "StartTime") WHERE "Status" != 'CANCELLED'` to eliminate double-booking at the database engine level. |
| `05_constraints.sql` | Enforces referential integrity with cascading rules and domain `CHECK` constraints on fees, currencies (BDT), payment methods, and statuses. |
| `06_seed_data.sql` | Realistic Bangladesh data with certified clinicians (BSMMU, DU, NIMH), BDT membership plans, and mental wellness CBT resources. |
| `07_test_queries.sql` | Diagnostic health checks, appointment ledgers, revenue aggregations, and double-booking conflict verification queries. |
