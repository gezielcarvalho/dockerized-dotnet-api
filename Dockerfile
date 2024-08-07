﻿# Build Stage dotnet 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./CloudCustomers.API/CloudCustomers.API.csproj" --disable-parallel
RUN dotnet publish "./CloudCustomers.API/CloudCustomers.API.csproj" -c Release -o /app

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
COPY --from=build /app .

EXPOSE 5000

ENTRYPOINT ["dotnet", "CloudCustomers.API.dll"]

