# User Logout & Session Invalidation

How users terminate authenticated sessions.

---

## 1. Logout Endpoint (`POST /api/auth/logout`)

```csharp
[HttpPost("logout")]
public IActionResult Logout()
{
    Response.Cookies.Delete("sunshine_token", new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Lax,
        Path = "/"
    });

    return Ok(new { success = true, message = "Successfully logged out." });
}
```

* Clears the `sunshine_token` cookie from the client's browser.
* The frontend removes the local token copy:
  ```javascript
  localStorage.removeItem('sunshine_token');
  window.location.href = '/';
  ```
