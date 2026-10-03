# Tutorial: How to Add a New API Endpoint

Adding a REST endpoint to an existing controller.

---

## Step 1: Define DTOs in `DTOs.cs`
```csharp
public record DoctorReviewDto(int Rating, string Comment, DateTime CreatedAt);
```

## Step 2: Implement Controller Action
```csharp
[HttpGet("{id}/reviews")]
[AllowAnonymous]
public async Task<ActionResult<List<DoctorReviewDto>>> GetDoctorReviews(int id)
{
    var reviews = await _doctorService.GetReviewsAsync(id);
    return Ok(reviews);
}
```

## Step 3: Test via cURL
```bash
curl -s http://localhost:5000/api/patient/doctors/1/reviews | jq .
```
