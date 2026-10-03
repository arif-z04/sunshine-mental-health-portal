# Files & Directories Command Reference

Detailed breakdown of everyday terminal commands.

---

### 1. `pwd` (Print Working Directory)
Shows where you are currently located:
```bash
pwd
# Example: /home/noir/Desktop/Another-Sunshine-Project
```

### 2. `ls` (List)
Lists files in the current folder:
```bash
ls -la
```
* `-l`: Uses long format (showing permissions, file size in bytes, and last modification date).
* `-a`: Shows hidden files (files beginning with a dot, like `.env.example` or `.gitignore`).

### 3. `cd` (Change Directory)
Navigates through folders:
```bash
cd server/Sunshine.App   # Enter subfolder
cd ..                   # Move up one level
cd ~                    # Jump straight to your personal home folder (/home/noir)
```

### 4. `mkdir` (Make Directory)
```bash
mkdir -p my_folder/sub_folder
```
* `-p`: Creates parent folders automatically if they don't already exist.

### 5. `cp` (Copy) and `mv` (Move / Rename)
```bash
cp .env.example .env    # Copies example config to active config
mv old_name.txt new.txt # Renames a file
```

### 6. `rm` (Remove)
```bash
rm temp.txt             # Deletes a file
rm -rf temp_dir/        # Deletes a directory and all files inside (Use with caution!)
```
