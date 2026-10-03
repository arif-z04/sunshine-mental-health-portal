# Tutorial: How to Add a New Database Table

Safely adding a table to the PostgreSQL schema and EF Core.

---

## Step 1: Write SQL DDL
```sql
CREATE TABLE doctor_reviews (
    "Id" SERIAL PRIMARY KEY,
    "DoctorId" INT NOT NULL REFERENCES doctors("Id") ON DELETE CASCADE,
    "PatientId" INT NOT NULL REFERENCES patients("Id") ON DELETE CASCADE,
    "Rating" INT NOT NULL CHECK ("Rating" BETWEEN 1 AND 5),
    "Comment" TEXT,
    "CreatedAt" TIMESTAMPTZ DEFAULT NOW()
);
```

## Step 2: Add C# Entity
```csharp
[Table("doctor_reviews")]
public class DoctorReview
{
    [Key]
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

## Step 3: Register in `ApplicationDbContext.cs`
```csharp
public DbSet<DoctorReview> DoctorReviews => Set<DoctorReview>();
```
