# PostgreSQL Setup & Service Management on Omarchy

PostgreSQL 18 is the primary relational database for Sunshine.

---

## 1. Install PostgreSQL

```bash
sudo pacman -S postgresql
```

Verify the client tools:
```bash
psql --version
# Expected: psql (PostgreSQL) 18.x or 16.x
```

---

## 2. Initialize the Database Cluster (First Time Only)

Arch Linux does not initialize the PostgreSQL data directory automatically upon installation. Run this command once:

```bash
sudo -u postgres initdb -D /var/lib/postgres/data --locale=C.UTF-8 --encoding=UTF8
```
* `sudo -u postgres`: Runs the command as the system `postgres` user.
* `initdb`: The PostgreSQL cluster initialization utility.
* `-D /var/lib/postgres/data`: Specifies the directory where table data and logs are stored.
* `--encoding=UTF8`: Ensures full internationalization and Bengali script support.

---

## 3. Manage the PostgreSQL Service with `systemctl`

On Linux, background services are managed by **systemd**:

```bash
# Start PostgreSQL immediately and enable it to start on system boot:
sudo systemctl enable --now postgresql

# Check service health and running status:
systemctl status postgresql

# Stop PostgreSQL if needed:
sudo systemctl stop postgresql

# Restart PostgreSQL after configuration changes:
sudo systemctl restart postgresql
```

---

## 4. Inspecting Service Logs with `journalctl`

If PostgreSQL fails to start, use `journalctl` to view the service log:

```bash
journalctl -u postgresql -n 50 --no-pager
```
* `-u postgresql`: Filter logs strictly for the postgresql unit.
* `-n 50`: Show only the last 50 log entries.
* `--no-pager`: Output directly to the terminal without opening a scroll viewer.
