
# Build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file and restore dependencies
COPY ["TestProject1/TestProject1.csproj", "TestProject1/"]
RUN dotnet restore "TestProject1/TestProject1.csproj"

# Copy application source code
COPY TestProject1/ TestProject1/

# Publish the application
RUN dotnet publish "TestProject1/TestProject1.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# Configure HTTP
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Copy published files
COPY --from=build /app/publish .

# Run as non-root
USER $APP_UID

ENTRYPOINT ["dotnet", "TestProject1.dll"]
