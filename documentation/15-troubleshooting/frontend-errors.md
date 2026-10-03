# Frontend & UI Diagnostic Guide

---

## 1. Modal Dialog Does Not Appear
* **Cause**: Modal DOM element hidden by CSS display property.
* **Fix**: Check that `dialog.showModal()` is invoked or class `active` is toggled.

## 2. Card Content Clipping
* **Fix**: Inspect element in DevTools; ensure `overflow: hidden` is not applied to parent scroll containers.
