# API Status Code Troubleshooting

---

* **401 Unauthorized**: Missing Bearer token or expired cookie. Log in again via `/api/auth/login`.
* **403 Forbidden**: Logged-in user lacks role permissions (e.g. Patient accessing Doctor API).
* **404 Not Found**: Resource ID does not exist or URL path has a typo.
* **500 Internal Server Error**: Check server console logs for exact stack trace.
