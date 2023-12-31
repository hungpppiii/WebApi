﻿# WebApi

> Tech Stack: ASP.NET, EF Core, SQL Server

# Usage

## Create or update file appsettings.Development.json:
```json
{
  "ConnectionStrings": {
    // Connection String for Mysql
    //"DefaultConnection": "server=[DB SERVER URL];user=[User Name];password=[Password];database=[Database Name]"
    // Connection String for SQL Server
    //"DefaultConnection": "Data Source=[DB SERVER URL]; Initial Catalog=[Database Name]; User Id=[User Name]; Password=[Password]; Trusted_Connection=SSPI; Encrypt=false; TrustServerCertificate=true"
    "DefaultConnection": "Data Source=[DB SERVER URL]; Database=[Database Name]; Trusted_Connection=True; TrustServerCertificate=True;"
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
