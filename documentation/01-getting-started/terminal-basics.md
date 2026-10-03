# Linux Terminal Basics for Beginners

If you are new to the Linux terminal, this guide explains the core commands used throughout the Sunshine project.

---

## 1. Navigating the File System

* **`pwd`** (*Print Working Directory*): Shows your current absolute directory location.
  ```bash
  pwd
  # Example output: /home/noir/Desktop/Another-Sunshine-Project
  ```
* **`ls`** (*List*): Displays files and folders in the current directory.
  ```bash
  ls -la
  # -l shows detailed permissions, file sizes, and modification dates
  # -a includes hidden files (files starting with a dot, like .env.example)
  ```
* **`cd`** (*Change Directory*): Navigates between folders.
  ```bash
  cd server/Sunshine.App   # Move into the server application folder
  cd ..                   # Move up one level
  cd ~                    # Jump directly to your home directory (/home/username)
  ```

---

## 2. Managing Files & Folders

* **`mkdir`** (*Make Directory*): Creates a new folder.
  ```bash
  mkdir -p my_folder/subfolder
  # -p creates parent folders automatically without throwing errors if they already exist
  ```
* **`cp`** (*Copy*): Copies files or directories.
  ```bash
  cp .env.example .env    # Copies the example environment file to an active .env
  ```
* **`rm`** (*Remove*): Deletes files.
  ```bash
  rm file.txt             # Removes file.txt
  rm -rf test_dir/        # Force-removes a directory and all its contents
  ```
* **`cat`** (*Concatenate*): Prints file content directly into the terminal.
  ```bash
  cat appsettings.json
  ```

---

## 3. System Privileges: What is `sudo`?

Commands like package installation and starting system services require **root (administrator)** privileges.
* **`sudo`** stands for *Superuser Do*.
* When you prefix a command with `sudo` (e.g. `sudo pacman -S git`), Linux prompts you for your user password and runs the command with administrative authority.
* **Never** run `dotnet run` or everyday development code with `sudo`; only use `sudo` for system package installation and system daemon management.
