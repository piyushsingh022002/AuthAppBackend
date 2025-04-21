# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy everything and publish the app
COPY . . 
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port (same as launchSettings.json)
EXPOSE 80

# Entry point for app
ENTRYPOINT ["dotnet", "RegLogBackend.dll"]
