# Sunshine Mental Health Portal — React Migration & Architecture Guide

## 1. Executive Summary

The frontend of the **Sunshine Mental Health Portal** has been transformed from static HTML/JavaScript files into a modern **React single-page application (SPA)** in the `client/` directory.

The migration preserves the existing ASP.NET Core 10 backend (`server/Sunshine.App`), PostgreSQL 18 database, Entity Framework Core models, business services, and security policies.

---

## 2. Technology Stack & Architectural Overview

```text
┌─────────────────────────────────────────────────────────────┐
│                 React 19 + Vite 8 Frontend                   │
│   (React Router 7, Tailwind CSS v4, Lucide Icons, Fetch API) │
│                        Port: 5173                           │
└──────────────────────────────┬──────────────────────────────┘
                               │
                HTTP / JSON (Bearer JWT + CORS)
                Vite Reverse Proxy: /api -> :5000
                               │
┌──────────────────────────────▼──────────────────────────────┐
│             ASP.NET Core 10 Telehealth Backend               │
│          (JWT Authentication, Role Policies, CORS)          │
│                        Port: 5000                           │
└──────────────────────────────┬──────────────────────────────┘
                               │
                       Npgsql / EF Core
                               │
┌──────────────────────────────▼──────────────────────────────┐
│                    PostgreSQL 18 Database                   │
│              Database: sunshine_db (Port 5432)              │
└─────────────────────────────────────────────────────────────┘
```

### Core Frontend Packages
- **React 19 (`react`, `react-dom`)**: Concurrent rendering, hooks, and clean state primitives.
- **Vite 8 (`vite`, `@vitejs/plugin-react`)**: Sub-second Hot Module Replacement (HMR) and optimized Rollup production builds.
- **Tailwind CSS v4 (`@tailwindcss/vite`)**: CSS tokens for teal primary and amber sunshine accents.
- **React Router 7 (`react-router-dom`)**: Declarative client-side routing with role-based route protection.
- **Lucide React (`lucide-react`)**: Accessible iconography.

---

## 3. Directory Structure (`client/`)

```text
client/
├── package.json               # Frontend dependencies & scripts
├── vite.config.js             # Vite configuration with Tailwind & /api proxy
├── index.html                 # HTML template with Google Fonts
└── src/
    ├── main.jsx               # Application bootstrap
    ├── App.jsx                # Router configuration & protected routes
    ├── index.css              # Tailwind v4 import & custom theme tokens
    │
    ├── api/
    │   └── apiClient.js       # Centralized API service with Bearer auth & error handling
    │
    ├── context/
    │   └── AuthContext.jsx    # User session, JWT persistence, login/logout, and role checks
    │
    ├── utils/
    │   └── dateUtils.js       # BDT currency (৳), Asia/Dhaka (BST) formatters, hold countdown
    │
    ├── components/
    │   ├── common/
    │   │   ├── EmergencyBanner.jsx   # 24/7 Bangladesh crisis hotline (Kaan Pete Roi, 999)
    │   │   ├── Navbar.jsx            # Responsive navigation & role indicators
    │   │   ├── Footer.jsx            # Telehealth standards & legal disclaimers
    │   │   └── ProtectedRoute.jsx    # Role-based route guard (PATIENT, DOCTOR, ADMIN)
    │   └── patient/
    │       ├── DoctorCard.jsx        # Specialist card with BMDC badge and BDT fee
    │       ├── BookingModal.jsx      # Slot selector with 15-minute advance hold logic
    │       ├── BkashPaymentModal.jsx # MFS checkout (bKash, Nagad, Rocket) with countdown
    │       └── RescheduleModal.jsx   # Interactive appointment rescheduling dialog
    │
    └── pages/
        ├── public/
        │   ├── LandingPage.jsx       # Public homepage with doctor directory & FAQs
        │   ├── LoginPage.jsx         # Sign in with one-click demo credentials
        │   └── RegisterPage.jsx      # Patient & Doctor onboarding form
        ├── patient/
        │   └── PatientPortal.jsx     # Patient Care Portal (Specialists, Consultations, Vault)
        ├── doctor/
        │   └── DoctorDashboard.jsx   # Practice ledger, policy toggle, notes modal
        └── admin/
            ├── AdminLoginPage.jsx    # Isolated high-security admin authentication
            └── AdminDashboard.jsx    # Revenue metrics, BMDC approvals, user toggles, audit logs
```

