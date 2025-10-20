#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Configures the MTM Workflow MCP server in VS Code settings.

.DESCRIPTION
    This script automatically configures the MCP server in VS Code's mcp.json file.
    Run this on any new development machine to enable the MTM workflow tools.

.PARAMETER Force
    Overwrites existing mtm-workflow configuration if present.

.EXAMPLE
    .\setup-mcp.ps1
    
.EXAMPLE
    .\setup-mcp.ps1 -Force
#>

[CmdletBinding()]
param(
    [switch]$Force
)

$ErrorActionPreference = 'Stop'

# Colors for output
function Write-Success { Write-Host $args -ForegroundColor Green }
function Write-Info { Write-Host $args -ForegroundColor Cyan }
function Write-Warning { Write-Host $args -ForegroundColor Yellow }
function Write-Failure { Write-Host $args -ForegroundColor Red }

Write-Info "🔧 MTM Workflow MCP Server Setup"
Write-Info "================================"
Write-Host ""

# 1. Determine paths
$repoRoot = Split-Path -Parent $PSScriptRoot
$mcpServerPath = Join-Path $PSScriptRoot "mtm-workflow\dist\index.js"
$mcpConfigPath = Join-Path $env:APPDATA "Code\User\mcp.json"

Write-Info "Repository Root: $repoRoot"
Write-Info "MCP Server Path: $mcpServerPath"
Write-Info "VS Code Config:  $mcpConfigPath"
Write-Host ""

# 2. Check if server is built
if (-not (Test-Path $mcpServerPath)) {
    Write-Warning "⚠️  MCP server not built yet. Building now..."
    
    Push-Location (Join-Path $PSScriptRoot "mtm-workflow")
    try {
        # Check if node_modules exists
        if (-not (Test-Path "node_modules")) {
            Write-Info "📦 Installing dependencies..."
            npm install
            if ($LASTEXITCODE -ne 0) {
                throw "npm install failed"
            }
        }
        
        Write-Info "🔨 Building TypeScript..."
        npm run build
        if ($LASTEXITCODE -ne 0) {
            throw "npm run build failed"
        }
        
        Write-Success "✅ Build successful!"
    }
    finally {
        Pop-Location
    }
    Write-Host ""
}

# 3. Check if mcp.json exists
if (-not (Test-Path $mcpConfigPath)) {
    Write-Info "📝 Creating new mcp.json..."
    
    $newConfig = @{
        inputs = @()
        servers = @{
            "mtm-workflow" = @{
                command = "node"
                args = @($mcpServerPath.Replace('\', '/'))
                type = "stdio"
            }
        }
    }
    
    $configDir = Split-Path -Parent $mcpConfigPath
    if (-not (Test-Path $configDir)) {
        New-Item -ItemType Directory -Path $configDir -Force | Out-Null
    }
    
    $newConfig | ConvertTo-Json -Depth 10 | Set-Content $mcpConfigPath -Encoding UTF8
    Write-Success "✅ Created mcp.json with mtm-workflow server"
    Write-Host ""
    
    # Open the file in VS Code for user verification
    Write-Info "📂 Opening mcp.json for verification..."
    if (Get-Command code -ErrorAction SilentlyContinue) {
        code $mcpConfigPath
    } else {
        Write-Warning "⚠️  'code' command not found. Open manually: $mcpConfigPath"
    }
}
else {
    # 4. Update existing mcp.json
    Write-Info "📝 Updating existing mcp.json..."
    
    try {
        # Create backup before modifying
        $backupPath = "$mcpConfigPath.backup-$(Get-Date -Format 'yyyyMMdd-HHmmss')"
        Copy-Item $mcpConfigPath $backupPath -Force
        Write-Info "💾 Backup created: $backupPath"
        
        $config = Get-Content $mcpConfigPath -Raw | ConvertFrom-Json
        
        # Check if mtm-workflow already exists
        if ($config.servers.PSObject.Properties.Name -contains "mtm-workflow") {
            if (-not $Force) {
                Write-Warning "⚠️  mtm-workflow server already configured."
                $response = Read-Host "Overwrite existing configuration? (y/N)"
                if ($response -ne 'y' -and $response -ne 'Y') {
                    Write-Info "Skipping configuration update."
                    Write-Host ""
                    return
                }
            }
            Write-Info "Updating mtm-workflow configuration..."
        }
        else {
            Write-Info "Adding mtm-workflow server to configuration..."
        }
        
        # Update or add mtm-workflow server
        $config.servers | Add-Member -MemberType NoteProperty -Name "mtm-workflow" -Value @{
            command = "node"
            args = @($mcpServerPath.Replace('\', '/'))
            type = "stdio"
        } -Force
        
        # Save updated config
        $config | ConvertTo-Json -Depth 10 | Set-Content $mcpConfigPath -Encoding UTF8
        Write-Success "✅ Updated mcp.json successfully"
        Write-Host ""
        
        # Open the file in VS Code for user verification
        Write-Info "📂 Opening mcp.json for verification..."
        if (Get-Command code -ErrorAction SilentlyContinue) {
            code $mcpConfigPath
        } else {
            Write-Warning "⚠️  'code' command not found. Open manually: $mcpConfigPath"
        }
    }
    catch {
        Write-Failure "❌ Failed to update mcp.json: $_"
        Write-Info "You may need to manually edit: $mcpConfigPath"
        exit 1
    }
}

# 5. Verify configuration
Write-Info "🔍 Verifying configuration..."
try {
    $config = Get-Content $mcpConfigPath -Raw | ConvertFrom-Json
    $serverConfig = $config.servers."mtm-workflow"
    
    if ($null -eq $serverConfig) {
        throw "mtm-workflow server not found in configuration"
    }
    
    if ($serverConfig.command -ne "node") {
        throw "Server command is not 'node'"
    }
    
    $configuredPath = $serverConfig.args[0]
    if ($configuredPath -ne $mcpServerPath.Replace('\', '/')) {
        throw "Server path mismatch. Expected: $($mcpServerPath.Replace('\', '/'))"
    }
    
    Write-Success "✅ Configuration verified!"
}
catch {
    Write-Failure "❌ Configuration verification failed: $_"
    exit 1
}

# 6. Display success message and next steps
Write-Host ""
Write-Success "🎉 Setup Complete!"
Write-Host ""
Write-Info "Available MCP Tools:"
Write-Host "  • check_checklists         - Analyze markdown checklist completion"
Write-Host "  • validate_dao_patterns    - Validate C# DAO files for MTM standards"
Write-Host ""
Write-Warning "⚠️  Important: You must restart VS Code for changes to take effect!"
Write-Host ""
Write-Info "Next Steps:"
Write-Host "  1. Close VS Code completely"
Write-Host "  2. Reopen VS Code"
Write-Host "  3. Ask GitHub Copilot to use the new tools"
Write-Host ""
Write-Info "Example usage:"
Write-Host '  > "Check the checklists in specs/002-003-database-layer-complete/checklists"'
Write-Host ""
Write-Info "Documentation: .mcp/mtm-workflow/README.md"
Write-Host ""
