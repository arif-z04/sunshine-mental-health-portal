# Responsive Design for Mobile Devices

In Bangladesh, over 80% of healthcare portal traffic originates from Android smartphones.

---

## 1. Mobile-First CSS Media Queries
```css
/* Default: Mobile phone (Single column) */
.specialist-grid {
  display: grid;
  grid-template-columns: 1fr;
  gap: 1rem;
}

/* Tablet (2 columns) */
@media (min-width: 768px) {
  .specialist-grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

/* Desktop (3 columns) */
@media (min-width: 1024px) {
  .specialist-grid {
    grid-template-columns: repeat(3, 1fr);
  }
}
```
