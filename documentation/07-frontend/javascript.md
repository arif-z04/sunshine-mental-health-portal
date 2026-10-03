# Vanilla JavaScript Architecture

Client-side behavior uses modern ECMAScript 6+ patterns.

---

## 1. Asynchronous Networking with `fetch`

All API communication uses native `async` / `await` syntax:

```javascript
async function loadSpecialists() {
  try {
    const response = await fetch('/api/patient/doctors');
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    const doctors = await response.json();
    renderDoctorCards(doctors);
  } catch (error) {
    console.error('Failed to load specialists:', error);
    showToast('Failed to load specialists. Please try again.', 'error');
  }
}
```

---

## 2. Event Delegation & DOM Manipulation

Instead of attaching event listeners to hundreds of dynamically generated cards, listeners are bound to container elements using event delegation (`event.target.closest('.btn-book')`), conserving memory.
