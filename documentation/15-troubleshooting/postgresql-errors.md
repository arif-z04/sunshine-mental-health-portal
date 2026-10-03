# PostgreSQL Diagnostic & Error Guide

---

## 1. Password Authentication Failed
* **Cause**: Incorrect password in connection string or missing permissions.
* **Fix**: Reset user password:
  ```bash
  sudo -u postgres psql -c "ALTER USER sunshine_user WITH PASSWORD 'SunshinePass123!';"
  ```

## 2. Database "sunshine_db" Does Not Exist
* **Fix**: Run `sql/01_create_database.sql`:
  ```bash
  sudo -u postgres psql -f sql/01_create_database.sql
  ```
