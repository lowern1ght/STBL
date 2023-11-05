# STBL Library

A chain of nuget packages that I constantly implemented in my projects and decided not to repeat myself and here)

1. STBL.Configuration.EntityFramework

It is possible to assign a configuration file or assign the following sector in `appsettings`

```json

{
  "DatabaseConnections": {
    "DbContextName": {
      "Host": "localhost",
      "Port": 5432,
      "Database": "test-db",
      "Username": "test-user",
      "Password": "b0ef8467b6fc4a1eafaacc73abcf347a"
    }
  }
}


```

```csharp

var contextSettings = configurationRoot.GetDbContextSettings<DbContext>();

```