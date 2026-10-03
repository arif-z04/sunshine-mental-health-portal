# Middleware Pipeline Explained

## 1. What is Middleware?
**Middleware** is software assembled into the application pipeline to handle requests and responses. Each component can inspect the request, perform logic, and either pass it to the next component or terminate the pipeline immediately.

```text
Incoming Request ──> [StaticFiles] ──> [Authentication] ──> [Authorization] ──> [Controller]
                                                                                    │
Outgoing Response <── [StaticFiles] <── [Authentication] <── [Authorization] <──────┘
```
