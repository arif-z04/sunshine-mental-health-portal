# Installing & Initializing PostgreSQL on Omarchy

Follow these steps to set up PostgreSQL on Omarchy Linux.

---

## 1. Install PostgreSQL Package

```bash
sudo pacman -S postgresql
```

Verify the installed client version:
```bash
psql --version
# Output: psql (PostgreSQL) 18.x (or 16.x)
```

---

## 2. Initialize the Database Cluster

Before PostgreSQL can start, its internal data directory `/var/lib/postgres/data` must be initialized:

```bash
sudo -u postgres initdb -D /var/lib/postgres/data --locale=C.UTF-8 --encoding=UTF8
```
* **Why UTF-8?**: Mental health counseling in Bangladesh requires storing both Bengali and English text without encoding corruption.

---

## 3. Enable and Start the Systemd Service

```bash
sudo systemctl enable --now postgresql
```
* `enable`: Instructs Linux to launch PostgreSQL automatically whenever the computer boots.
* `--now`: Starts the database service immediately in the current session.

Verify service health:
```bash
systemctl status postgresql
```
The status should display `Active: active (running)`.
