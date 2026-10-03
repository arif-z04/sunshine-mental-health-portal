# Production Configuration & Secrets

Configuring Sunshine for production environments.

---

## 1. Environment Configuration

Set the environment variable:
```bash
export ASPNETCORE_ENVIRONMENT=Production
```

In `appsettings.Production.json`:
* Set secure database credentials.
* Provide a cryptographically random JWT secret (at least 64 characters).
* Disable detailed exception pages.
