# Configuration & `appsettings.json`

Managing settings and secrets across environments:

---

In `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=sunshine_db;Username=sunshine_user;Password=SunshinePass123!"
  },
  "Jwt": {
    "Key": "SunshineSecretKeyForMentalHealthPortalBangladesh2026ProductionSuperKey!",
    "Issuer": "Sunshine.Api",
    "Audience": "Sunshine.Client"
  }
}
```
ASP.NET Core reads these keys at startup and injects `IConfiguration` into services that need them.
