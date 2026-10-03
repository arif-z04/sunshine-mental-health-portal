# What is a Framework?

## 1. What is it?
A **framework** is a comprehensive software platform providing a pre-built architectural skeleton for building applications.

## 2. The Hollywood Principle: "Don't Call Us, We'll Call You"
* In ordinary programming, your code controls the flow and calls small helper functions when needed.
* In a framework like **ASP.NET Core**, the framework controls the main loop (listening on network sockets, parsing HTTP headers). The framework calls *your* controller methods when an incoming request matches a route.
