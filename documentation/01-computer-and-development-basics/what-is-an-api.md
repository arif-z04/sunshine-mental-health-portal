# What is an API?

## 1. What is it?
**API** stands for **Application Programming Interface**. It is a formal contract of URLs, request formats, and response formats allowing two computer programs to talk to each other.

## 2. Real-World Analogy: The Electrical Wall Socket
You do not need to understand how the power station generates electricity or splice bare copper wires to charge your phone. You simply plug your standard power adapter into the standard wall socket.
The wall socket is an API: a standardized interface hiding complex inner workings.

## 3. How Sunshine Uses APIs
Our frontend JavaScript never connects directly to the PostgreSQL database. Instead, it calls standard APIs:
* `GET /api/patient/doctors` -> "Please give me the list of certified doctors."
* `POST /api/patient/appointments` -> "Please book a slot for October 22nd at 16:00."
