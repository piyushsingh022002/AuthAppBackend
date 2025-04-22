# Use .NET 7 SDK to build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["AuthApp.csproj", "./"]
RUN dotnet restore "AuthApp.csproj"
COPY . .
RUN dotnet publish "AuthApp.csproj" -c Release -o /app/publish

# Use .NET 7 runtime to run
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AuthApp.dll"]
