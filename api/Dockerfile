FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies

# Copy everything else and build
COPY . .
RUN dotnet restore
RUN dotnet build Identity/Nauther.Identity.Api/Nauther.Identity.Api.csproj -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish Identity/Nauther.Identity.Api/Nauther.Identity.Api.csproj -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install SQL Server tools for health check
RUN apt-get update && apt-get install -y curl

# Add wait-for-it script
COPY Identity/Nauther.Identity.Api/wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

EXPOSE 80

# Wait for SQL Server to be ready before starting the application
ENTRYPOINT ["/wait-for-it.sh", "db:1433", "--", "dotnet", "Nauther.Identity.Api.dll"]
