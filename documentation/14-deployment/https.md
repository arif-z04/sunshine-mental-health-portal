# Securing with HTTPS (Certbot & Let's Encrypt)

Enabling SSL/TLS certificates for encrypted communications.

---

## 1. Install Certbot on Omarchy

```bash
sudo pacman -S certbot certbot-nginx
```

## 2. Obtain and Install Certificate

```bash
sudo certbot --nginx -d sunshine.example.org
```
Certbot automatically configures HTTPS port 443 and sets up automatic certificate renewal.
