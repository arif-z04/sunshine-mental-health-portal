# Sunshine Visual System Modeling & Diagrams

Visual software architecture models and workflows for the **Sunshine Mental Health Portal**.

---

## 1. Diagram Catalog & Index

| Category | Diagram File | Modeling Standard | Focus & Scope |
| :--- | :--- | :--- | :--- |
| **Guide** | [`comparison.md`](comparison.md) | Comparative Guide | Explains what each diagram type means and when to use it |
| **Architecture** | [`architecture/overall-architecture.md`](architecture/overall-architecture.md) | High-Level Flowchart | Browser, Kestrel, Controllers, Services, EF Core & PostgreSQL |
| **Architecture** | [`architecture/backend-architecture.md`](architecture/backend-architecture.md) | Layered Block Diagram | ASP.NET Core DI Container, Middleware Pipeline & Scoped Services |
| **Architecture** | [`architecture/frontend-architecture.md`](architecture/frontend-architecture.md) | Component Diagram | Vanilla JS modules, Material UI design system, Modal dialogs |
| **Architecture** | [`architecture/deployment-architecture.md`](architecture/deployment-architecture.md) | Infrastructure Diagram | Omarchy Linux host, Nginx reverse proxy, Systemd & PostgreSQL |
| **Database** | [`database/er-diagram.md`](database/er-diagram.md) | Mermaid ERD | Complete 16-table relational schema with PK/FK cardinality |
| **Database** | [`database/database-overview.md`](database/database-overview.md) | Schema Grouping Map | Entity clusters (Identity, Clinical, Financial, Digital Vault) |
| **Database** | [`database/relationships.md`](database/relationships.md) | Relational Mapping | 1:1, 1:N, and N:M mappings with foreign key integrity |
| **UML** | [`uml/class-diagram.md`](uml/class-diagram.md) | UML Class Diagram | Domain Entities, DTOs, ApplicationDbContext & Services |
| **UML** | [`uml/use-case-diagram.md`](uml/use-case-diagram.md) | UML Use Case | Actors (Patient, Doctor, Admin) and system boundary use cases |
| **UML** | [`uml/sequence-diagrams.md`](uml/sequence-diagrams.md) | UML Sequence Diagram | Appointment booking & bKash MFS payment workflows |
| **Data Flow** | [`dfd/dfd-level-0.md`](dfd/dfd-level-0.md) | Context Level DFD | System boundary with Patient, Doctor, Admin external entities |
| **Data Flow** | [`dfd/dfd-level-1.md`](dfd/dfd-level-1.md) | Process Level 1 DFD | Decomposition into 7 core processes and 6 data stores |
| **Data Flow** | [`dfd/dfd-level-2.md`](dfd/dfd-level-2.md) | Detailed Level 2 DFD | Deep-dive into 3.0 Appointment Booking & Concurrency Control |
