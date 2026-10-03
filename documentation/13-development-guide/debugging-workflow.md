# Practical Debugging Guide for Developers

Diagnosing issues efficiently across the entire stack.

---

## 1. Inspecting Live PostgreSQL Queries
Connect with `psql` to view running queries:
```sql
SELECT pid, query, state, age(clock_timestamp(), query_start) 
FROM pg_stat_activity 
WHERE state != 'idle';
```

## 2. Enabling Verbose EF Core Logging
In `appsettings.json`, set:
```json
"Logging": {
  "LogLevel": {
    "Microsoft.EntityFrameworkCore.Database.Command": "Information"
  }
}
```
This logs the exact raw SQL query, parameters, and execution time to the terminal.
