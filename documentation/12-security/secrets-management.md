# Secrets Management & Environment Isolation

Handling passwords, encryption keys, and connection strings securely.

---

## 1. Rules for Secrets
1. **Never commit secrets to Git**: Passwords and JWT secret keys must not be hardcoded in repository files.
2. **Use `.env.example` as a template**: The repository includes a clean `.env.example` showing configuration variable names with placeholder values.
3. **Production Deployment**: Inject secrets via Linux environment variables or systemd service credential vaults.
