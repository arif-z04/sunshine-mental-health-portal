# Browser Errors & DevTools Diagnostics

---

## 1. CORS Policy Errors
* If frontend and backend are served from different domains, configure `app.UseCors()` in `Program.cs`. When served together via Kestrel/Nginx, CORS is bypassed as requests are same-origin.

## 2. Cookie Not Set
* Verify that `SameSite=Lax` and `Path=/` are configured on the auth cookie.
