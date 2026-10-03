# Data Transfer Objects (DTOs)

## 1. What is a DTO?
A **DTO** is a simple object used solely to carry data between the client and server.

## 2. Why Not Send Database Entities to the Browser?
* **Security**: If you send the `User` entity to the browser, you risk accidentally exposing the `PasswordHash` column!
* **Decoupling**: You can change your database table structure without breaking your client's mobile app or web frontend.
