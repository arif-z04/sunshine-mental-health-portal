# API Authentication & Token Lifecycle

How API clients authenticate requests.

---

## 1. Using Bearer JWT
Include the JWT token in the `Authorization` request header:
```http
GET /api/patient/appointments HTTP/1.1
Host: localhost:5000
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## 2. Using Browser Cookies
When accessing endpoints via web browsers, the backend automatically issues the `sunshine_token` cookie with `HttpOnly; SameSite=Lax`. No manual header injection is required for standard browser interactions.
