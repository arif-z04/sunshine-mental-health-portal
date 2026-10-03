# Development Environment Setup

Setting up a comfortable, productive local coding environment is essential for working on Sunshine.

---

## 1. Recommended Code Editors

You can use any text editor or Integrated Development Environment (IDE) that supports C# and web standards:

1. **Visual Studio Code / VSCodium**:
   - Install via pacman/AUR: `sudo pacman -S code` (or `yay -S vscodium-bin`).
   - Essential extensions:
     - *C# Dev Kit* or *OmniSharp* (provides syntax highlighting, auto-completion, and debugging).
     - *PostgreSQL* extension (provides SQL query runner inside the IDE).
     - *Prettier* or *HTML CSS Support* (formats web markup).
2. **JetBrains Rider**:
   - Outstanding professional cross-platform .NET IDE.
3. **Neovim / Vim**:
   - For terminal power-users using `nvim-lspconfig`, `mason.nvim`, and `omnisharp`.

---

## 2. Recommended Terminal & Shell

Omarchy Linux typically ships with modern shells such as `bash` or `zsh`:
* Use terminal emulators like `kitty`, `alacritty`, `foot`, or `ghostty`.
* Configure your terminal font with a Nerd Font (e.g., `FiraCode Nerd Font` or `JetBrainsMono Nerd Font`) to render icons and symbols properly.

---

## 3. Directory Layout Recommendation

We recommend placing projects inside your user's home directory:

```text
/home/noir/Desktop/Another-Sunshine-Project/
```

Avoid placing project files inside system root directories (`/tmp`, `/usr`, or `/var`) to prevent permission conflicts and automatic cleaning policies.
