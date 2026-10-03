# Systemd Service Configuration

Configuring systemd to run Sunshine as a reliable background daemon.

---

## 1. Create `/etc/systemd/system/sunshine.service`

```ini
[Unit]
Description=Sunshine Mental Health Portal Service
After=network.target postgresql.service

[Service]
WorkingDirectory=/var/www/sunshine
ExecStart=/usr/bin/dotnet /var/www/sunshine/Sunshine.App.dll --urls "http://127.0.0.1:5000"
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=sunshine-web
User=sunshine
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

---

## 2. Enable and Start the Service

```bash
sudo systemctl daemon-reload
sudo systemctl enable --now sunshine.service
sudo systemctl status sunshine.service
```
