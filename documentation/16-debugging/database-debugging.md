# Database Diagnostics with `psql`

Query running processes in PostgreSQL:
```sql
SELECT pid, query, state FROM pg_stat_activity WHERE state != 'idle';
```
