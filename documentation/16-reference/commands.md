# Master Command Reference

Essential terminal commands grouped by domain:

---

## 1. Linux & Systemd (Omarchy)
* `sudo pacman -Syu`: Update all system packages.
* `sudo systemctl enable --now postgresql`: Start and enable PostgreSQL.
* `systemctl status postgresql`: Check PostgreSQL daemon health.
* `journalctl -u postgresql -n 50`: Inspect PostgreSQL logs.
* `lsof -i :5000`: Find process occupying port 5000.

## 2. PostgreSQL Shell (`psql`)
* `psql -U sunshine_user -d sunshine_db`: Connect to database.
* `\dt`: List all tables in current database.
* `\d+ appointments`: Inspect schema and indexes of a table.
* `\q`: Exit psql shell.

## 3. .NET & C# CLI
* `dotnet build Sunshine.slnx`: Compile entire solution.
* `dotnet test`: Execute all 26 automated unit and integration tests.
* `cd server/Sunshine.App && dotnet run --urls "http://0.0.0.0:5000"`: Run application on port 5000.

## 4. Verification Scripts
* `./test_e2e_full.sh`: Comprehensive 9-suite multi-portal end-to-end verification.
* `./test_verification.sh`: Smoke and authorization checks.
* `./test_bangladesh.sh`: Bangladesh localization and BDT MFS flows.
* `./test_double_booking.sh`: Concurrency conflict test.
