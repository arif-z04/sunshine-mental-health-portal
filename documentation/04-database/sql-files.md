# SQL Pipeline Reference Guide

The `sql/` directory provides an ordered, version-controlled SQL pipeline:

---

| Script | Purpose | Execution Mode |
| :--- | :--- | :--- |
| `01_create_database.sql` | Creates user `sunshine_user` and database `sunshine_db` | Superuser (`sudo -u postgres psql`) |
| `02_extensions.sql` | Installs `uuid-ossp` and `pgcrypto` extensions | Application user (`sunshine_user`) |
| `03_schema.sql` | Creates all 16 tables, columns, and primary/foreign keys | Application user (`sunshine_user`) |
| `04_indexes.sql` | Generates foreign key and partial unique indexes | Application user (`sunshine_user`) |
| `05_constraints.sql` | Enforces check constraints and format validators | Application user (`sunshine_user`) |
| `06_seed_data.sql` | Populates clinicians, emergency hotlines, and articles | Application user (`sunshine_user`) |
| `07_test_queries.sql` | Executes sanity queries verifying counts and relationships | Application user (`sunshine_user`) |

---

## Pipeline Execution Command

Run all scripts cleanly:
```bash
sudo -u postgres psql -f sql/01_create_database.sql

for script in sql/02_extensions.sql sql/03_schema.sql sql/04_indexes.sql sql/05_constraints.sql sql/06_seed_data.sql sql/07_test_queries.sql; do
    echo "Executing $script..."
    PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f "$script"
done
```
