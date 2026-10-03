# Modern JavaScript (ES6+) Fundamentals

## 1. What is JavaScript?
**JavaScript** is the programming language of the web browser. It makes web pages interactive by reacting to clicks, validating input, and sending data to the server behind the scenes without refreshing the page.

## 2. Variables: `const` and `let`
```javascript
const doctorId = 1;       // Constant: cannot be reassigned
let selectedTime = null;  // Mutable: can change when user clicks a slot
selectedTime = "16:00:00";
```

## 3. Asynchronous Functions (`async` / `await`)
When talking to a backend server over the network, responses take milliseconds to arrive. JavaScript does not freeze your screen while waiting; it uses `async` and `await`:
```javascript
async function fetchDoctors() {
  const response = await fetch('/api/patient/doctors');
  const doctors = await response.json();
  console.log('Loaded doctors:', doctors);
}
```
