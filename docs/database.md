# Sunshine Mental Health Portal — Database Architecture

## 1. Relational Schema Summary

The database uses PostgreSQL (default database name: `sunshine_db`, dedicated user: `sunshine_user`). The schema comprises 16 normalized tables categorized into 6 functional modules:

```text
       +-------------------+       1:N       +-------------------+
       |       roles       |<----------------|    user_roles     |
       +-------------------+                 +-------------------+
                                                       ^
                                                       | N:1
       +-------------------+       1:1       +-------------------+
       |     patients      |<----------------|       users       |
       +-------------------+                 +-------------------+
                 ^                                     | 1:1
                 | 1:N                                 v
                 |                           +-------------------+
                 |                           |      doctors      |
                 |                           +-------------------+
                 |                                     | 1:N
                 |                                     v
       +-------------------+       N:1       +-------------------+
       |   appointments    |---------------->| doctor_schedules  |
       +-------------------+                 +-------------------+
                 ^
                 | 1:N
       +-------------------+       N:1       +-------------------+
       |     payments      |---------------->|   subscriptions   |
       +-------------------+                 +-------------------+
                                                       | N:1
                                                       v
                                             +-------------------+
                                             |subscription_plans |
                                             +-------------------+
```

---

## 2. Table Catalog

| Table | Entity Purpose | Key Constraints |
| :--- | :--- | :--- |
| `users` | ASP.NET Core Identity user accounts | Unique `Email`, `Role IN ('ADMIN', 'DOCTOR', 'PATIENT')` |
| `roles` | System roles (ADMIN, DOCTOR, PATIENT) | Unique `NormalizedName` |
| `user_roles` | Identity user-to-role junction | Composite PK `(UserId, RoleId)` |
| `patients` | 1-to-1 extension of patient user profile | Unique `UserId`, FK with CASCADE |
| `doctors` | 1-to-1 extension of doctor user profile | Unique `UserId`, `PaymentPolicy IN ('ADVANCE', 'POST_PAYMENT')`, `Fee >= 0` |
| `doctor_schedules` | Weekly recurring clinical hours | `DayOfWeek BETWEEN 0 AND 6`, `StartTime < EndTime`, `SlotDurationMinutes > 0` |
| `appointments` | Booked telehealth sessions | Partial Unique Index on `(DoctorId, Date, StartTime) WHERE Status != 'CANCELLED'` |
| `subscription_plans` | Membership passes in BDT (৳) | `DurationType IN ('MONTHLY', 'QUARTERLY', 'YEARLY')`, `Price >= 0` |
| `subscriptions` | Active patient vault passes | `Status IN ('ACTIVE', 'EXPIRED', 'CANCELLED')`, `StartDate <= EndDate` |
| `payments` | bKash/Nagad/Rocket/Card ledger | Unique `TransactionId`, `PaymentMethod IN ('BKASH', 'NAGAD', 'ROCKET', 'CARD')` |
| `resource_categories`| Taxonomy for psychoeducational materials | Unique `Slug` |
| `resources` | Self-help e-books, guides, audio sessions | `ResourceType IN ('BOOK', 'ARTICLE', 'AUDIO', 'VIDEO')`, `IsPremium` flag |
| `resource_access` | Access audit trail for paywalled books | Tracks `UserId`, `ResourceId`, `AccessedAt` |
| `notifications` | In-app user notification items | Filtered by `UserId` and `IsRead` status |
| `audit_logs` | Administrative and security event audit trail | Records `ActorId`, `ActorEmail`, `Action`, `TargetType`, `TargetId`, `IpAddress` |

---

## 3. SQL Pipeline Execution Order

All scripts must be executed in numeric sequence as documented in [`sql/README.md`](file:///home/noir/Desktop/Sunshine-project/sql/README.md):
1. `01_create_database.sql` — Create database `sunshine_db` & role `sunshine_user`.
2. `02_extensions.sql` — Enable `uuid-ossp` and `pgcrypto`.
3. `03_schema.sql` — Relational table DDL.
4. `04_indexes.sql` — Performance indexes & partial unique double-booking index.
5. `05_constraints.sql` — Foreign keys and domain CHECK rules.
6. `06_seed_data.sql` — Real-world Bangladesh seed clinicians, patients, and BDT plans.
7. `07_test_queries.sql` — Sanity checks and diagnostic ledger aggregations.
