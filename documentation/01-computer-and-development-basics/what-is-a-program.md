# What is a Program?

## 1. What is it?
A **program** is an executable file containing a sequence of instructions written in a programming language to solve a specific problem.

## 2. How it works
1. **Source Code**: Human programmers write readable text files (e.g. `AppointmentService.cs`).
2. **Compilation**: The .NET compiler (`dotnet build`) translates those text files into binary intermediate language and machine code (`Sunshine.App.dll`).
3. **Execution**: The operating system loads the binary into memory and the CPU executes the instructions line by line.

## 3. In Sunshine
When you run:
```bash
dotnet run --urls "http://0.0.0.0:5000"
```
The operating system launches the compiled Sunshine program, which listens for incoming HTTP web connections.
