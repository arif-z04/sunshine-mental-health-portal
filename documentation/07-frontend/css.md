# CSS3 Architecture & Custom Properties

All styling in Sunshine is organized using pure CSS3 custom properties (CSS variables) to enforce a unified Material theme.

---

## 1. Theme Color Palette & Typography

```css
:root {
  /* Brand Primary Colors */
  --primary: #1976d2;        /* Material Indigo/Blue */
  --primary-dark: #115293;
  --primary-light: #e3f2fd;
  
  /* Healthcare Accents */
  --teal: #00897b;
  --teal-light: #e0f2f1;
  
  /* Emergency / Crisis Alerts */
  --danger: #d32f2f;
  --danger-light: #ffebee;
  
  /* Surface and Neutral Tones */
  --surface: #ffffff;
  --background: #f8fafc;
  --text-primary: #1e293b;
  --text-secondary: #64748b;
  
  /* Elevation Shadows */
  --shadow-sm: 0 1px 3px rgba(0,0,0,0.08);
  --shadow-md: 0 4px 6px -1px rgba(0,0,0,0.1);
  --shadow-lg: 0 10px 15px -3px rgba(0,0,0,0.1);
  
  /* Geometry */
  --radius-sm: 6px;
  --radius-md: 12px;
  --radius-lg: 16px;
}
```
