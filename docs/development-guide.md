# Sunshine Mental Health Portal — Developer Onboarding Guide

## 1. Quick Start

### Step 1: Clone & Verify Tooling
Ensure .NET 10 SDK and PostgreSQL 15+ are installed:
```bash
dotnet --version    # Expected: 10.0.x
psql --version      # Expected: 15.x - 18.x
```

### Step 2: Initialize Database
```bash
# Execute pure SQL pipeline
PGPASSWORD='postgres' psql -h localhost -p 5432 -U postgres -f sql/01_create_database.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/02_extensions.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/03_schema.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/04_indexes.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/05_constraints.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/07_test_queries.sql
```

### Step 3: Run the Application
```bash
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```
Access the application at:
- **Public Homepage**: [http://localhost:5000/](http://localhost:5000/)
- **Patient Portal**: [http://localhost:5000/patient](http://localhost:5000/patient)
- **Doctor Portal**: [http://localhost:5000/doctor](http://localhost:5000/doctor)
- **Admin Portal**: [http://localhost:5000/admin](http://localhost:5000/admin) (Login at `/admin/login`)

---

## 2. Pre-Configured Demo Accounts (Password: `Password123!`)

| Role | Email | Name & Bio |
| :--- | :--- | :--- |
| **Super Admin** | `admin@sunshine.org` | Prof. Dr. Farzana Rahman (Full system access) |
| **Doctor (Advance)** | `dr.tanvir@sunshine.org` | Dr. Tanvir Ahmed, MBBS, MD (BSMMU) (৳1,500 BDT) |
| **Doctor (Advance)** | `dr.nusrat@sunshine.org` | Nusrat Jahan, MS (University of Dhaka) (৳1,200 BDT) |
| **Doctor (Post-Pay)** | `dr.rafiq@sunshine.org` | Dr. K. M. Rafiqul Islam, FCPS (NIMH) (৳1,800 BDT) |
| **Patient (Active Sub)**| `anika@example.com` | Anika Tabassum (Annual Pass active, full library access) |
| **Patient (Free)** | `rahat@example.com` | Rahat Chowdhury (BCS candidate, test booking) |
| **Patient (Free)** | `sazzad@example.com` | Sazzad Hossain (Free tier, test paywall) |

---

## 3. Running Automated Tests

```bash
# Run unit and integration test suite
dotnet test

# Run shell verification tests
bash test_verification.sh
bash test_bangladesh.sh
bash test_double_booking.sh
```

---

## 4. How to Add a New Feature

1. **Database Change**: Add the SQL migration statement to `sql/` and document in `sql/README.md`.
2. **Entity Model**: Update `server/Sunshine.App/Models/Entities.cs` and map in `ApplicationDbContext.cs`.
3. **DTOs**: Add request and response records in `server/Sunshine.App/DTOs/DTOs.cs`.
4. **Service Logic**: Implement domain logic in `server/Sunshine.App/Services/`.
5. **Controller API**: Expose REST endpoints with appropriate `[Authorize(Roles = ...)]` attributes.
6. **Tests**: Add unit and integration tests in `server/Sunshine.Tests/`.
7. **Frontend Integration**: Connect API in `wwwroot/` with accessible HTML5, responsive CSS, and error handling.
