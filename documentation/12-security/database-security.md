# Database Security & SQL Injection Mitigation

Securing PostgreSQL against unauthorized access and injection attacks.

---

## 1. Automatic Parameterization via EF Core
EF Core compiles LINQ queries into **parameterized SQL queries**:
```csharp
var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == inputEmail);
```
Translates to:
```sql
SELECT * FROM users WHERE "Email" = @__inputEmail_0 LIMIT 1;
```
Because `@__inputEmail_0` is treated strictly as literal data rather than executable SQL syntax, SQL injection is completely prevented.
