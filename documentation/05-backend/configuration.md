# Configuration Management & Environment Variables

ASP.NET Core uses a hierarchical configuration system combining `appsettings.json`, environment variables, and command-line flags.

---

## 1. `appsettings.json` Structure

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=sunshine_db;Username=sunshine_user;Password=SunshinePass123!"
  },
  "Jwt": {
    "Key": "SunshineSecretKeyForMentalHealthPortalBangladesh2026ProductionSuperKey!",
    "Issuer": "Sunshine.Api",
    "Audience": "Sunshine.Client",
    "ExpiryDays": 7
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

---

## 2. Overriding with Environment Variables

In production or automated CI pipelines, configuration values should never be stored in plain text. ASP.NET Core automatically maps environment variables using double underscores (`__`):

```bash
export ConnectionStrings__DefaultConnection="Host=db.internal;Port=5432;Database=sunshine_db;Username=prod_user;Password=SecureVaultPassword!"
export Jwt__Key="YourSuperSecretProductionKeyWithAtLeast32BytesLength!"
```
