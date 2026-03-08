# Use .NET 9.0 SDK as base image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy solution and project files first
COPY ["Pegasus.sln", "./"]
COPY ["Pegasus/*.csproj", "./Pegasus/"]
COPY ["Pegasus.DungeonGenerator/*.csproj", "./Pegasus.DungeonGenerator/"]

# Restore as distinct layers
RUN dotnet restore

# Copy everything else
COPY . .

# Build and publish
RUN mkdir -p /app/out && \
    dotnet publish "./Pegasus/Pegasus.csproj" -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app

# Copy the published output
COPY --from=build-env /app/out .

# Copy the config file
COPY ["Config.json", "./"]

# Set environment variables (PEGASUS_DB_PASSWORD is injected at runtime via docker-compose)
ENV PEGASUS_DB_HOSTNAME=postgres \
    PEGASUS_DB_PORT=5432 \
    PEGASUS_DB_DATABASE=pegasus \
    PEGASUS_DB_USERNAME=pegasus

ENTRYPOINT ["dotnet", "Pegasus.dll"] 