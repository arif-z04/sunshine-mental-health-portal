# The Document Object Model (DOM)

## 1. What is the DOM?
The **DOM** is the browser's internal tree representation of your HTML code. When the browser reads your HTML file, it converts every tag into a JavaScript object you can inspect, modify, or delete dynamically.

## 2. Selecting and Updating DOM Elements
```javascript
// Find element by its ID
const banner = document.getElementById('emergency-banner');

// Change its visible text
banner.textContent = 'Crisis Hotlines: Call 16263 or +8801779554391';

// Add a CSS class
banner.classList.add('urgent');
```
