# CSS3 Architecture & Theming

## 1. What is CSS?
**CSS** stands for **Cascading Style Sheets**. It controls the visual design, colors, fonts, spacing, and layout of HTML elements.

## 2. Custom Properties (CSS Variables) in Sunshine
In `server/Sunshine.App/wwwroot/css/main.css`, Sunshine defines a unified healthcare color palette:
```css
:root {
  --primary: #1976d2;        /* Trustworthy Blue */
  --teal: #00897b;           /* Calming Healthcare Accent */
  --danger: #d32f2f;         /* Emergency Crisis Red */
  --surface: #ffffff;        /* Clean White Cards */
  --radius-md: 12px;         /* Friendly Rounded Corners */
}
```
Using variables ensures that if you change `--primary`, the color updates instantly across all doctor cards, buttons, and navigation bars!
