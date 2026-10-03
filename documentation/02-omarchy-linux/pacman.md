# The `pacman` Package Manager

`pacman` is the lightning-fast official package manager for Arch Linux and Omarchy.

---

## 1. Upgrading the Entire System
```bash
sudo pacman -Syu
```
* `sudo`: Execute as administrator.
* `pacman`: The package manager tool.
* `-S`: Synchronize with official online repositories.
* `y`: Refresh (download) the latest package index databases.
* `u`: Upgrade all installed packages to their newest releases.

---

## 2. Installing Packages
```bash
sudo pacman -S git postgresql dotnet-sdk
```

---

## 3. Searching for Packages
```bash
pacman -Ss postgresql
```
* Searches the repository package catalog for any software containing the word "postgresql".

---

## 4. Removing Packages Cleanly
```bash
sudo pacman -Rns package_name
```
* `-R`: Remove.
* `-n`: Remove backup configuration files.
* `-s`: Recursively remove dependencies that are no longer used by any other program.
