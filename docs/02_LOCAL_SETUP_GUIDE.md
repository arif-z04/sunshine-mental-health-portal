# Sunshine Mental Health Counseling System
## 02: Local Setup & Step-by-Step Execution Guide (Bangladesh Edition)

This guide walks you through setting up and running the Sunshine Mental Health Counseling System locally with Bangladesh criteria.

---

## 1. Database Setup (Pure SQL First)

The database schema and seed data are created **entirely with plain SQL scripts** (no EF Core Code-First auto-migrations):

```bash
# 1. Create database and user role (run as postgres superuser)
PGPASSWORD='postgres' psql -h localhost -p 5433 -U postgres -f sql/01_create_database.sql

# 2. Create tables, constraints, and partial unique indexes
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/02_create_tables.sql

# 3. Insert Bangladesh dummy seed data (clinicians, BDT plans, resources, users)
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/03_seed_dummy_data.sql

# 4. Run test sanity check queries
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/04_test_queries.sql

# 5. Run useful production/reporting queries
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5433 -U sunshine_user -d sunshine_db -f sql/05_useful_queries.sql
```

---

## 2. Launching the Backend

Once the database is created and verified:

```bash
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```

The ASP.NET Core Web API maps directly to the pre-existing PostgreSQL schema via Entity Framework Core in Database-First style, serving both the REST APIs and static frontend portals on port 5000.

Open your browser and navigate to:
👉 **`http://localhost:5000`**

---

## 3. Dedicated Portals & Routing

| Portal | Route | Primary Use Case |
| :--- | :--- | :--- |
| **Public Homepage** | `http://localhost:5000/` | Browse therapists, filter by clinical modality, preview CBT self-help resources, crisis banner with **Kaan Pete Roi (+8801779554391)**, **999**, and **16263**. |
| **Admin Portal** | `http://localhost:5000/admin` | Isolated login at `/admin/login`. Real-time KPI cards in ৳ BDT, BMDC doctor verification, universal appointment ledger in BST, subscription plan CRUD, resource manager, bKash / Nagad transaction monitor & refunds. |
| **Doctor / Clinician Portal** | `http://localhost:5000/doctor` | Dedicated login with 1-click test clinicians. Profile editor, **Instant Payment Policy Switch** (`ADVANCE` vs. `POST_PAYMENT`), weekly recurring schedule configuration (BST), appointment desk with clinical notes. |
| **Patient Care Portal** | `http://localhost:5000/patient` | Patient registration & login. Search therapists, dynamic date/time slot booking with double-booking prevention, membership checkout with **bKash / Nagad / Rocket / Card sandbox payment modal**, and server-paywalled digital resource library. |

---

## 4. Pre-Configured Demo Accounts (Password: `Password123!`)

| Role | Email | Name & Details |
| :--- | :--- | :--- |
| **Super Admin** | `admin@sunshine.org` | Prof. Dr. Farzana Rahman (Full administrative control) |
| **Doctor (Advance Pay)** | `dr.tanvir@sunshine.org` | Dr. Tanvir Ahmed, MBBS, MD (BSMMU) (৳1,500 BDT, Advance policy) |
| **Doctor (Post Pay)** | `dr.rafiq@sunshine.org` | Dr. K. M. Rafiqul Islam, FCPS (NIMH) (৳1,800 BDT, Post-Payment policy) |
| **Doctor (DU CBT)** | `dr.nusrat@sunshine.org` | Nusrat Jahan, MS (University of Dhaka) (৳1,200 BDT, Advance policy) |
| **Patient (Subscribed)** | `anika@example.com` | Anika Tabassum (Holds active Annual Pass via bKash, access to all premium books) |
| **Patient (Free Tier)** | `rahat@example.com` | Rahat Chowdhury (Patient account for booking appointments and testing bKash payments) |
| **Patient (Free Tier)** | `sazzad@example.com` | Sazzad Hossain (Free account, tests paywall and slot booking) |
