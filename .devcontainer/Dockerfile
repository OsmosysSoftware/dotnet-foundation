# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN dotnet restore
# Install EF Core tools extension
RUN dotnet tool install --global dotnet-ef --version 8.0.0
ENV PATH $PATH:/root/.dotnet/tools




