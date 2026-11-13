CompanyTshirt

CompanyTshirt is a personal project developed as part of my programming studies. It's a REST API for a football shirt store, where users can browse products, manage their accounts, and place orders.

I built this project to practice backend development with C# and .NET, and to get more familiar with how real-world applications are structured.

Technologies

C#, .NET 10, ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT, Swagger, Azure and GitHub Actions.

Features

The API allows users to manage football shirts, brands, clubs, and sizes. It also includes user registration and authentication, order management, role-based authorization, and integration with an external exchange-rate service.

Architecture

The project follows a layered architecture with four layers: Presentation, Application, Domain, and Infrastructure. Each one has its own responsibilities, keeping the code organized and easier to maintain.

Running locally

You'll need the .NET 10 SDK and SQL Server. Configure your database connection string and run:

1) dotnet restore
2) dotnet build
3) dotnet run --project CompanyTshirt/CompanyTshirt.Presentation.csproj

You can then explore the API through Swagger.

Developed by Mateo Eguiazu
