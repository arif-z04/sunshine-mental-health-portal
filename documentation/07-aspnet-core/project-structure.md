# Backend Directory Layout

`server/Sunshine.App/` contains the backend source code:

---

```text
server/Sunshine.App/
├── Controllers/         # REST API endpoints (Auth, Patient, Doctor, Admin, Payment)
├── Services/            # Business logic (Appointment, Payment, Auth, Audit)
├── Models/              # Database entities matching PostgreSQL tables (Entities.cs)
├── DTOs/                # Data Transfer Objects for API inputs and outputs (DTOs.cs)
├── Data/                # ApplicationDbContext (EF Core mapping)
├── wwwroot/             # Client HTML, CSS, JavaScript assets
├── appsettings.json     # PostgreSQL connection string and JWT keys
└── Program.cs           # Entrypoint: registers services and middleware
```
