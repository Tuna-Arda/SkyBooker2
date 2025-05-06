FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ["FlightService.csproj", "./"]
RUN dotnet restore "FlightService.csproj"
COPY . .
RUN dotnet publish "FlightService.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "FlightService.dll"]
