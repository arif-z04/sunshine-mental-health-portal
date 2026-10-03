# HTTP Request Pipeline & Middleware

Middleware components sit between the incoming HTTP network socket and your application controllers.

---

## 1. The Request Lifecycle

```text
Incoming HTTP Request
         │
         ▼
[ Exception Handling Middleware ]  <-- Catches unhandled exceptions, returns JSON 500
         │
         ▼
[ Static Files Middleware ]        <-- Serves index.html, CSS, JS directly from wwwroot
         │
         ▼
[ Routing Middleware ]             <-- Matches URL to controller action
         │
         ▼
[ Authentication Middleware ]      <-- Validates JWT token or sunshine_token cookie
         │
         ▼
[ Authorization Middleware ]       <-- Verifies [Authorize(Roles = "DOCTOR")]
         │
         ▼
[ Controller Action Execution ]    <-- Executes business logic & returns response
```

---

## 2. Middleware Registration Order in `Program.cs`

Order is critical in ASP.NET Core:
1. `UseExceptionHandler` or `UseDeveloperExceptionPage` must be first.
2. `UseStaticFiles` precedes routing to quickly serve web assets.
3. `UseAuthentication` must precede `UseAuthorization`.
4. `MapControllers` registers endpoint execution at the end of the pipeline.
