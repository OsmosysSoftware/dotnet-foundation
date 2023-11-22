# Dotnet Foundation

## Overview of the project

Dotnet Foundation is a boiler plate project for ASP .NET projects. This boiler plate can be used in any .NET project for Osmosys. This project contains common files that  can be reused  again and again. Objective of building this project is to make it so that people can copy the project and build on top of it.

## Development Setup

1. Install [VS Code](https://code.visualstudio.com/)
2. Install [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download)
3. Verify that .NET SDK is installed successfully by executing the following command in the terminal. `dotnet --version`.
4. Clone this project.
5. Open VS Code in this directory `\dotnet-foundation`.
6. Install the recommended [VS Code extensions](https://imgur.com/XIh4IPI)
7. In the project's directory `\dotnet-foundation\DotnetFoundation`, execute the following commands to run the project.
```csharp
- dotnet restore
- dotnet build
- dotnet run --project .\DotnetFoundation.API\
```








