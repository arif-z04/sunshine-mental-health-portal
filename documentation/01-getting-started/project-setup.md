# Quick Project Setup (Step-by-Step)

Follow this 5-minute walkthrough to get Sunshine up and running from scratch.

---

## Step 1: Clone or Navigate to the Repository

```bash
cd /home/noir/Desktop/Another-Sunshine-Project
```

Ensure the root solution file `Sunshine.slnx` and database scripts in `sql/` are present:
```bash
ls -la
```

---

## Step 2: Initialize the PostgreSQL Database

Execute the canonical SQL migration pipeline in sequential order. Note that the password for `sunshine_user` is configured as `SunshinePass123!`:

```bash
# 1. Create database and user (requires postgres administrative access)
sudo -u postgres psql -f sql/01_create_database.sql

# 2. Install extensions (uuid-ossp, pgcrypto)
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/02_extensions.sql

# 3. Create 16 relational tables
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/03_schema.sql

# 4. Create performance and double-booking prevention indexes
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/04_indexes.sql

# 5. Apply database integrity and business check constraints
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/05_constraints.sql

# 6. Insert seed data (clinicians, demo patients, Bangladesh hotlines, and articles)
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql

# 7. Run integrity test queries
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/07_test_queries.sql
```

---

## Step 3: Build the .NET Solution

```bash
dotnet build Sunshine.slnx
```
This restores all NuGet packages (`Npgsql.EntityFrameworkCore.PostgreSQL`, `xUnit`, etc.) and compiles both `Sunshine.App` and `Sunshine.Tests`.

---

## Step 4: Run Automated Tests

Before launching the web server, run the automated test suite to ensure all business rules, MFS validations, and concurrency logic pass:

```bash
dotnet test
```
*Expected: 26 passed, 0 failed.*

---

## Step 5: Start the Web Application

Launch the server using Kestrel on port 5000:

```bash
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```

Once running, access the portals in your browser:
* **Landing Page**: `http://localhost:5000/`
* **Patient Care Portal**: `http://localhost:5000/patient`
* **Doctor Practice Portal**: `http://localhost:5000/doctor`
* **Admin Operations Console**: `http://localhost:5000/admin` (Isolated login: `http://localhost:5000/admin/login`)

---

## Step 6: Verify with Automated Verification Scripts

Open another terminal window in the project root and run:
```bash
./test_e2e_full.sh
```
This executes end-to-end HTTP requests verifying all authentication, appointment booking, rescheduling, bKash checkout, paywall access, and admin metrics workflows.
