# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5000

# Copy everything else and build
COPY DotnetFoundation/ ./

# Restore the project dependencies
RUN dotnet restore
# Build the project in Debug mode and store artifacts in /out folder
RUN dotnet publish -c Debug -o out

# Install EF Core tools extension
RUN dotnet tool install --global dotnet-ef --version 8.0.0
ENV PATH $PATH:/root/.dotnet/tools

#Install wait-for-it script
ADD https://github.com/vishnubob/wait-for-it/raw/master/wait-for-it.sh /usr/local/bin/wait-for-it
RUN chmod +x /usr/local/bin/wait-for-it

# Update the database during the build process
WORKDIR /app
RUN echo "#!/bin/bash\n\
wait-for-it -t 60 foundation-db:3306 -- dotnet ef database update --project ./DotnetFoundation.Api\n\
cd /app/DotnetFoundation.Api\n\
dotnet /app/out/DotnetFoundation.Api.dll" > entrypoint.sh
RUN chmod +x entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
