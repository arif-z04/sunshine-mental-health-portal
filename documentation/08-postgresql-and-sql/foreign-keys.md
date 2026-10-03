# Foreign Keys: Building Relationships

## 1. What is a Foreign Key?
A **Foreign Key** is a column in one table that points directly to the Primary Key of another table, ensuring data consistency.

## 2. Real-World Example
In `appointments`:
```sql
"DoctorId" INT NOT NULL REFERENCES doctors("Id") ON DELETE RESTRICT
```
* You cannot book an appointment for Doctor #99 if Doctor #99 does not exist in the `doctors` table!
* PostgreSQL rejects invalid foreign keys automatically with an error.
