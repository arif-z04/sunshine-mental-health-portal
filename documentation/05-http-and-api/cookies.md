# How Cookies Work

## 1. What is a Cookie?
A **cookie** is a small piece of data stored in the browser by the server. On subsequent requests to the same server, the browser attaches the cookie automatically.

## 2. Security Flags Used by Sunshine
* **`HttpOnly`**: JavaScript running in the browser cannot read the cookie value. This protects user sessions if a malicious script runs on the page.
* **`SameSite=Lax`**: The browser will not send the cookie during cross-site requests, mitigating Cross-Site Request Forgery (CSRF).
