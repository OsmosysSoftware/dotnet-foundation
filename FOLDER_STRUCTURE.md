# Project Folder Structure and Guidelines

## Introduction
This document outlines the recommended folder structure for the project, focusing on maintaining clarity, scalability, and maintainability. A well-organized structure is crucial for collaboration and the scaling of the project.

## Folder Structure

### `DotnetFoundation.DAL`
The `DotnetFoundation.DAL` folder contains files relating to  database calls(repositories and interfaces) and objects(DBOs and DTOs)

1. `DatabaseContext`: This directory consists of contains code that defines and configures the data model, including entity classes that represent database tables, relationships between these entities, and configuration settings related to database connectivity, schema, etc.
2. `Models`: This contains all database objects and data transfer objects that store attributes of a tables which can be used for input or output.
3. `Repositories`: This folder stores all interfaces and repository code files that deal with implementation/interactions of different tables.

### `DotnetFoundation.API`
The `DotnetFoundation.API` folder contains all files and folders related to business logic layer which is more in concern with client interaction.

1. `BLL`: This folder contains BLL files which deal with http responses to an api request.
2. `Controller`: This folder consists of controller files that act as an intermediary between the user interface and the application logic.
3. `Helpers`: This folder consists of files that are used to encapsulate reusable methods or functionalities that can be used across an application.
4. `Models`: This folder consists of file that deals with error response when an exception has occured.  
5. `Properties`: This folder usually consists of launchsettings.json which determines the type of launch profiles to be used to launch the application during development.
6. `appsettings.json`: This file contains application-specific settings, configurations, and connection strings.
7. `program.cs`: This file  contains the entry point for the application and is responsible for configuring and building the web host.

## Best Practices

- **Separation of Concerns**: Follow the separation of concerns principle by organizing the project into distinct layers: Data Access Layer (DAL), Business Logic Layer (BLL), API layer, and possibly Presentation layer.
- **Modularity**: Use modular design by employing folders or namespaces to encapsulate related functionalities.
- **Single Responsibility Principle (SRP)**: Ensure that each class, method, or component has a clear, focused responsibility.
- **Consistent Naming Conventions**: Follow consistent naming conventions for classes, methods, variables, and folders for easier understanding.
- **Dependency Injection**: Utilize dependency injection to decouple components and improve testability and maintainability.
- **Error Handling and Logging**: Implement consistent error handling strategies and logging mechanisms across the application for effective debugging and troubleshooting.
- **Version Control**: Use a version control system like Git to manage the project's source code, allowing easy collaboration and tracking of changes.

## Maintenance Guidelines

- **Regular Code Reviews**: Conduct regular code reviews to maintain code quality, identify potential issues, and ensure adherence to coding standards.
- **Documentation**: Keep the codebase well-documented, including comments, README files, and inline documentation, to facilitate understanding for future developers.
- **Testing**: Prioritize writing unit tests, integration tests, and end-to-end tests to validate the functionality and behavior of the application.
- **Refactoring**: Periodically refactor the codebase to eliminate code smells, enhance performance, and improve maintainability.
- **Security Updates**: Stay updated with security patches, libraries, and framework versions to mitigate vulnerabilities.
- **Clean Code Practices**: Encourage clean code practices like meaningful naming, proper indentation, and avoiding code duplication.
- **Performance Monitoring**: Implement monitoring tools to analyze application performance and address performance bottlenecks.