# API Security & Cross-Site Protection

Securing HTTP interfaces against common web vulnerabilities.

---

## 1. Cross-Site Scripting (XSS) Mitigation
* The `sunshine_token` cookie is marked `HttpOnly`, making it invisible to JavaScript running in the browser.
* Frontend renders dynamic content using `.textContent` rather than `.innerHTML` whenever displaying user-supplied strings.

---

## 2. Cross-Site Request Forgery (CSRF) Mitigation
* All authentication cookies specify `SameSite=Lax`. Browsers will not send the cookie on cross-site POST requests.
* Critical actions require explicit Bearer tokens or JSON POST bodies, which standard HTML form submissions cannot forge.
