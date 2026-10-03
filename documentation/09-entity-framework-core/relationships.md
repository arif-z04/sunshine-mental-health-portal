# Modeling Relationships with Navigation Properties

In C#:
```csharp
public class Appointment
{
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; } // Navigation property!
}
```
You can query related data seamlessly:
```csharp
var apt = await _db.Appointments.Include(a => a.Doctor).FirstOrDefaultAsync();
Console.WriteLine(apt.Doctor.Qualifications);
```
