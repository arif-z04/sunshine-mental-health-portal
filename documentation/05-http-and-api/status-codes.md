# HTTP Status Codes: The Server's Traffic Lights

Status codes are 3-digit numbers categorizing response outcomes:

---

### 2xx: Success
* `200 OK`: Request succeeded.
* `201 Created`: Resource successfully saved.

### 4xx: Client Mistakes
* `400 Bad Request`: Invalid data (e.g. malformed phone number).
* `401 Unauthorized`: Not logged in.
* `403 Forbidden`: Logged in, but lacking permission (e.g. Patient trying to access Admin console).
* `404 Not Found`: Requested item doesn't exist.

### 5xx: Server Problems
* `500 Internal Server Error`: An unhandled C# exception occurred on the server.
