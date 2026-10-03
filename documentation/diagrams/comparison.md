# System Diagram Comparison Guide

Visual software diagrams answer different questions about a software system. Use this guide to understand which diagram to open depending on what you want to learn.

---

## 1. Quick Comparison Table

| Diagram Type | Main Question It Answers | When a Developer Uses It | Real-World Analogy |
| :--- | :--- | :--- | :--- |
| **System Architecture** | *How is the whole system organized?* | Understanding tiers, servers, protocols, and high-level boundaries. | The architectural floor plan of a hospital building. |
| **Entity-Relationship (ERD)** | *How is database data organized?* | Designing tables, foreign keys, columns, and data integrity constraints. | The card catalog in a national library. |
| **UML Class Diagram** | *How is the C# code organized?* | Studying object-oriented relationships, inheritance, and domain models. | The mechanical blueprint of an automobile engine. |
| **UML Use Case Diagram** | *What can each user do?* | Reviewing business requirements and user permissions by role. | The job descriptions of doctors, nurses, and hospital administrators. |
| **Sequence Diagram** | *What happens step by step over time?* | Tracing the chronological flow of messages between client, server, and DB. | The script of a theatrical play showing who speaks in what order. |
| **Data Flow Diagram (DFD)** | *How does information move through the system?* | Tracking input data, internal processing transformations, and storage. | A plumbing schematic showing water flowing from reservoirs through pipes. |

---

## 2. When to Use Which Diagram

* **If you want to know which tables store appointments**:
  Look at [`database/er-diagram.md`](database/er-diagram.md).
* **If you want to see what classes are involved in C#**:
  Look at [`uml/class-diagram.md`](uml/class-diagram.md).
* **If you want to understand the exact network flow when a user clicks "Book"**:
  Look at [`uml/sequence-diagrams.md`](uml/sequence-diagrams.md).
* **If you want to see how data flows from external patients into the booking engine**:
  Look at [`dfd/dfd-level-1.md`](dfd/dfd-level-1.md) and [`dfd/dfd-level-2.md`](dfd/dfd-level-2.md).
* **If you want to see how Nginx, Kestrel, and PostgreSQL run on Linux**:
  Look at [`architecture/deployment-architecture.md`](architecture/deployment-architecture.md).
