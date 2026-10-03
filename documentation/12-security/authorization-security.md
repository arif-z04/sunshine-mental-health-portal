# Authorization Security & Privilege Escalation Defense

Preventing unauthorized access to administrative and clinical operations.

---

## 1. Isolated Admin Route Security
The dedicated administrative route `/api/admin/login` strictly checks user role:
```csharp
if (user.Role != AppRoles.Admin)
{
    return StatusCode(StatusCodes.Status403Forbidden, new { message = "Access denied. Administrator privileges required." });
}
```
Non-admin users cannot bypass this check, even with valid credentials for other portals.
