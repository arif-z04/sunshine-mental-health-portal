# Classes: The Blueprint of Objects

## 1. What is a Class?
A **class** is a blueprint or template defining the properties and behaviors that objects created from it will have.

## 2. Example: `Doctor` in `Entities.cs`
```csharp
public class Doctor
{
    public int Id { get; set; }
    public string BmdcNumber { get; set; } = string.Empty;
    public string Qualifications { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }
    public bool IsVerified { get; set; }
}
```
