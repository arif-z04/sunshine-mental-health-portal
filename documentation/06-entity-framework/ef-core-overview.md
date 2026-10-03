# Entity Framework Core Overview

Entity Framework Core (EF Core) is the official Object-Relational Mapper (ORM) for .NET.

---

## 1. What does an ORM do?

In traditional software development, developers had to manually write SQL queries, map database rows into C# objects, and handle data type conversions.
EF Core automates this translation:
* C# classes represent database tables (`Entities.cs`).
* LINQ queries in C# are translated into optimized SQL executed on PostgreSQL.
* Change tracking monitors object modifications and issues corresponding `UPDATE` statements when `SaveChanges()` is called.

---

## 2. Provider: `Npgsql.EntityFrameworkCore.PostgreSQL`

Sunshine uses the official open-source PostgreSQL provider for EF Core, maintained by the Npgsql team. It maps native PostgreSQL types (`uuid`, `timestamptz`, `date`, `time`, `numeric`) directly to C# types (`Guid`, `DateTime`, `DateOnly`, `TimeOnly`, `decimal`).
