# Mastering Browser DevTools (`F12`)

## 1. The Tabs You Need
* **Console**: Shows JavaScript runtime errors, exceptions, and `console.log` statements.
* **Network**: Displays every HTTP request, headers, query parameters, status codes, and JSON response bodies.
* **Application / Storage**: Inspects cookies (`sunshine_token`), localStorage, and sessionStorage.
* **Elements**: Inspects and edits the live HTML and CSS rules.

## 2. Debugging Flow When Clicking a Button Fails:
```text
Click "Book Session" does nothing
              │
              ▼
1. Look at Console Tab (Did JavaScript crash with an error?)
              │
              ▼
2. Look at Network Tab (Did an HTTP request fire? Did it return 400 or 500?)
              │
              ▼
3. Look at Response Preview (What is the server error message?)
              │
              ▼
4. Check Terminal (What does ASP.NET Core log?)
```
