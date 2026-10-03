# Database Learning Guide (PostgreSQL)

Key database concepts exemplified in Sunshine:

---

1. **Schema Design**: Normalizing data across 16 tables to prevent data duplication.
2. **Partial Indexes**: Understanding how `WHERE "Status" != 'CANCELLED'` optimizes query performance and enforces business invariants.
3. **Sequence Synchronization**: Understanding how PostgreSQL sequences work with `SERIAL` columns and how `setval` repairs sequence drift.
