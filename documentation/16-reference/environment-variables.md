# Environment Variables Reference

Configuration values supported by the backend:

---

| Variable | Description | Example / Default | Required |
| :--- | :--- | :--- | :---: |
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string | `Host=localhost;Database=sunshine_db;Username=sunshine_user;Password=SunshinePass123!` | Yes |
| `Jwt__Key` | HMAC-SHA256 256-bit secret key | `SunshineSecretKeyForMentalHealthPortalBangladesh2026ProductionSuperKey!` | Yes |
| `Jwt__Issuer` | Token issuer identifier | `Sunshine.Api` | No |
| `Jwt__Audience` | Token audience identifier | `Sunshine.Client` | No |
| `Jwt__ExpiryDays` | Token lifetime in days | `7` | No |
| `ASPNETCORE_ENVIRONMENT` | Runtime environment | `Development` (or `Production`) | No |
| `ASPNETCORE_URLS` | Kestrel binding URLs | `http://0.0.0.0:5000` | No |
