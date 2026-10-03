# Service Management with `systemctl`

On modern Linux systems, background server programs (called **daemons** or **services**) are managed by **systemd**.

---

## 1. Core Service Commands

### Checking Service Health
```bash
systemctl status postgresql
```
* Outputs whether PostgreSQL is actively running, its process ID (PID), memory usage, and recent log messages.

### Starting a Service
```bash
sudo systemctl start postgresql
```

### Stopping a Service
```bash
sudo systemctl stop postgresql
```

### Restarting a Service
```bash
sudo systemctl restart postgresql
```

### Enabling Automatic Startup on Boot
```bash
sudo systemctl enable --now postgresql
```
* `enable`: Tells Linux to launch PostgreSQL automatically whenever the computer boots up.
* `--now`: Starts the service immediately right now in the current session.
