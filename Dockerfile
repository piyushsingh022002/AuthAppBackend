# Use the official .NET image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RegLogBackend/RegLogBackend.csproj", "RegLogBackend/"]
RUN dotnet restore "RegLogBackend/RegLogBackend.csproj"
COPY . .
WORKDIR "/src/RegLogBackend"
RUN dotnet publish "RegLogBackend.csproj" -c Release -o /app/publish

# Copy the built app from the build stage and set it up
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RegLogBackend.dll"]
