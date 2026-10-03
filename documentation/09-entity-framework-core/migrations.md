# Migrations vs. SQL Pipeline in Sunshine

Sunshine uses a hybrid **Database-First SQL Pipeline**:
* Complex constraints, triggers, and sequences are version-controlled in `sql/`.
* C# models in `Entities.cs` mirror the SQL tables directly.
