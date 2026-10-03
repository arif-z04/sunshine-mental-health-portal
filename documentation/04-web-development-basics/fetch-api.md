# The `fetch()` API: Talking to Servers

## 1. What is `fetch()`?
`fetch()` is the native JavaScript function used to send HTTP requests to servers and receive responses.

## 2. Sending JSON Data (POST Request)
```javascript
async function bookAppointment(doctorId, date, slot) {
  const response = await fetch('/api/patient/appointments', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('sunshine_token')}`
    },
    body: JSON.stringify({
      doctorId: doctorId,
      appointmentDate: date,
      startTime: slot
    })
  });

  const result = await response.json();
  if (response.ok) {
    alert('Booking confirmed! ID: ' + result.id);
  } else {
    alert('Booking failed: ' + result.message);
  }
}
```
