King.Data.Sql.Reflection
===========

Reflect SQL Server Schema
+ Load Stored Procedures, and all parameters
+ Load tables, and all columns and keys

## NuGet
[Add via NuGet](https://www.nuget.org/packages/King.Data.Sql.Reflection)
```
PM> Install-Package King.Data.Sql.Reflection
```

## Get Started
### Stored Procedures
```
var reader = new SchemaReader("Server=localhost;Database=DataBase;Trusted_Connection=True;");
var schema = await reader.Load();
```
### Tables
```
var reader = new SchemaReader("Server=localhost;Database=DataBase;Trusted_Connection=True;");
var schema = await reader.Load(SchemaTypes.Tables);
```