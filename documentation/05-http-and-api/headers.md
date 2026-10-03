# HTTP Headers Explained

Headers are key-value pairs providing metadata about the request or response.

---

## 1. Common Request Headers in Sunshine
* `Content-Type: application/json`: Informs the server that the payload is JSON text.
* `Accept: application/json`: Asks server to respond in JSON format.
* `Authorization: Bearer <token>`: Transmits the user's secure digital badge.

## 2. Common Response Headers
* `Set-Cookie: sunshine_token=...; HttpOnly; SameSite=Lax`: Instructs browser to store a secure session cookie.
