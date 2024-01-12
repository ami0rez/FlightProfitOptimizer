# Use the Microsoft .NET Core SDK image as the build environment
FROM mcr.microsoft.com/dotnet/core/sdk:net8.0 AS build-env
WORKDIR /app

# Copy csproj and restore any dependencies (via NuGet)
COPY src/FlightProfitOptimizer/*.csproj ./
RUN dotnet restore

# Copy the source code and build the release
COPY src/FlightProfitOptimizer/ ./
RUN dotnet publish -c Release -o out

# Generate the runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

# Expose the port the app runs on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "FlightProfitOptimizer.dll"]