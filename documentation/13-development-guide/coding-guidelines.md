# Coding Standards & Guidelines

Maintaining clean, readable, and robust code across Sunshine.

---

## 1. C# & .NET Standards
* Use modern C# 13 features (records, pattern matching, top-level statements, nullable reference types).
* Name classes with `PascalCase`, parameters with `camelCase`, and private fields with `_camelCase`.
* Always use `async` / `await` for I/O operations (`ToListAsync()`, `SaveChangesAsync()`).
* Always disallow raw SQL string interpolation to prevent injection.

## 2. Frontend Standards
* Semantic HTML5 markup.
* CSS custom properties (`--primary`, `--shadow-md`) for consistent theming.
* Clean vanilla JavaScript with no global variable leaks (wrap in modules or IIFEs).
