# Project Folder Structure and Guidelines

## Introduction
This document outlines the recommended folder structure for the project, focusing on maintaining clarity, scalability, and maintainability. A well-organized structure is crucial for collaboration and the scaling of the project.

## Folder Structure

### `DotnetFoundation.API`
The `DotnetFoundation.API` folder contains all files and folders related to presenting data for client interaction in the form of JSON objects through HTTP requests. This facilitates the consumption of data by front-end applications. Front-end applications can consume these APIs to display and interact with the data in a user-friendly manner through the UI.

#### Components

 `Controller`: This folder consists of controller files that act as an intermediary between the user interface and the application logic.

 `Properties`: This folder usually consists of launchsettings.json which determines the type of launch profiles to be used to launch the application during development.

 `Program.cs`: This file  contains the entry point for the application and is responsible for configuring and building the web host.

 `appsettings.json`: This file contains application-specific settings, configurations, and connection strings.

#### Function

- Responsible for handling HTTP requests.
- Follows RESTful API design principles.
- Contains controllers for user management, authentication, and authorization.

### `DotnetFoundation.Application`
The `DotnetFoundation.Application` folder contains the business logic. All the business logic will be written in this layer. It is in this layer that services interfaces are kept, separate from their implementation, for loose coupling and separation of concerns.

#### Components

`DTO`: Data Transfer Object. Define the DTO for clean data transfer between different layers of the application

`Interfaces`: Contain the interfaces for the services and the repositories providing a contract for interactions between different layers of the application

`Services`: Contain the implementations of the various application services centralizing and managing the business logic and application-specific rules

#### Function

- Manages business logic and application-specific rules.
- Consists of Data Transfer Objects (DTOs), interfaces, and services.
- Interfaces define contracts for interacting with the domain and infrastructure layers.
- Services implement application logic, ensuring separation from infrastructure concerns.

### `DotnetFoundation.Domain`
The `DotnetFoundation.Domain` folder contains the enterprise logic, like the entities and their specifications

#### Components

`Entities`: Defines the Core business entities, representing the fundamental building blocks of the business model

#### Function

- Defines core entities representing the business model.
- Encapsulates business rules, validation, and domain-specific logic.
- Remains independent of infrastructure details.

### `DotnetFoundation.Infrastructure`
The `DotnetFoundation.Infrastructure` folder contains all the database migrations and database context Objects. Here, we have the repositories of all the domain model objects

#### Components

`Identity`: This directory defines entities related to identity models within the ASP.NET Identity framework

`Migrations`: Contain database migration scripts

`Persistence`: Contain database context object and repositories

#### Function

- Implements data storage, external integrations, and identity.
- Includes Identity Models extending the ASP.NET Identity framework.
- UserRepository serves as the bridge between the application and the data storage mechanism.
- Configures dependency injection for infrastructure services.


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