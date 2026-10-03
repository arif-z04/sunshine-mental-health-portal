# Responsive Design & Mobile Viewports

Mental health patients in Bangladesh frequently access portals via mobile smartphones.

---

## 1. CSS Media Queries

Sunshine utilizes fluid CSS grids and responsive breakpoints:

```css
/* Mobile phones (Default) */
.container {
  padding: 1rem;
}
.grid-cards {
  grid-template-columns: 1fr;
}

/* Tablets (>= 768px) */
@media (min-width: 768px) {
  .grid-cards {
    grid-template-columns: repeat(2, 1fr);
  }
}

/* Desktop Displays (>= 1024px) */
@media (min-width: 1024px) {
  .grid-cards {
    grid-template-columns: repeat(3, 1fr);
  }
}
```

---

## 2. Mobile Touch Targets & Navigation Drawer
* All interactive touch targets (buttons, slot chips, form inputs) are at least 44x44 pixels to ensure comfortable tapping on touchscreens.
* Mobile navigation utilizes a sliding drawer triggered by a hamburger menu icon.
