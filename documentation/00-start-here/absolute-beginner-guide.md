# Absolute Beginner Guide: How Software Actually Works

If you have ever wondered what actually happens when you double-click an icon, open a website, or click a button on your phone, read this first.

---

## 1. What is Hardware vs. Software?
* **Hardware**: The physical parts of a computer you can touch—the screen, the keyboard, the processor (CPU), memory (RAM), and storage disk.
* **Software**: The set of instructions telling the physical hardware what to do. Hardware without software is like a piano with nobody playing it. Software is the musical sheet instructing the piano which keys to strike.

---

## 2. What is Source Code?
Computers only understand electrical switches that are either on or off—represented as binary numbers (`1`s and `0`s). Because humans cannot write millions of ones and zeros without losing their minds, we invented **programming languages** like **C#** and **JavaScript**.

Programming languages look similar to structured English:
```csharp
if (user.IsActive == true)
{
    Console.WriteLine("Welcome back!");
}
```
A special tool called a **compiler** translates this human-readable text into machine instructions the computer processor executes.

---

## 3. The Three Layers of Every Modern Web Application

Imagine ordering food at a restaurant:
1. **The Dining Table (Frontend / Client)**:
   This is what the customer sees and touches—the menu, the table setting, the physical buttons on the napkin dispenser. In web development, this is **HTML, CSS, and JavaScript** running inside your web browser.
2. **The Waiter (API & Backend Server)**:
   The customer doesn't walk into the kitchen to cook their own food. They tell the waiter: *"I would like chicken biryani, please."* The waiter takes the order, verifies it, carries it to the kitchen, and returns with the dish. In our project, the waiter is **ASP.NET Core 10**.
3. **The Kitchen & Pantry (Database)**:
   Where the ingredients, recipes, and food are stored and prepared. The waiter asks the chef to fetch ingredients. In our project, the pantry is the **PostgreSQL 18** database storing user passwords, doctor profiles, and appointment ledgers.
