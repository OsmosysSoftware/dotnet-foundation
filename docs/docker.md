Below are step-by-step instructions to set up a development environment using Visual Studio Code Dev Containers:

### Step 1: Install Prerequisites

Ensure that you have the necessary software installed on your machine:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/products/docker-desktop)

### Step 2: Open Project in VS Code

Open Visual Studio Code and navigate to the project folder:

```bash
cd <project-folder>
code .
```

### Step 3: Install "Remote - Containers" Extension

In Visual Studio Code, go to the Extensions view (shortcut: `Ctrl+Shift+X`), search for "Remote Development and Dev Containers," and install the extension provided by Microsoft.

### Step 4: Reopen Project in Dev Container

Press `Ctrl+Shift+P`, type "Dev Containers: Reopen in container," and select this option. This command reloads the project in a containerized development environment.

### Step 5: Check and Configure Environment Variables

Ensure that you have the necessary environment variables configured by checking the existence of the .env file. Use the provided .env.example file as a template

### Step 6: Verify Dev Container Setup

Open a terminal in VS Code; you should find yourself inside the development container. Confirm that the required dependencies are installed, and the project runs successfully.

```bash
ls       # Ensure project files are present
dotnet build   # Build the project
dotnet run -p ./DotnetFoundation.API #Run the project to verify setup
```

### Step 7: Run Migration (if new migrations are added)

```bash
# Install dotnet-ef tool
dotnet tool restore

# Run migration
dotnet-ef database update -p ./DotnetFoundation.API
```

### Step 8: Run the Application

Proceed with the development of your application within the container.

These steps help streamline the development process by isolating dependencies within a container, ensuring consistent environments across different development machines.
