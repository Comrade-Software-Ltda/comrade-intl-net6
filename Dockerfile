# Use the official .NET 6 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish /App/src/Comrade.Api/Comrade.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .

# Expose the necessary port, e.g., 80 for HTTP (adjust if you need other ports)
EXPOSE 5000

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Comrade.Api.dll"]


