# Primary Keys: Unique Identifiers

## 1. What is a Primary Key?
A **Primary Key** is a column (or group of columns) that uniquely identifies every row in a table. No two rows can have the same primary key value, and it can never be empty (`NULL`).

## 2. In Sunshine
Every table in Sunshine uses an auto-incrementing integer `Id` as its primary key:
```sql
"Id" SERIAL PRIMARY KEY
```
