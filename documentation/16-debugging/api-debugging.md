# API Debugging: Isolating Network Issues

Test the backend directly using `curl` to rule out frontend JavaScript issues:
```bash
curl -i -X POST http://localhost:5000/api/auth/login   -H "Content-Type: application/json"   -d '{"email":"admin@sunshine.org","password":"Password123!"}'
```
