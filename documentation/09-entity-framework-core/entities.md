# Entities: C# Classes as Database Tables

Data annotations tell EF Core how C# classes map to PostgreSQL:
* `[Table("appointments")]`: Maps class to PostgreSQL table `appointments`.
* `[Key]`: Marks the primary key column.
* `[ForeignKey(nameof(DoctorId))]`: Configures foreign key relationship.
