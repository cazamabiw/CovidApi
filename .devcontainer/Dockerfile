# Use Microsoft's official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

# Install PostgreSQL client tools
RUN apt-get update && apt-get install -y postgresql-client

# Set working directory
WORKDIR /workspace

# Expose ports
EXPOSE 5000

# Entry Point
CMD ["sleep", "infinity"]
