# Setup

This document provides a step-by-step guide for setting up the project. By following these steps, you'll be able to run your application locally with the necessary environment configurations.

## Prerequisites

Before setting up .NET Identity for development, ensure you have the following prerequisites:

- **.NET SDK and runtime:** .NET 7.0.
- **Visual Studio:** Visual Studio 2019 or Visual Studio Code.
- **Git:** Git v2.x or higher.
- **MariaDB:** MariaDB v10.x or higher.

These prerequisites are essential for deploying and running .NET Identity in a development environment.
Please make sure to have these tools and versions installed on your development machine before proceeding with the setup.
Make sure MariaDB server is up and running.

## Getting Started

1. Clone the repository to your local machine:

   ```sh
   git clone https://github.com/OsmosysSoftware/dotnet-foundation
   cd dotnet-foundation/DotnetFoundation
   ```

2. Open the project in Visual Studio or Visual Studio Code.

3. In the `appsettings.json` file in the project root add the required connection string for MariaDB:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=YourDatabase;User=root;Password=YourPassword;"
     },
     "Jwt": {
       "Key": "YourJwtKey",
       "Issuer": "YourIssuer",
       "Audience": "YourAudience"
     }
   }
   ```

   Replace placeholders (`YourDatabase`, `YourPassword`, `YourJwtKey`, `YourIssuer`, `YourAudience`) with your actual configuration.

4. Set up the database:

   - Ensure your database server (MariaDB) is running.
   - Run Entity Framework Core migrations to create tables:

     ```sh
     dotnet ef database update
     ```

5. Start the development server:

   ```sh
   dotnet run
   ```

Your .NET Identity application will now be running locally, typically at `http://localhost:5207` or `https://localhost:7248` if using HTTPS.

You can customize the application further based on your needs, and this setup provides a foundation for developing with .NET Identity in your project.

Feel free to adapt the configuration settings and structure according to your project requirements.
