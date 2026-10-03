# SQL from Scratch

## 1. What is SQL?
**SQL** stands for **Structured Query Language**. It is the international standard language used to interact with relational databases.

## 2. The Four Fundamental Commands (CRUD)
* **`SELECT`** (Read): `SELECT * FROM doctors WHERE "IsVerified" = true;`
* **`INSERT`** (Create): `INSERT INTO payments ("Amount", "Currency") VALUES (1500.00, 'BDT');`
* **`UPDATE`** (Edit): `UPDATE appointments SET "Status" = 'CONFIRMED' WHERE "Id" = 8;`
* **`DELETE`** (Remove): `DELETE FROM notifications WHERE "Id" = 42;`
