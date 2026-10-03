# Frontend Architecture Overview

The Sunshine Mental Health Portal user interface is built using standard, framework-free web technologies: **HTML5, CSS3, and modern JavaScript (ES6+)**.

---

## 1. Why No Heavy JavaScript Frameworks?

Many modern web apps depend on complex build pipelines (React, Vue, Webpack, Vite, Node.js). Sunshine deliberately uses pure vanilla web standards:
1. **Zero Build Step**: No `npm install`, no compilation delays, and no `node_modules` security bloat. Edit an HTML or CSS file, refresh the browser, and see changes immediately.
2. **Instant Performance**: Pages load in milliseconds, even on modest 3G/4G cellular connections across rural Bangladesh.
3. **Long-Term Maintainability**: Pure web standards remain compatible across decades without breaking library upgrades.
4. **Beginner Accessibility**: Any developer who knows basic HTML, CSS, and JavaScript can inspect and modify the frontend immediately.

---

## 2. Directory Layout in `wwwroot`

```text
server/Sunshine.App/wwwroot/
├── index.html            # Public landing page (Hero, Hotlines, Specialties, FAQ)
├── admin/
│   ├── index.html        # Admin Operations Console
│   └── login.html        # Isolated Admin Login
├── doctor/
│   └── index.html        # Doctor Practice Portal
├── patient/
│   └── index.html        # Patient Care Portal (Specialists, Booking, Vault, Profile)
├── css/
│   └── main.css          # Shared Material UI styles & theme variables
└── js/
    └── api.js            # Shared HTTP fetch helpers & token managers
```
