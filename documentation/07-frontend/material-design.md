# Material Design / Material UI Implementation

Sunshine implements Google Material Design principles to deliver a clean, trustworthy healthcare experience.

---

## 1. Material Core Elements

* **Surface Elevation**: Cards, modals, and app bars use subtle box shadows (`--shadow-sm`, `--shadow-md`) against clean white `#ffffff` backgrounds.
* **Rounded Corners**: 8px to 12px border radius across cards and inputs to provide a friendly, calming aesthetic.
* **Interactive Ripple Effects**: Buttons transition background color smoothly on hover and active states.
* **Status Badges**: Distinct pastel pill badges indicating appointment state:
  - `CONFIRMED`: Green background (`#e8f5e9`, text `#2e7d32`).
  - `PENDING`: Amber background (`#fff8e1`, text `#f57f17`).
  - `CANCELLED`: Red background (`#ffebee`, text `#c62828`).
