# The Client-Server Model

## 1. What is it?
The **Client-Server model** is the foundational architecture of the Internet where tasks are partitioned between service requesters (**clients**) and service providers (**servers**).

```text
[ Client: Web Browser ]  ────── HTTP Request (GET /api/patient/doctors) ─────>  [ Server: Kestrel ]
[ On Phone / Laptop   ]  <───── HTTP Response (JSON Doctor List)  ────────────  [ ASP.NET Core 10 ]
```

## 2. Real-World Analogy
* **Client**: A customer calling a pharmacy to ask if a medication is in stock.
* **Server**: The pharmacist who checks the shelves and answers the question.
