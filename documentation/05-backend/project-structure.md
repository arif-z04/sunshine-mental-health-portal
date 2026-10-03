# Backend Project Architecture & Folders

The backend source code is located in `server/Sunshine.App`:

---

```text
Sunshine.App/
├── Controllers/
│   ├── AuthController.cs         # /api/auth/register, login, logout, me
│   ├── PatientController.cs      # /api/patient/doctors, slots, appointments, profile
│   ├── DoctorController.cs       # /api/doctor/profile, appointments, working-hours
│   ├── AdminController.cs        # /api/admin/metrics, users, doctors, audit-logs
│   ├── PaymentController.cs      # /api/payment/simulate, appointment checkout
│   ├── SubscriptionController.cs # /api/subscription/plans, subscribe
│   └── ResourceController.cs     # /api/resource/categories, items
│
├── Data/
│   └── ApplicationDbContext.cs   # Entity Framework Core database context
│
├── DTOs/
│   └── DTOs.cs                   # Strongly typed C# records for API requests/responses
│
├── Models/
│   └── Entities.cs               # C# domain classes matching PostgreSQL tables
│
├── Services/
│   ├── AppointmentService.cs     # Slot calculation, double-booking prevention, rescheduling
│   ├── AuthService.cs            # Password hashing, JWT token generation
│   ├── PaymentService.cs         # MFS checkout simulation, transaction recording
│   ├── SubscriptionService.cs    # Subscription activation & paywall gating
│   └── AuditService.cs           # Immutable security audit logging
│
├── wwwroot/                      # Static client assets (HTML, CSS, JS)
├── appsettings.json              # Connection strings and JWT settings
└── Program.cs                    # Application startup and middleware registration
```
