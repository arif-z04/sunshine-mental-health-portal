# Form Handling & Client-Side Validation

Forms collect data defensively before sending HTTP requests.

---

## 1. Bangladesh Phone Number Validation

```javascript
function validateBangladeshPhone(phone) {
  // Matches +8801[3-9] followed by 8 digits, or local 01[3-9] followed by 8 digits
  const bdRegex = /^(?:\+?880|0)?1[3-9]\d{8}$/;
  return bdRegex.test(phone.trim());
}
```

---

## 2. Live Password Strength & Confirmation
Registration forms check for:
* Minimum 8 characters.
* Immediate equality check between password and confirm password inputs before enabling submit buttons.
