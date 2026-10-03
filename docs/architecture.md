# Sunshine Mental Health Portal — System Architecture

## 1. Architectural Overview

The **Sunshine Mental Health Portal** is a production-grade Bangladesh-focused healthcare and telecounseling SaaS platform. It is engineered with a clean separation of concerns, strong domain modeling, robust concurrency controls, and high-performance database design.

```text
+---------------------------------------------------------------------------------+
|                                 CLIENT LAYER                                    |
|   HTML5 • Modern CSS3 • Vanilla JavaScript • Material Design & Wellness Tokens   |
|   - Public Landing & Discovery Portal (/)                                       |
|   - Patient Care Portal (/patient)                                              |
|   - Doctor / Clinician Portal (/doctor)                                         |
|   - Admin Operations Console (/admin)                                           |
+----------------------------------------+----------------------------------------+
                                         | RESTful JSON over HTTPS (JWT Bearer)
+----------------------------------------v----------------------------------------+
|                               API & BACKEND LAYER                              |
|                              ASP.NET Core 10 • C#                               |
|   - Middleware: ExceptionHandlingMiddleware, StaticFiles, Routing, Auth, CORS   |
|   - Controllers: AuthController, PatientController, DoctorController, AdminController |
+----------------------------------------+----------------------------------------+
                                         | Dependency Injection
+----------------------------------------v----------------------------------------+
|                                SERVICE LAYER                                    |
|   - AppointmentService (Slot generation, 15m expiration, concurrency safety)    |
|   - PaymentService & MockPaymentGatewayService (bKash, Nagad, Rocket, Cards)   |
|   - SubscriptionService (BDT pass tiers, duration calculation, active checks)   |
|   - ResourceService (CBT vault, category filtering, paywall enforcement)        |
|   - DoctorService (BMDC credentials, availability, weekly schedule management) |
|   - NotificationService (In-app real-time alerts)                              |
|   - AuditService (Security-sensitive operational trail logging)                |
+----------------------------------------+----------------------------------------+
                                         | Entity Framework Core 10 (Database-First)
+----------------------------------------v----------------------------------------+
|                            DATA ACCESS / ORM LAYER                              |
|                      Npgsql.EntityFrameworkCore.PostgreSQL                      |
|   - ApplicationDbContext (Maps directly to pre-created PostgreSQL schema)       |
|   - Serializable transaction blocks for slot reservation                        |
+----------------------------------------+----------------------------------------+
                                         | TCP Port 5432 (UTF-8, Asia/Dhaka)
+----------------------------------------v----------------------------------------+
|                               DATABASE LAYER                                    |
|                               PostgreSQL 18                                     |
|   - Normalized Relational Tables (16 tables)                                    |
|   - Partial Unique Index ("IX_appointments_DoctorId_AppointmentDate_StartTime")  |
|   - Domain CHECK Constraints (BDT currency, roles, statuses, policies)         |
|   - Foreign Keys with Cascading and Restricted Delete Rules                     |
+---------------------------------------------------------------------------------+
```

---

## 2. Core Architectural Pillars

### A. Database-First Integrity
Rather than relying on loosely typed code-first migrations that can drift or fail on concurrent execution, the system defines its authoritative database structure in pure SQL scripts under `/sql/`. The EF Core context (`ApplicationDbContext`) maps explicitly to this normalized schema, preserving full database engine capabilities (such as PostgreSQL partial unique indexes with `WHERE` clauses).

### B. Concurrency & Double-Booking Prevention
Appointment booking implements a two-tier defense against race conditions:
1. **Application Layer**: Serializable transactions with slot occupancy pre-checks and 15-minute pending hold windows for advance payment policies.
2. **Database Engine Layer**: A PostgreSQL partial unique index:
   ```sql
   CREATE UNIQUE INDEX IF NOT EXISTS "IX_appointments_DoctorId_AppointmentDate_StartTime"
   ON appointments ("DoctorId", "AppointmentDate", "StartTime")
   WHERE "Status" != 'CANCELLED';
   ```
   Even under thousands of concurrent requests, PostgreSQL physically prevents duplicate active bookings for the same therapist at the same timestamp.

### C. Localization & Bangladesh Criteria
- **Timezone**: Explicit scheduling aligned to `Asia/Dhaka` (BST, UTC+6).
- **Currency**: Primary currency formatted in Bangladeshi Taka (`৳ BDT`).
- **Payment Channels**: Architecture ready for Bangladesh MFS (bKash, Nagad, Rocket) and local card schemes.
- **Crisis Hotlines**: Prominently featured national emergency resources including **Kaan Pete Roi (+8801779554391)**, **999**, and **16263**.
- **Specialists**: Verified with Bangladesh Medical & Dental Council (BMDC) credentials from BSMMU, DU, NIMH, SOMC, and CMC.
