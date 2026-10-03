# Introduction to Sunshine Mental Health Portal

Welcome to the **Sunshine Mental Health Portal** engineering documentation! Sunshine is a specialized, production-ready telehealth and tele-counseling web application tailored specifically for the socio-cultural, medical, and financial landscape of **Bangladesh**.

---

## 1. What is the Sunshine Mental Health Portal?

Mental health remains a deeply stigmatized and underserved healthcare domain in Bangladesh. Patients often struggle to find licensed, BMDC-certified (Bangladesh Medical & Dental Council) psychiatrists and clinical psychologists. Furthermore, the financial barrier, lack of anonymous access, and inability to seamlessly pay using local Mobile Financial Services (MFS) like **bKash (বিকাশ)**, **Nagad (নগদ)**, and **Rocket (রকেট)** prevent thousands from seeking help.

Sunshine solves these critical challenges by offering:
1. **Certified Specialist Directory**: Vetted psychiatrists (MD, FCPS) and certified psychological counselors with explicit clinical qualifications.
2. **Bangladesh Standard Time (BST, UTC+06:00) Evening Clinics**: Working professionals and university students can consult specialists from 16:00 to 20:30 BST (Saturday through Thursday).
3. **MFS Payment Integration**: Instant advance reservations and digital resource access using local mobile wallets in Bangladeshi Taka (৳ BDT).
4. **Emergency Crisis Integration**: Direct zero-latency access to emotional support hotlines such as **কান পেতে রই (Kaan Pete Roi)**, **999**, and **16263**.
5. **Therapeutic Digital Resource Vault**: Evidence-based psychoeducation workbooks (including Bengali CBT worksheets) gated behind affordable subscription tiers.

---

## 2. Target Audience for this Documentation

This documentation is written for **beginner software developers, junior engineers, and students** who understand basic programming concepts (variables, functions, loops) but are learning:
* How modern backend web applications work using **ASP.NET Core 10** and **C#**.
* How to design relational databases and write structured SQL in **PostgreSQL 18**.
* How an Object-Relational Mapper (ORM) like **Entity Framework Core** connects code to databases.
* How to craft responsive, accessible frontends using pure **HTML5, CSS3, and JavaScript** without bulky JavaScript frameworks.
* How to build and run software reliably on **Omarchy Linux** (an Arch Linux-based distribution).

---

## 3. High-Level System Architecture

Sunshine employs a clean, layered architecture separating user presentation, API routing, business domain logic, and data persistence:

```text
+--------------------------------------------------------------+
|                        Browser Client                        |
|  - HTML5 / CSS3 (Material UI Design System)                  |
|  - Vanilla JavaScript ES6+ fetch API                         |
+--------------------------------------------------------------+
                               |
                        HTTP / HTTPS JSON
                               v
+--------------------------------------------------------------+
|                   ASP.NET Core 10 Web API                    |
|  - Controllers: Auth, Patient, Doctor, Admin, Payment        |
|  - Middleware: Exception Handling, Auth Cookies, JWT         |
+--------------------------------------------------------------+
                               |
                        Method Invocations
                               v
+--------------------------------------------------------------+
|                     Business Domain Layer                    |
|  - Services: Appointment, Payment, Subscription, Audit       |
|  - DTOs (Data Transfer Objects)                              |
+--------------------------------------------------------------+
                               |
                     LINQ & Object Mapping
                               v
+--------------------------------------------------------------+
|                    Entity Framework Core                     |
|  - ApplicationDbContext                                      |
|  - Npgsql Provider (Npgsql.EntityFrameworkCore.PostgreSQL)   |
+--------------------------------------------------------------+
                               |
                         SQL Statements
                               v
+--------------------------------------------------------------+
|                    PostgreSQL 18 Database                    |
|  - 16 Relational Tables, Constraints, Indexes                |
|  - Partial Unique Index preventing double-booking            |
+--------------------------------------------------------------+
```

---

## 4. How to Read This Documentation

Follow the chapters in sequential order:
1. **01-getting-started**: Understand prerequisites and launch the app in 5 minutes.
2. **02-project-overview**: Inspect features, multi-portal architecture, and folder layout.
3. **03-omarchy-setup**: Set up your Arch/Omarchy developer workstation cleanly.
4. **04-database & 06-entity-framework**: Understand the relational schema, constraints, and EF Core mappings.
5. **05-backend & 07-frontend**: Learn how controllers, services, and frontend UI work together.
6. **08-authentication to 12-security**: Master JWT, password hashing, and role-based access control.
7. **11-testing**: Run the 26 automated unit/integration tests and bash E2E verification suites.
8. **diagrams**: Review visual Mermaid diagrams of every flow, ERD, and class relationship.
