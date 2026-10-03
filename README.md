# ☀️ Sunshine Mental Health Counseling System (Bangladesh Edition)

> A production-ready, secure telehealth web application and clinical practice portal tailored for Bangladesh. Built with **HTML5/CSS3/JavaScript (Material Design)**, **ASP.NET Core 10 (C# / EF Core)**, and **PostgreSQL 18** (`Npgsql.EntityFrameworkCore.PostgreSQL`).

---

## 🌟 Bangladesh System Features & Criteria

1. **Local Currency & Pricing (৳ BDT)**:
   - Clinical consultation fees: ৳1,200 to ৳1,800 BDT.
   - Subscription plans: Monthly (৳499), Quarterly (৳1,299), Yearly (৳3,999).
2. **Mobile Financial Services (MFS) & Payment Channels**:
   - Integrated sandbox simulation for **bKash (বিকাশ)**, **Nagad (নগদ)**, **Rocket (রকেট)**, and **Cards (Visa/Mastercard)**.
   - Test numbers: `01700000000` (Success), `01700000002` (Insufficient balance).
3. **Emergency Crisis Support in Bangladesh**:
   - Prominent alert banner on landing and patient portals:
     - **কান পেতে রই (Kaan Pete Roi)** emotional support helpline: `+8801779554391`
     - **জাতীয় জরুরি সেবা:** `999`
     - **স্বাস্থ্য বাতায়ন:** `16263`
     - **জাতীয় মানসিক স্বাস্থ্য ইনস্টিটিউট (NIMH):** Sher-e-Bangla Nagar, Dhaka.
4. **Multi-Portal Architecture (Material Design)**:
   - 🛡️ **Admin Portal (`/admin` & `/admin/login`)**: Isolated login route, user account management, doctor verification approval/rejection, appointment ledger, subscription plans, resource management, and immutable audit logs.
   - 🩺 **Doctor Portal (`/doctor`)**: BMDC credentials, payment policy toggle (`ADVANCE` vs. `POST_PAYMENT`), BST evening clinic scheduling (Sat–Thu), and clinical consultation notes.
   - 👤 **Patient Care Portal (`/patient`)**: Specialist discovery, BST slot booking, bKash/Nagad checkout, active appointment tracking, and self-help resource vault.
   - 🌐 **Public Landing (`/`)**: Hero section, Bangladesh crisis hotlines, how it works, therapist specialties, FAQ, and mobile drawer navigation.
5. **Double-Booking & Concurrency Protection**:
   - Application-level serializable transactions inside `AppointmentService.cs`.
   - PostgreSQL engine-level partial unique index on `appointments("DoctorId", "AppointmentDate", "StartTime") WHERE "Status" != 'CANCELLED'`.
6. **Immutable Audit Logging**:
   - Automated event capture across auth, admin status overrides, doctor verification, and appointment booking stored in `audit_logs`.

---

## 🏗️ Project Structure

```text
Another-Sunshine-Project/
├── Sunshine.slnx                # .NET 10 solution manifest
├── prompt.md                    # Master requirements specification
├── .env.example                 # Environment configuration template
├── test_e2e_full.sh             # Comprehensive end-to-end multi-portal verification suite
├── test_verification.sh         # Automated verification script
├── test_bangladesh.sh           # Bangladesh requirements verification script
├── test_double_booking.sh       # Concurrency and double-booking test script
├── client/                      # Modern React 19 Frontend Application (Vite 8 + Tailwind v4)
│   ├── src/
│   │   ├── api/                 # API Client (authApi, patientApi, doctorApi, adminApi)
│   │   ├── context/             # AuthContext (JWT session & RBAC)
│   │   ├── components/          # Modals (bKash/Nagad, Reschedule), DoctorCard, Banner, Navbar
│   │   ├── pages/               # LandingPage, PatientPortal, DoctorDashboard, AdminDashboard
│   │   └── utils/               # Bangladesh BST time, BDT currency, and hold countdown
│   ├── package.json             # React dependencies
│   └── vite.config.js           # Vite config with API proxy
├── documentation/               # Comprehensive beginner-to-developer docs & React migration guide
│   └── REACT-MIGRATION.md       # Full documentation of the React frontend migration
├── sql/                         # Database-First PostgreSQL SQL Pipeline
│   ├── 01_create_database.sql   # Creates sunshine_user and sunshine_db
│   ├── 02_extensions.sql        # Installs uuid-ossp and pgcrypto
│   ├── 03_schema.sql            # DDL for all 16 relational tables
│   ├── 04_indexes.sql           # Performance & partial unique double-booking indexes
│   ├── 05_constraints.sql       # Foreign keys, check constraints, BDT validations
│   ├── 06_seed_data.sql         # Seed clinicians (BSMMU, DU, NIMH), patients, resources
│   ├── 07_test_queries.sql      # Database sanity and integrity checks
│   └── README.md                # SQL execution documentation
├── docs/                        # Complete technical documentation suite
└── server/
    ├── Sunshine.App/            # Main ASP.NET Core 10 Web Application
    │   ├── Controllers/         # AuthController, PatientController, DoctorController, AdminController
    │   ├── Data/                # ApplicationDbContext (EF Core PostgreSQL mapping)
    │   ├── Models/              # Relational entity definitions (Entities.cs)
    │   ├── Services/            # Business logic: Appointment, Payment, Subscription, Audit, etc.
    │   └── Program.cs           # Application entrypoint, CORS & middleware pipeline
    └── Sunshine.Tests/          # Comprehensive Automated Test Suite (26 tests passing)
```

---

## 🔑 Pre-Configured Demo Accounts (Password: `Password123!`)

| Portal | Email | Role | Profile Details |
| :--- | :--- | :--- | :--- |
| **Admin Operations** | `admin@sunshine.org` | `ADMIN` | Prof. Dr. Farzana Rahman (Full system administration & bKash revenue oversight). |
| **Doctor Portal** | `dr.tanvir@sunshine.org` | `DOCTOR` | Dr. Tanvir Ahmed, MBBS, MD (BSMMU) • Advance policy (৳1,500 BDT). |
| **Doctor Portal** | `dr.nusrat@sunshine.org` | `DOCTOR` | Dr. Nusrat Jahan, MS (DU) • Advance policy (৳1,200 BDT). |
| **Doctor Portal** | `dr.rafiq@sunshine.org` | `DOCTOR` | Dr. K. M. Rafiqul Islam, FCPS (NIMH) • Post-Pay policy (৳1,800 BDT). |
| **Patient Portal** | `anika@example.com` | `PATIENT` | Anika Tabassum (Active Annual Pass via bKash, full vault access). |
| **Patient Portal** | `rahat@example.com` | `PATIENT` | Rahat Chowdhury (Patient with active appointment history). |
| **Patient Portal** | `sazzad@example.com` | `PATIENT` | Sazzad Hossain (Standard visitor, tests paywall and slot booking). |

---

## 🚀 Quick Start Guide

### Prerequisites
- .NET 10 SDK (`dotnet --version` >= 10.0.100)
- Node.js (v18+) & npm
- PostgreSQL 16+ (PostgreSQL 18 tested)
- Bash shell (Linux / macOS / WSL)

### 1. Database Setup
Execute the ordered SQL pipeline:
```bash
psql -U postgres -f sql/01_create_database.sql
psql -U sunshine_user -d sunshine_db -f sql/02_extensions.sql
psql -U sunshine_user -d sunshine_db -f sql/03_schema.sql
psql -U sunshine_user -d sunshine_db -f sql/04_indexes.sql
psql -U sunshine_user -d sunshine_db -f sql/05_constraints.sql
psql -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql
```

### 2. Run the Full Stack Application

**Backend (ASP.NET Core 10):**
```bash
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```

**Frontend (React 19 + Vite 8):**
```bash
cd client
npm install
npm run dev
```
Open **`http://localhost:5173`** in your browser. All API requests are automatically routed to the backend at port 5000.

Verify database integrity:
```bash
psql -U sunshine_user -d sunshine_db -f sql/07_test_queries.sql
```

### 2. Build the Application
```bash
dotnet build Sunshine.slnx
```

### 3. Run Automated Tests
```bash
dotnet test
```
*Executes all 26 automated tests across scheduling, concurrency, MFS payment processing, audit logging, and RBAC.*

Run automated integration and E2E test scripts against the running instance:
```bash
./test_e2e_full.sh        # Comprehensive end-to-end multi-portal verification suite
./test_verification.sh    # Core smoke and authorization checks
./test_bangladesh.sh      # Bangladesh localization, BDT pricing & MFS flows
./test_double_booking.sh  # Concurrency & double-booking prevention check
```

### 4. Start Local Development Server
```bash
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```
The application will be live at:
- **Public Website**: `http://localhost:5000/`
- **Patient Portal**: `http://localhost:5000/patient`
- **Doctor Portal**: `http://localhost:5000/doctor`
- **Admin Console**: `http://localhost:5000/admin` (or `/admin/login`)

---

## 📚 Technical Documentation Suite

Detailed guides are located in the [`docs/`](docs/) directory:
- [Architecture Guide](docs/architecture.md) — System boundaries, layered design, and client-server flow.
- [Database Architecture](docs/database.md) — Relational schema, PostgreSQL DDL, indexing, and normalization.
- [API Reference](docs/api.md) — Comprehensive RESTful endpoint catalog and request/response payloads.
- [Authentication](docs/authentication.md) — JWT issuance, cookie handling, PBKDF2 hashing, and session management.
- [Authorization](docs/authorization.md) — Role-based access control, claims-based authorization, and IDOR protection.
- [Testing Guide](docs/testing.md) — Unit testing, test fixtures, integration verification, and concurrency testing.
- [Deployment Guide](docs/deployment.md) — Production Linux host setup, systemd daemon, Nginx reverse proxy, and SSL.
- [Development Guide](docs/development-guide.md) — Local developer onboarding, styling conventions, and feature workflows.
- [Security Guide](docs/security.md) — Healthcare confidentiality, data protection, rate limiting, and audit logging.
