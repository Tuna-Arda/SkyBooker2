FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ["BookingService.csproj", "./"]
RUN dotnet restore "BookingService.csproj"
COPY . .
RUN dotnet publish "BookingService.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "BookingService.dll"]
