# Package Management with Pacman & Yay

This guide explains how to install and update packages on Omarchy Linux.

---

## 1. Updating the System

Before installing new development tools, synchronize the local package database with upstream mirrors and upgrade all packages:

```bash
sudo pacman -Syu
```
* `sudo`: Executes the command with root administrative authority.
* `pacman`: The Arch Linux package manager.
* `-S`: Synchronize packages from official repositories.
* `y`: Refresh the package lists from remote servers (*download fresh database*).
* `u`: Upgrade all installed packages to their latest versions.

---

## 2. Installing Packages

To install software, use `-S` followed by the package name:

```bash
sudo pacman -S git postgresql curl
```

To search for a package when you are not sure of its exact name:
```bash
pacman -Ss dotnet-sdk
```

To remove a package cleanly along with its unused dependencies:
```bash
sudo pacman -Rns package_name
# -R: Remove
# -n: Remove configuration files saved by pacman
# -s: Recursively remove dependencies that are no longer needed
```

---

## 3. Using the AUR Helper (`yay`)

If a specific development tool is not in the core Arch repositories, it is likely in the AUR:
```bash
yay -S visual-studio-code-bin
```
*(Notice: Never run `yay` with `sudo`; `yay` will automatically prompt for your root password when it needs to install the built package).*