---

## 4. Key Bangladesh Telehealth Implementations

### 1. BDT Currency (৳)
All doctor fees, subscription tier prices, and transaction records display in Bangladeshi Taka using `formatBDT()` (`utils/dateUtils.js`).

### 2. Asia/Dhaka (+06:00 BST) Timezone Handling
Consultation slots and appointment times are explicitly calculated and formatted for the Bangladesh Standard Timezone (`Asia/Dhaka`).

### 3. 15-Minute Advance Hold Countdown & Double-Booking Prevention
When a patient books a slot with an advance-payment doctor:
- The backend creates an appointment in `HELD` status with a 15-minute expiration timestamp.
- The React frontend displays a live countdown timer (`getHoldRemainingTime()`).
- If another patient attempts to select the same slot, the backend returns HTTP 400 (`This time slot has already been booked`), which the frontend cleanly displays without crashing.

### 4. MFS Payment Sandbox Modal
`BkashPaymentModal.jsx` simulates the Bangladesh mobile financial checkout flow:
- Supports **bKash** (signature magenta `#e2136e`), **Nagad** (orange `#f7941d`), **Rocket** (purple `#8c3494`), and cards.
- Displays live countdown remaining on the hold.
- Collects 11-digit Bangladeshi mobile number and sandbox PIN.
- Dispatches payment processing request to `/api/patient/payments/process`.

### 5. 24/7 Bangladesh Emergency Hotline Banner
A persistent banner (`EmergencyBanner.jsx`) alerts users to emergency hotlines:
- **কান পেতে রই (Kaan Pete Roi)**: `+8801779554391`
- **জাতীয় জরুরি সেবা**: `999`
- **স্বাস্থ্য বাতায়ন**: `16263`
- **জাতীয় মানসিক স্বাস্থ্য ইনস্টিটিউট (NIMH)**: `09612-600600`

---

## 5. Portal Implementations

| Portal | URL | Allowed Role | Key Features |
|---|---|---|---|
| **Public Landing** | `/` | All / Guest | Specialist directory, CBT preview, FAQ, booking trigger |
| **Authentication** | `/login`, `/register` | Guest | Demo auto-fill, patient & doctor registration |
| **Patient Care Portal** | `/patient` | `PATIENT` | Find counselors, consultations ledger, 15-min hold pay, reschedule modal, CBT vault, medical profile |
| **Doctor Practice Portal** | `/doctor` | `DOCTOR` | Practice ledger, fast policy switch (Advance vs Post-Payment), clinical notes editor, join telehealth |
| **Admin Console** | `/admin` | `ADMIN` | Total BDT revenue, BMDC verification queue, user activation toggles, live audit logs |
| **Isolated Admin Login** | `/admin/login` | Guest | Dedicated high-security entrance for administrators |

---

## 6. Development & Production Operations

### Running in Development
1. Start the ASP.NET Core backend:
   ```bash
   cd server/Sunshine.App
   dotnet run --urls "http://0.0.0.0:5000"
   ```
2. Start the Vite React development server:
   ```bash
   cd client
   npm run dev -- --host 0.0.0.0 --port 5173
   ```
3. Open `http://localhost:5173` in your browser. All `/api/*` calls are automatically proxied to the ASP.NET Core backend.

### Building for Production
```bash
cd client
npm run build
```
Build output is generated in `client/dist/`.

### Automated Testing
- Backend unit and integration tests:
  ```bash
  dotnet test Sunshine.slnx
  ```
- Bangladesh criteria verification:
  ```bash
  bash test_bangladesh.sh
  ```
- Concurrency and double-booking verification:
  ```bash
  bash test_double_booking.sh
  ```
