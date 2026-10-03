# Form Handling & Validation

Forms allow patients and clinicians to input data.

---

## 1. Defensive Client-Side Validation
Before sending an HTTP request across the network, validate input locally:
```javascript
function validatePhone(phone) {
  // Matches Bangladesh mobile numbers: +8801[3-9] followed by 8 digits
  const bdRegex = /^(?:\+?880|0)?1[3-9]\d{8}$/;
  if (!bdRegex.test(phone.trim())) {
    alert('Please enter a valid Bangladesh mobile number (e.g. 01711000000)');
    return false;
  }
  return true;
}
```
