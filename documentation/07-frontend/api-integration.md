# Connecting Frontend to Backend APIs

How client JavaScript communicates with ASP.NET Core endpoints.

---

## 1. Authentication Headers & Credentials

API requests include both JWT Bearer headers and automatic cookie transport:

```javascript
async function apiRequest(url, options = {}) {
  const token = localStorage.getItem('sunshine_token');
  const headers = {
    'Content-Type': 'application/json',
    ...(token ? { 'Authorization': `Bearer ${token}` } : {}),
    ...options.headers
  };

  const response = await fetch(url, {
    ...options,
    headers,
    credentials: 'include' // Ensures sunshine_token HTTP-only cookie is transmitted
  });

  if (response.status === 401) {
    // Redirect to login if unauthorized
    window.location.href = '/patient#login';
    return null;
  }

  return response;
}
```
