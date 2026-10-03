# Automated API Testing with cURL & Scripts

Testing the API endpoints directly from the Omarchy terminal.

---

## 1. Quick Testing with `curl`

Authenticate and retrieve doctor list:
```bash
# Login as patient
LOGIN_RES=$(curl -s -X POST http://localhost:5000/api/auth/login   -H "Content-Type: application/json"   -d '{"email":"anika@example.com","password":"Password123!"}')

TOKEN=$(echo "$LOGIN_RES" | grep -o '"token":"[^"]*' | cut -d'"' -f4)

# Fetch appointments
curl -s http://localhost:5000/api/patient/appointments   -H "Authorization: Bearer $TOKEN" | jq .
```
