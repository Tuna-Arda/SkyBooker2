FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ["Gateway.csproj", "./"]
RUN dotnet restore "Gateway.csproj"
COPY . .
RUN dotnet publish "Gateway.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Gateway.dll"]
