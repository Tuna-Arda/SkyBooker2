# Stage 1: Build mit .NET 9 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiere das gesamte Repo in die Build-Stage
COPY . .

# Restore & Publish
RUN dotnet restore "src/AuthService/AuthService.API/AuthService.API.csproj"
RUN dotnet publish "src/AuthService/AuthService.API/AuthService.API.csproj" -c Release -o /app/publish

# Stage 2: Runtime mit .NET 9 ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Nur die gepublishte Ausgabe kopieren
COPY --from=build /app/publish .

# Starte die API
ENTRYPOINT ["dotnet", "AuthService.API.dll"]
