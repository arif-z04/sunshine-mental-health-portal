# What is HTTP?

## 1. What is it?
**HTTP** stands for **HyperText Transfer Protocol**. It is the set of rules governing how web browsers and servers format and transmit messages.

## 2. Core Components of an HTTP Message
1. **Method (Verb)**: Tells the server what action to perform:
   - `GET`: "Retrieve data for me."
   - `POST`: "Here is new data, please save or process it."
   - `PUT`: "Update this existing data."
   - `DELETE`: "Remove this record."
2. **URL (Uniform Resource Locator)**: The web address (e.g. `http://localhost:5000/api/patient/appointments`).
3. **Headers**: Metadata (e.g. `Content-Type: application/json`, `Authorization: Bearer ...`).
4. **Body**: The payload data being transmitted.
