# Entity Mapping & Data Annotations

EF Core uses data annotations and conventions to map C# domain classes to database tables.

---

## 1. Key Annotations Used in Sunshine

* **`[Table("table_name")]`**: Overrides default class naming and maps directly to the PostgreSQL table.
* **`[Key]`**: Designates the primary key.
* **`[ForeignKey(nameof(Property))]`**: Establishes relational integrity.
* **`[MaxLength(N)]`**: Generates `VARCHAR(N)` column constraints.
* **`[Column(TypeName = "decimal(10,2)")]`**: Specifies numeric precision for BDT currency fields.
