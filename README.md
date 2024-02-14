# Dotnet Foundation

## Overview of the project

Dotnet Foundation is a boilerplate project for ASP.NET WebAPI projects. This boilerplate can be used in any .NET project for Osmosys. This project contains common files that can be reused. Objective of building this project is to make it so that people can copy the project and build on top of it.

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

# Setting up the app in a Docker based environment

The development of this application happens using Visual Studio IDE on a Windows machine. The app setup in a Docker based environment enables developers of non-Windows origins to setup the backend application on their machine to test the APIs.

## Steps

1. [Install Docker](https://docs.docker.com/engine/install/) on your machine. Choose to follow the instructions based on your device' OS.
2. [Install Docker Compose](https://docs.docker.com/compose/install/). A separate installation is required for Linux based OS. If you are using Windows or MacOS, installing the Docker Desktop app includes Docker Compose.
3. Clone Dotnet-Foundation project.
4. In the root directory of the project, run `docker-compose -f docker-compose.yaml up`. This command starts two containers, one contains the DotnetFoundation-Backend (`foundation-api`) and the other contains MariaDB database server (`foundation-db`). This command will not exit, it registers logs on the console.
5. Ensure both containers are up, by running the command `docker ps -a`. You should notice two containers listed: `foundation-api` & `foundation-db`
6. Project will run on `http://localhost:5000` you can access the swagger ui at `http:localhost:5000/swagger/index.html`
7. Test the API via Postman. The app can be accessed using `http://localhost:5000/<API>`.

# License

The Dotnet Foundation is licensed under the [MIT](https://github.com/OsmosysSoftware/dotnet-foundation/blob/main/LICENSE) license.

## 👏 Big Thanks to Our Contributors

<a href="https://github.com/OsmosysSoftware/dotnet-foundation/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=OsmosysSoftware/dotnet-foundation" alt="Contributors" />
</a>

We appreciate the time and effort put in by all contributors to make this project better!
