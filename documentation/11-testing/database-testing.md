# Database Sanity Testing (`sql/07_test_queries.sql`)

Direct database verification without application middleware.

---

## 1. Running the Test Queries

```bash
PGPASSWORD='SunshinePass123!' psql -h localhost -U sunshine_user -d sunshine_db -f sql/07_test_queries.sql
```

## 2. What Is Verified?
* Table row counts across all 16 tables.
* Foreign key join integrity between `users`, `doctors`, and `patients`.
* Partial unique index existence and query planner usage.
* Total live revenue in BDT (`SELECT SUM("Amount") FROM payments WHERE "Status" = 'SUCCESS'`).
* Audit log chronology.
