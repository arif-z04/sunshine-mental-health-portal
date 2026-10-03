# EF Core Migrations vs. SQL Pipeline

Understanding Sunshine's hybrid database-first approach.

---

## 1. Why Sunshine Uses a SQL Pipeline

Many .NET tutorials use `dotnet ef migrations add`. However, in complex enterprise and healthcare applications:
1. Native PostgreSQL features like check constraints, regex validation, and dynamic sequence reset loops are cleaner to version-control in pure SQL.
2. Database administrators (DBAs) can review, optimize, and audit `sql/01_` through `sql/06_` without needing .NET installed.

---

## 2. Using EF Migrations (Optional Workflow)

If you prefer using EF Core CLI migrations:
```bash
# Add migration
dotnet ef migrations add InitialSunshineSchema -s server/Sunshine.App

# Apply to database
dotnet ef database update -s server/Sunshine.App
```
