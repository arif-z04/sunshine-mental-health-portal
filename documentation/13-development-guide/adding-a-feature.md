# Tutorial: How to Add a New Feature

Step-by-step guide for implementing a new full-stack feature (e.g. "Doctor Endorsement").

---

## 1. Architecture Flow for New Features

```text
Database Table (SQL / EF Core)
              │
              ▼
Domain Model (Entities.cs)
              │
              ▼
DTOs (Request / Response records)
              │
              ▼
Domain Service (Business rules)
              │
              ▼
Controller Endpoint (HTTP routing)
              │
              ▼
Frontend UI (HTML / CSS / JS)
              │
              ▼
Automated Tests (xUnit & E2E)
```

1. **Database**: Author table in SQL or add migration.
2. **Model**: Add C# entity in `Models/Entities.cs`.
3. **DbContext**: Register `DbSet<T>` in `Data/ApplicationDbContext.cs`.
4. **DTOs**: Add request and response records in `DTOs/DTOs.cs`.
5. **Service**: Implement business logic with transaction handling in `Services/`.
6. **Controller**: Expose endpoint with `[Authorize(Roles = ...)]` attribute.
7. **Frontend**: Create UI form/card in `wwwroot/` and attach `fetch` call.
8. **Testing**: Write unit tests in `Sunshine.Tests`.
