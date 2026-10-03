# Browser Events & User Interaction

## 1. What is an Event?
An **event** is a signal from the browser that something took place—such as a mouse click, keyboard press, form submission, or page resize.

## 2. Listening for Events
```javascript
const bookButton = document.querySelector('#confirm-booking');

bookButton.addEventListener('click', async (event) => {
  event.preventDefault(); // Prevents browser from reloading the page
  await submitBooking();
});
```
