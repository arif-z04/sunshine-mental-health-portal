# Web Browser & Developer Tools Setup

A modern web browser with developer tools is essential for testing the Sunshine frontend and inspecting network requests.

---

## 1. Recommended Browsers on Omarchy

Install Google Chrome or Chromium:
```bash
sudo pacman -S chromium
```
Or install Firefox:
```bash
sudo pacman -S firefox
```

---

## 2. Developer Tools Essentials

Open any page in your browser and press **F12** (or `Ctrl + Shift + I`):
* **Console Tab**: Displays JavaScript errors, warnings, and log outputs.
* **Network Tab**: Inspects HTTP requests made by the frontend (headers, payloads, status codes, timing).
* **Application / Storage Tab**: Inspects Cookies (like `sunshine_token`), LocalStorage, and SessionStorage.
* **Responsive Design Mode**: Press `Ctrl + Shift + M` to test the website on simulated mobile devices (iPhone, Pixel, iPad).
