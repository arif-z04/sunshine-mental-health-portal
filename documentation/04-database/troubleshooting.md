# Database Troubleshooting & Diagnostics

Common PostgreSQL issues and their solutions.

---

## 1. "Connection Refused" (Port 5432)
* **Symptom**: `psql: error: connection to server on socket "/tmp/.s.PGSQL.5432" failed: No such file or directory`.
* **Cause**: PostgreSQL service is stopped or not installed.
* **Fix**:
  ```bash
  sudo systemctl start postgresql
  systemctl status postgresql
  ```

---

## 2. "Password Authentication Failed for User sunshine_user"
* **Symptom**: `psql: error: password authentication failed for user "sunshine_user"`.
* **Fix**: Re-run the password update command:
  ```bash
  sudo -u postgres psql -c "ALTER ROLE sunshine_user WITH PASSWORD 'SunshinePass123!';"
  ```

---

## 3. "Duplicate Key Value Violates Unique Constraint" (Error 23505)
* **Cause**: A table's `SERIAL` sequence is behind the maximum ID currently stored in that table.
* **Fix**: Re-run the PL/pgSQL sequence synchronization block at the bottom of `sql/06_seed_data.sql`.
