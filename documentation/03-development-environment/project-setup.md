# End-to-End Project Setup Guide

Complete walk-through for setting up the Sunshine repository:

---

```bash
# 1. Navigate to workspace
cd /home/noir/Desktop/Another-Sunshine-Project

# 2. Run the SQL Pipeline to prepare database tables & seed data
sudo -u postgres psql -f sql/01_create_database.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/02_extensions.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/03_schema.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/04_indexes.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/05_constraints.sql
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/06_seed_data.sql

# 3. Compile the solution
dotnet build Sunshine.slnx

# 4. Run automated test suite
dotnet test

# 5. Start the web application
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```
