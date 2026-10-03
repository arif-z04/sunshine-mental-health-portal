# Frontend vs. Backend vs. Database

Understanding the classic three-tier software architecture.

---

| Layer | What It Is | Technologies Used in Sunshine | Location in Repository |
| :--- | :--- | :--- | :--- |
| **Frontend** | The visual user interface running inside the user's browser. | HTML5, CSS3, JavaScript, Material UI | `server/Sunshine.App/wwwroot/` |
| **Backend** | The business logic engine running on the server. | C# 13, ASP.NET Core 10, Entity Framework Core | `server/Sunshine.App/Controllers/`, `Services/` |
| **Database** | The permanent, reliable electronic storage vault. | PostgreSQL 18, SQL | `sql/`, PostgreSQL engine |

---

## Why Separate Them?
1. **Security**: Sensitive medical notes and password hashes must never live in the user's browser where anyone can open DevTools and alter them.
2. **Reliability**: If a user's phone runs out of battery mid-session, their appointment record is already safely persisted on the server's PostgreSQL database.
