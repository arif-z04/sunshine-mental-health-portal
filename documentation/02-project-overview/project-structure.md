# Repository Project Structure

Below is the annotated file tree of the entire Sunshine Mental Health repository:

```text
Another-Sunshine-Project/
├── Sunshine.slnx                     # Solution file organizing C# projects
├── prompt.md                         # Initial product specification requirements
├── prompt2.md                        # Comprehensive documentation specification
├── .env.example                      # Template environment variable configuration
│
├── test_e2e_full.sh                  # Master end-to-end multi-portal verification script
├── test_verification.sh              # Basic smoke and RBAC test script
├── test_bangladesh.sh                # Bangladesh localization and BDT test script
├── test_double_booking.sh            # Concurrency stress and double-booking test script
│
├── sql/                              # PostgreSQL Database Pipeline
│   ├── 01_create_database.sql        # Creates sunshine_user and sunshine_db
│   ├── 02_extensions.sql             # Installs uuid-ossp and pgcrypto
│   ├── 03_schema.sql                 # DDL for all 16 tables
│   ├── 04_indexes.sql                # Foreign key & partial unique booking indexes
│   ├── 05_constraints.sql            # Check constraints & business validation rules
│   ├── 06_seed_data.sql              # Seed clinicians, patients, plans, resources & sequence sync
│   ├── 07_test_queries.sql           # Database sanity test queries
│   └── README.md                     # SQL execution instructions
│
├── docs/                             # Legacy architectural reference guides
│
├── documentation/                    # Complete Beginner-Friendly Documentation System
│   ├── README.md                     # Documentation Hub & Navigation
│   ├── 01-getting-started/           # Onboarding & prerequisites
│   ├── 02-project-overview/          # System design & features
│   ├── 03-omarchy-setup/             # Omarchy / Arch Linux workstation guide
│   ├── 04-database/                  # PostgreSQL from scratch & schema deep-dive
│   ├── 05-backend/                   # ASP.NET Core controllers, services & middleware
│   ├── 06-entity-framework/          # EF Core context, LINQ & migrations
│   ├── 07-frontend/                  # HTML5, CSS3 & Material UI design system
│   ├── 08-authentication/            # JWT, PBKDF2 & RBAC security
│   ├── 09-features/                  # Detailed feature documentation
│   ├── 10-api/                       # REST endpoint specifications & payloads
│   ├── 11-testing/                   # Unit, integration & E2E testing
│   ├── 12-security/                  # Healthcare privacy & threat mitigations
│   ├── 13-development-guide/         # Developer contribution workflows
│   ├── 14-deployment/                # Linux systemd & reverse proxy deployment
│   ├── 15-troubleshooting/          # Error diagnostic guide
│   ├── 16-reference/                # Command dictionary, variables & FAQ
│   ├── 17-learning-path/             # Step-by-step developer learning roadmap
│   └── diagrams/                     # Mermaid diagrams (Architecture, ERD, UML, DFD)
│
└── server/                           # Source Code Directory
    ├── Sunshine.App/                 # Main ASP.NET Core 10 Web Application
    │   ├── Controllers/              # API endpoints (Auth, Patient, Doctor, Admin, etc.)
    │   ├── Data/                     # ApplicationDbContext
    │   ├── DTOs/                     # Strongly typed request/response records
    │   ├── Models/                   # C# Entity definitions (Entities.cs)
    │   ├── Services/                 # Business logic: Appointment, Payment, Auth, Audit
    │   ├── wwwroot/                  # Client assets served by Kestrel
    │   │   ├── admin/                # Admin Console & isolated login
    │   │   ├── doctor/               # Doctor Practice Portal
    │   │   ├── patient/              # Patient Care Portal
    │   │   └── index.html            # Public Landing Page
    │   ├── appsettings.json          # Configuration & PostgreSQL connection string
    │   ├── Program.cs                # Dependency injection & HTTP middleware pipeline
    │   └── Sunshine.App.csproj       # Project build manifest
    │
    └── Sunshine.Tests/               # xUnit Automated Test Suite
        ├── AppointmentSchedulingTests.cs
        ├── PaymentAndSubscriptionTests.cs
        ├── AuthenticationAndRbacTests.cs
        ├── BangladeshRequirementsTests.cs
        ├── AuditLoggingTests.cs
        ├── TestHelpers.cs
        └── Sunshine.Tests.csproj
```
