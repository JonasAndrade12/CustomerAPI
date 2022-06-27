FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

# Copy everything
COPY ./CustomerAPI/ ./CustomerAPI/
COPY ./CustomerAPI.Tests/ ./CustomerAPI.Tests/

# Restore as distinct layers
RUN dotnet restore ./CustomerAPI/CustomerAPI.sln

# Build and publish a release
RUN dotnet publish ./CustomerAPI/CustomerAPI.sln -c Debug -o /app/published-app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]
