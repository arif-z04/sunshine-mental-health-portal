# Using `yay` for AUR Packages

The **Arch User Repository (AUR)** is a community-driven repository containing packages not yet included in official Arch repositories.

---

## 1. What is `yay`?
`yay` (**Yet Another Yogurt**) is an automated AUR helper. It searches both official repositories and the community AUR, downloads source code, compiles it, and installs it cleanly via pacman.

## 2. Installing Software with `yay`
```bash
yay -S visual-studio-code-bin
```
*(Notice: Never type `sudo yay`. `yay` will automatically prompt for administrative permissions only when it is ready to install the compiled package).*
