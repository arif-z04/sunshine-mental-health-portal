# Entity Framework Core Troubleshooting

Common EF Core exceptions and their solutions.

---

## 1. Npgsql.PostgresException: 23505 Duplicate Key Value
* **Problem**: Auto-incrementing ID collided with an existing row.
* **Solution**: Re-run the dynamic sequence synchronization script in `sql/06_seed_data.sql`.

---

## 2. InvalidOperationException: Sequence Contains No Elements
* **Problem**: Calling `.FirstAsync()` on a query that returned zero rows.
* **Solution**: Use `.FirstOrDefaultAsync()` and check if the result is `null` before accessing properties.

---

## 3. DbUpdateConcurrencyException
* **Problem**: Two operations attempted to update the same record simultaneously under serializable isolation.
* **Solution**: Catch the exception and prompt the user to retry their operation.
