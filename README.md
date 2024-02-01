# Dotnet Foundation

## Overview of the project

Dotnet Foundation is a boilerplate project for ASP.NET WebAPI projects. This boilerplate can be used in any .NET project for Osmosys. This project contains common files that  can be reused. Objective of building this project is to make it so that people can copy the project and build on top of it.

## Features

This project uses modern features of dotnet such as Clean Architecture and AspNetCore.Identity. To learn more about them, please go through the following documents:

1. [Clean Architecture](./docs/CLEAN_ARCHITECTURE.md)
2. [AspNetCore.Identity](./docs/ASPNETCORE_IDENTITY.md)

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

# License
The Dotnet Foundation is licensed under the [MIT](https://github.com/OsmosysSoftware/dotnet-foundation/blob/main/LICENSE) license.

## 👏 Big Thanks to Our Contributors

<a href="https://github.com/OsmosysSoftware/dotnet-foundation/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=OsmosysSoftware/dotnet-foundation" alt="Contributors" />
</a>

We appreciate the time and effort put in by all contributors to make this project better!






