# PostgreSQL Users, Permissions & Security

Security begins at the database layer. This document explains role ownership and privilege grants in Sunshine.

---

## 1. Role Definition & Password Authentication

The application connects using:
* **Username**: `sunshine_user`
* **Password**: `SunshinePass123!` (development credential)
* **Default Database**: `sunshine_db`

To grant complete table ownership and sequence usage to `sunshine_user`:
```sql
GRANT ALL PRIVILEGES ON DATABASE sunshine_db TO sunshine_user;
GRANT ALL ON ALL TABLES IN SCHEMA public TO sunshine_user;
GRANT ALL ON ALL SEQUENCES IN SCHEMA public TO sunshine_user;
```

---

## 2. Client Authentication Configuration (`pg_hba.conf`)

PostgreSQL controls connection access via `/var/lib/postgres/data/pg_hba.conf`:

```text
# TYPE  DATABASE        USER            ADDRESS                 METHOD
local   all             postgres                                peer
local   sunshine_db     sunshine_user                           scram-sha-256
host    sunshine_db     sunshine_user   127.0.0.1/32            scram-sha-256
```
* **scram-sha-256**: Modern salted challenge-response authentication preventing plaintext password transmission over local sockets.
