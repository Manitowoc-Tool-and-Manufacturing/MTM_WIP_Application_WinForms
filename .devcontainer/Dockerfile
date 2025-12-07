FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0

# Install Python 3 and MySQL client
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends \
    python3 \
    python3-pip \
    python3-venv \
    default-mysql-client \
    && apt-get clean -y && rm -rf /var/lib/apt/lists/*

# Install PowerShell
# Updated to Debian 12 to match .NET 8 base image and fixed dpkg filename typo
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y powershell \
    && rm packages-microsoft-prod.deb

# Install Node.js 20 for ESLint support
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y nodejs

# Set up NuGet cache directory
RUN mkdir -p /workspace/.nuget/packages \
    && chown -R vscode:vscode /workspace/.nuget

# Install global .NET tools
# Switch to vscode user to ensure tools are installed to /home/vscode/.dotnet/tools
USER vscode
RUN dotnet tool install --global dotnet-ef