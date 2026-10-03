# Production Deployment Overview

Deploying Sunshine onto a production Linux host (e.g. Omarchy Linux or Arch/Ubuntu server).

---

## 1. Production Architecture

```text
[ Client Web Browser ]
          │
          ▼ HTTPS (Port 443)
[ Nginx Reverse Proxy / SSL Termination ]
          │
          ▼ HTTP (Port 5000)
[ Kestrel Web Server (Managed by Systemd) ]
          │
          ▼ TCP Port 5432
[ PostgreSQL 18 Database Cluster ]
```

* **Nginx**: Terminates SSL/TLS certificates, compresses static assets, and proxies requests.
* **Kestrel**: High-performance internal server running the ASP.NET Core process.
* **Systemd**: Monitors the .NET process and restarts it automatically if it crashes.
