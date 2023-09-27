# WebApi

> Tech Stack: ASP.NET, EF Core, SQL Server

# Usage

## Create or update file appsettings.Development.json:
```json
{
  "ConnectionStrings": {
    // Connection String for Mysql
    //"DefaultConnection": "server=localhost;user=root;password=keydiaz.123;database=CourseApi"
    // Connection String for SQL Server
    //"DefaultConnection": "Data Source=localhost; Initial Catalog=CourseApi; User Id=sa; Password=keydiaz.123; Trusted_Connection=SSPI; Encrypt=false; TrustServerCertificate=true"
    "DefaultConnection": "Data Source=localhost; Database=CourseApi; Trusted_Connection=True; TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Create Migrations
.NET Core CLI
```console
dotnet ef migrations add Initial
```
Package Management Console
```console
Add-Migration Initial
```

## Create Database
.NET Core CLI
```console
dotnet ef database update
```
Package Management Console
```console
Update-Database
```

## Build App
.NET Core CLI
```console
dotnet build
```

## Run App
.NET Core CLI
```console
dotnet run
```
