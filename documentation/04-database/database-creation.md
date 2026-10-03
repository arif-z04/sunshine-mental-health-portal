# Creating the Sunshine Database & User

This guide details how `sql/01_create_database.sql` initializes the database user and catalog.

---

## 1. Inspecting `sql/01_create_database.sql`

```sql
DO $$
BEGIN
   IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'sunshine_user') THEN
      CREATE ROLE sunshine_user WITH LOGIN PASSWORD 'SunshinePass123!';
   END IF;
END
$$;

SELECT 'CREATE DATABASE sunshine_db OWNER sunshine_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'sunshine_db')\gexec
```

---

## 2. Why Not Use the `postgres` Superuser?

In development and production, web applications should **never connect as the database superuser (`postgres`)**:
* **Principle of Least Privilege**: If an application vulnerability occurs, an attacker connected as `postgres` could drop any database, create system users, or execute filesystem commands.
* Dedicated user `sunshine_user` is restricted solely to the `sunshine_db` catalog.

---

## 3. Execution Command

Run the initialization script as the local `postgres` administrator:

```bash
sudo -u postgres psql -f sql/01_create_database.sql
```
* `sudo -u postgres`: Switches user to system user `postgres`.
* `psql`: The PostgreSQL command-line shell.
* `-f sql/01_create_database.sql`: Tells psql to read and execute the SQL statements from the file.
