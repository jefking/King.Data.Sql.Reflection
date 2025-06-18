# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY King.Data.Sql.Reflection/King.Data.Sql.Reflection.csproj King.Data.Sql.Reflection/
COPY King.Data.Sql.Reflection.Console/King.Data.Sql.Reflection.Console.csproj King.Data.Sql.Reflection.Console/
RUN dotnet restore King.Data.Sql.Reflection.Console/King.Data.Sql.Reflection.Console.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish King.Data.Sql.Reflection.Console/King.Data.Sql.Reflection.Console.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "King.Data.Sql.Reflection.Console.dll"]