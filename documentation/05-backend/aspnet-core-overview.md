# ASP.NET Core 10 Overview

Sunshine's backend is developed with **ASP.NET Core 10** in C#.

---

## 1. What is ASP.NET Core?

ASP.NET Core is Microsoft's high-performance, open-source, cross-platform framework for building modern cloud-connected web applications and REST APIs.
* **Cross-Platform**: Runs natively on Linux (Omarchy), macOS, and Windows.
* **Kestrel Web Server**: Blazing fast, event-driven HTTP server capable of handling millions of requests per second.
* **Unified Pipeline**: Combines dependency injection, middleware routing, and model binding into a clean, modular structure.

---

## 2. Application Entrypoint: `Program.cs`

In modern .NET (.NET 6 through .NET 10), applications use the simplified top-level statements model in `server/Sunshine.App/Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// 1. Configure Services (Dependency Injection)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// 2. Build the App
var app = builder.Build();

// 3. Configure HTTP Request Pipeline (Middleware)
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```
