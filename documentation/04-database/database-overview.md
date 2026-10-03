# Relational Database Design Overview

The Sunshine Mental Health Portal uses **PostgreSQL 18** as its primary source of truth.

---

## 1. What is a Relational Database?

A relational database organizes data into **tables** (relations) consisting of **rows** (records) and **columns** (attributes). Tables are interconnected through **keys**:
* **Primary Key (PK)**: A unique identifier for every row in a table (e.g. `User.Id` or `Appointment.Id`).
* **Foreign Key (FK)**: A column in one table pointing to the Primary Key of another table (e.g. `appointments.DoctorId -> doctors.Id`), enforcing strict relational integrity.

---

## 2. Why PostgreSQL 18?

PostgreSQL is one of the world's most advanced, open-source object-relational databases. Sunshine leverages several distinctive PostgreSQL features:
1. **Partial Unique Indexes**: Enforces concurrency rules that no other standard SQL database handles as cleanly (e.g. preventing double-booking only where `Status != 'CANCELLED'`).
2. **Dynamic Serial Sequence Management**: Built-in sequence generators (`pg_get_serial_sequence`) handle auto-incrementing integer IDs without collisions.
3. **Rich Constraint Engine**: Check constraints (`CHECK (Price >= 0)`) ensure data validity at the database engine level, independent of application code.
4. **First-class UTF-8 Support**: Seamlessly persists Bengali text (বাংলা ইউনিকোড), doctor biographies, and emergency hotline descriptors.
