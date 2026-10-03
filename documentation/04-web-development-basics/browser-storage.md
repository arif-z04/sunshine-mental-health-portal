# Browser Storage: Cookies vs. LocalStorage

Where does the browser save data between page visits?

---

| Storage Type | Characteristics | How Sunshine Uses It |
| :--- | :--- | :--- |
| **HTTP-only Cookie** | Managed automatically by browser; cannot be read by JavaScript; protected against XSS. | Holds `sunshine_token` cookie for safe portal navigation. |
| **`localStorage`** | Key-value store accessible to JavaScript; persists even if browser is closed. | Stores token copy for programmatic API headers. |
| **`sessionStorage`** | Cleared immediately when browser tab closes. | Stores temporary booking draft state. |
