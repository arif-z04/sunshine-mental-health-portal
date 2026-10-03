# Databases & Catalogs in PostgreSQL

## 1. The PostgreSQL Server Hierarchy
A single PostgreSQL instance can host multiple independent databases:

```text
PostgreSQL Server (Port 5432)
├── postgres (Default administrative database)
└── sunshine_db (Sunshine Mental Health Portal database)
```

## 2. Creating the Sunshine Database
From `sql/01_create_database.sql`:
```sql
CREATE DATABASE sunshine_db OWNER sunshine_user;
```
This isolates Sunshine's tables from any other software running on your Linux host.
