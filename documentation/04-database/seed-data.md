# Seed Data & Sequence Synchronization

`sql/06_seed_data.sql` populates the database with realistic development data.

---

## 1. Seed Personas

* **Admin**: `admin@sunshine.org` (Prof. Dr. Farzana Rahman - Admin oversight).
* **Doctor 1**: `dr.tanvir@sunshine.org` (Dr. Tanvir Ahmed, MBBS, MD - BSMMU Psychiatry, fee ৳1,500 BDT).
* **Doctor 2**: `dr.nusrat@sunshine.org` (Dr. Nusrat Jahan, MS - DU Clinical Psychology, fee ৳1,200 BDT).
* **Doctor 3**: `dr.rafiq@sunshine.org` (Dr. K. M. Rafiqul Islam, FCPS - NIMH Adolescent specialist, fee ৳1,800 BDT).
* **Patient 1**: `anika@example.com` (Anika Tabassum - Active Annual Pass).
* **Patient 2**: `rahat@example.com` (Rahat Chowdhury - Patient with booked history).
* **Patient 3**: `sazzad@example.com` (Sazzad Hossain - Visitor testing paywall).

*Universal Demo Password*: `Password123!`

---

## 2. Dynamic Sequence Synchronization (Fixing Error 23505)

When explicit integer IDs are inserted via SQL seeds, PostgreSQL's internal auto-increment sequences remain set to 1. Subsequent API inserts would crash with:
`duplicate key value violates unique constraint` (Error 23505).

To prevent this, `sql/06_seed_data.sql` executes a dynamic PL/pgSQL synchronization block:
```sql
DO $$
DECLARE
    tbl RECORD;
    seq_name TEXT;
BEGIN
    FOR tbl IN
        SELECT table_name, column_name
        FROM information_schema.columns
        WHERE table_schema = 'public'
          AND column_default LIKE 'nextval%'
    LOOP
        seq_name := pg_get_serial_sequence(quote_ident(tbl.table_name), tbl.column_name);
        IF seq_name IS NOT NULL THEN
            EXECUTE format('SELECT setval(%L, COALESCE((SELECT MAX(%I) FROM %I), 0) + 1, false);',
                seq_name, tbl.column_name, tbl.table_name);
        END IF;
    END LOOP;
END
$$;
```
This inspects every table with an auto-incrementing column and synchronizes its sequence to `MAX(Id) + 1`.
