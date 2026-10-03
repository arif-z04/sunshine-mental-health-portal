# Deciphering `Program.cs`

`Program.cs` is the front door of the backend application:

---

```csharp
var builder = WebApplication.CreateBuilder(args);

// 1. Register Services with Dependency Injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddControllers();

var app = builder.Build();

// 2. Configure HTTP Pipeline (Middleware)
app.UseStaticFiles();      // Serve wwwroot files (HTML, CSS, JS)
app.UseAuthentication();   // Check user identity
app.UseAuthorization();    // Check user permissions
app.MapControllers();      // Route /api/... URLs to Controller classes

app.Run();                 // Start listening on port 5000!
```
