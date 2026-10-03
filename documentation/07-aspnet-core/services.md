# Domain Services: The Business Engine

Why business logic belongs in Services, not Controllers:

---

* **Controllers** are responsible for HTTP: checking headers, returning status codes (`200`, `400`), and formatting JSON.
* **Services** are responsible for Healthcare Rules:
  - Is the requested slot between 16:00 and 20:30 BST?
  - Does the doctor already have a confirmed session?
  - Is the doctor's policy ADVANCE or POST_PAYMENT?
  - Should we trigger a 15-minute reservation countdown timer?
