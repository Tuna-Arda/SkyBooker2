FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "src/FlightService/FlightService.API/FlightService.API.csproj"
RUN dotnet publish "src/FlightService/FlightService.API/FlightService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FlightService.API.dll"]
