FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

# Copy everything
COPY . .

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Debug -o /app/published-app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]
