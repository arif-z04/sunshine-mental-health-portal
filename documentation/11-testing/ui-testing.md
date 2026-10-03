# Manual UI & Cross-Browser Testing Guide

Checklist for verifying frontend visual fidelity.

---

## 1. Desktop Browser Testing
Test the application across:
* Google Chrome / Chromium (Linux)
* Mozilla Firefox (Linux)
* Microsoft Edge (Windows / macOS)

### Verification Checklist:
* [ ] Crisis emergency banner displays at the very top of the page.
* [ ] Navigation links jump smoothly to corresponding sections.
* [ ] Modal overlays (bKash checkout, slot picker) appear centered with clean backdrop blur.
* [ ] Toasts appear at the bottom right and disappear automatically after 4 seconds.
* [ ] Zero JavaScript console errors in DevTools (`F12`).
