# The Canonical SQL Pipeline (01 to 07)

Sunshine organizes its database logic into an ordered pipeline:
* `01_create_database.sql`: User and catalog initialization.
* `02_extensions.sql`: `uuid-ossp` and `pgcrypto`.
* `03_schema.sql`: 16 tables DDL.
* `04_indexes.sql`: Partial unique booking indexes.
* `05_constraints.sql`: Check constraints.
* `06_seed_data.sql`: Seed clinicians, patients, hotlines, and sequence sync.
* `07_test_queries.sql`: Integrity verification queries.
