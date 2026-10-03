# Why Use an ORM Instead of Raw SQL?

| Feature | Raw SQL Strings | Entity Framework Core (ORM) |
| :--- | :--- | :--- |
| **Typo Safety** | Typos in column names crash at runtime. | Caught immediately by C# compiler! |
| **Object Mapping** | Must manually loop through database rows. | Automatically instantiates typed C# objects. |
| **SQL Injection** | High risk if strings are concatenated. | Fully immune (automatic parameterization). |
| **Portability** | Hard to switch database engines. | Can switch to SQL Server or SQLite easily. |
