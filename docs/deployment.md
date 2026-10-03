# Sunshine Mental Health Portal — Deployment Guide

This guide details deploying the Sunshine Mental Health Portal to Linux servers (Ubuntu/Debian, Arch Linux, or Docker container environments).

---

## 1. Prerequisites

- **.NET SDK / Runtime 10.0+**
- **PostgreSQL 15+** (tested on PostgreSQL 18)
- **Nginx** (recommended reverse proxy)
- **systemd** for daemon management

---

## 2. Environment Variables

Configure application settings via environment variables or `appsettings.Production.json`:

| Variable | Description | Example |
| :--- | :--- | :--- |
| `ASPNETCORE_ENVIRONMENT` | Application environment | `Production` |
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string | `Host=localhost;Port=5432;Database=sunshine_db;Username=sunshine_user;Password=<STRONG_PASS>` |
| `Jwt__Key` | Secret key for signing JWT tokens | `<MINIMUM_32_CHARACTERS_SECURE_RANDOM_KEY>` |
| `Jwt__Issuer` | JWT token issuer | `Sunshine.Api` |
| `Jwt__Audience` | JWT audience claim | `Sunshine.Client` |

---

## 3. Database Preparation

Run the SQL scripts in numerical order:
```bash
PGPASSWORD='<POSTGRES_PASS>' psql -h localhost -p 5432 -U postgres -f sql/01_create_database.sql
PGPASSWORD='<USER_PASS>' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/02_extensions.sql
PGPASSWORD='<USER_PASS>' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/03_schema.sql
PGPASSWORD='<USER_PASS>' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/04_indexes.sql
PGPASSWORD='<USER_PASS>' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/05_constraints.sql
PGPASSWORD='<USER_PASS>' psql -h localhost -p 5432 -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql
```

---

## 4. Building the Application

Publish the production binaries:
```bash
cd server/Sunshine.App
dotnet publish -c Release -o /var/www/sunshine
```

---

## 5. systemd Service Setup

Create `/etc/systemd/system/sunshine.service`:
```ini
[Unit]
Description=Sunshine Mental Health Portal (.NET 10)
After=network.target postgresql.service

[Service]
WorkingDirectory=/var/www/sunshine
ExecStart=/usr/bin/dotnet /var/www/sunshine/Sunshine.App.dll --urls "http://127.0.0.1:5000"
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=sunshine-portal
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

Enable and start the service:
```bash
sudo systemctl daemon-reload
sudo systemctl enable --now sunshine.service
```

---

## 6. Nginx Reverse Proxy Configuration

Create `/etc/nginx/sites-available/sunshine.conf`:
```nginx
server {
    listen 80;
    server_name sunshine.org.bd www.sunshine.org.bd;
    return 301 https://$host$request_uri;
}

server {
    listen 443 ssl http2;
    server_name sunshine.org.bd www.sunshine.org.bd;

    ssl_certificate /etc/letsencrypt/live/sunshine.org.bd/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/sunshine.org.bd/privkey.pem;

    client_max_body_size 10M;

    location / {
        proxy_pass http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```
