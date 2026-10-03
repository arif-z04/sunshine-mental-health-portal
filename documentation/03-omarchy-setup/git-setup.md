# Git Setup on Omarchy Linux

Version control is essential for software engineering. Here is how to configure Git for Sunshine.

---

## 1. Install Git

```bash
sudo pacman -S git
```

Verify installation:
```bash
git --version
# Output: git version 2.4x.x
```

---

## 2. Configure Global Identity

Set your author name and email. These values will be attached to every commit you make:

```bash
git config --global user.name "Your Full Name"
git config --global user.email "your.email@example.com"
```

Configure default branch name to `main`:
```bash
git config --global init.defaultBranch main
```

Check your configuration:
```bash
git config --list
```

---

## 3. Essential Git Vocabulary for Beginners

* **Repository**: The project database containing all files and complete change history.
* **Commit**: A snapshot of your project at a specific point in time with a descriptive message.
* **Branch**: An independent line of development allowing you to work on features without breaking main code.
* **Working Tree**: Your active files on disk.
* **Staging Area**: Files marked to be included in the next commit (`git add`).
* **`.gitignore`**: A file specifying which files (build binaries, logs, secrets) Git should ignore.
