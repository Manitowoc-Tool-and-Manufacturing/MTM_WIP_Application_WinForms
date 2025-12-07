#!/bin/bash

# Function to handle exit
pause_on_exit() {
    echo ""
    echo "Press any key to close this window..."
    read -n 1 -s -r
}

# Trap errors and exit to ensure pause runs
trap pause_on_exit EXIT

echo "🚀 MTM WIP Application - Setting up development environment..."

# Install Python requirements
echo "🐍 Installing Python dependencies..."
if pip3 install --user -r .devcontainer/requirements.txt; then
    echo "   ✅ Python dependencies installed."
else
    echo "   ❌ Failed to install Python dependencies."
    exit 1
fi

# Restore NuGet packages
echo "📦 Restoring NuGet packages..."
if dotnet restore MTM_WIP_Application.sln; then
    echo "   ✅ NuGet packages restored."
else
    echo "   ❌ Failed to restore NuGet packages."
    exit 1
fi

# Build the solution
echo "🔨 Building solution..."
if dotnet build MTM_WIP_Application.sln --no-restore; then
    echo "   ✅ Solution built successfully."
else
    echo "   ❌ Build failed."
    exit 1
fi

# List installed packages for verification
echo "📋 Installed NuGet packages:"
dotnet list package

echo "✅ Development environment ready!"
echo ""
echo "📝 Key NuGet Packages:"
echo "  - ClosedXML 0.105.0 (Excel operations)"
echo "  - MySql.Data 9.4.0 (MySQL connectivity)"
echo "  - Microsoft.Data.SqlClient 6.1.3 (SQL Server)"
echo "  - Microsoft.Extensions.DependencyInjection 8.0.0"
echo "  - Microsoft.Extensions.Logging 8.0.0"
echo "  - Microsoft.Web.WebView2 1.0.2792.45"
echo "  - Newtonsoft.Json 13.0.4"
echo ""
echo "🎯 Ready to code with GitHub Copilot!"

# The trap will handle the pause here automatically