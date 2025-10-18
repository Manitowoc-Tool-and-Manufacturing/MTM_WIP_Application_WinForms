# MTM Workflow MCP Server - Quick Setup Guide

This directory contains setup scripts to configure the MTM Workflow MCP server on any development machine.

## Quick Setup (Choose Your Platform)

### Windows (PowerShell)
```powershell
cd .mcp
.\setup-mcp.ps1
```

### Windows (Command Prompt)
```cmd
cd .mcp
setup-mcp.bat
```

### Linux/Mac (Bash)
```bash
cd .mcp
chmod +x setup-mcp.sh
./setup-mcp.sh
```

## What the Setup Script Does

1. ✅ **Checks if the MCP server is built**
   - If not, runs `npm install` and `npm run build` automatically

2. ✅ **Creates automatic backup**
   - Backs up existing `mcp.json` before any modifications
   - Backup file: `mcp.json.backup-YYYYMMDD-HHMMSS`

3. ✅ **Configures VS Code's mcp.json**
   - Creates `mcp.json` if it doesn't exist
   - Updates existing `mcp.json` to add/update `mtm-workflow` server
   - Prompts before overwriting existing configuration

4. ✅ **Opens the file for verification**
   - Automatically opens `mcp.json` in VS Code
   - Allows you to verify changes before restart

5. ✅ **Verifies the configuration**
   - Ensures paths are correct
   - Validates JSON structure

6. ✅ **Provides next steps**
   - Clear instructions to restart VS Code
   - Usage examples

## After Running Setup

**IMPORTANT:** You must **restart VS Code completely** for the MCP server to load.

Then ask GitHub Copilot to use the tools:

```
"Check the checklists in specs/002-003-database-layer-complete/checklists"
```

or

```
"Validate all DAO files in the Data directory"
```

## Available Tools

After setup, these tools will be available to GitHub Copilot:

- **check_checklists** - Analyze markdown checklist completion status
- **validate_dao_patterns** - Validate C# DAO files for MTM coding standards

## Troubleshooting

### Restore from Backup
If something goes wrong, restore your original configuration:

**Windows:**
```powershell
# Find your backup
ls $env:APPDATA\Code\User\mcp.json.backup-*

# Restore the backup
cp "$env:APPDATA\Code\User\mcp.json.backup-YYYYMMDD-HHMMSS" "$env:APPDATA\Code\User\mcp.json"
```

**Linux/Mac:**
```bash
# Find your backup
ls ~/.config/Code/User/mcp.json.backup-* # Linux
ls ~/Library/Application\ Support/Code/User/mcp.json.backup-* # Mac

# Restore the backup
cp ~/.config/Code/User/mcp.json.backup-YYYYMMDD-HHMMSS ~/.config/Code/User/mcp.json
```

### Script won't run (PowerShell)
```powershell
# Temporarily bypass execution policy
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
.\setup-mcp.ps1
```

### Tools not appearing after restart
1. Check VS Code Output panel (View → Output → MCP)
2. Verify `node` is in your PATH: `node --version`
3. Manually check `mcp.json` location:
   - **Windows:** `%APPDATA%\Code\User\mcp.json`
   - **Mac:** `~/Library/Application Support/Code/User/mcp.json`
   - **Linux:** `~/.config/Code/User/mcp.json`

### Build errors
```bash
cd .mcp/mtm-workflow
npm install
npm run build
```

## Manual Setup (If Scripts Don't Work)

1. **Build the server:**
   ```bash
   cd .mcp/mtm-workflow
   npm install
   npm run build
   ```

2. **Edit VS Code config manually:**
   - Open: `%APPDATA%\Code\User\mcp.json` (Windows)
   - Add or update:
     ```json
     {
       "servers": {
         "mtm-workflow": {
           "command": "node",
           "args": [
             "C:/Users/yourname/source/repos/MTM_WIP_Application_WinForms/.mcp/mtm-workflow/dist/index.js"
           ],
           "type": "stdio"
         }
       }
     }
     ```

3. **Restart VS Code**

## Force Overwrite Existing Configuration

```powershell
.\setup-mcp.ps1 -Force
```

## Documentation

- **Full tool documentation:** `mtm-workflow/README.md`
- **Installation details:** `mtm-workflow/INSTALLATION.md`

## Need Help?

If setup fails, check:
1. Node.js is installed: `node --version`
2. npm is installed: `npm --version`
3. VS Code is closed during config changes
4. Path separators are correct for your OS

## Repository Structure

```
.mcp/
├── setup-mcp.ps1          # PowerShell setup script (Windows)
├── setup-mcp.bat          # Batch wrapper (Windows)
├── setup-mcp.sh           # Bash setup script (Linux/Mac)
├── SETUP-GUIDE.md         # This file
└── mtm-workflow/
    ├── dist/              # Compiled server (generated)
    ├── src/               # TypeScript source
    ├── README.md          # Tool documentation
    ├── INSTALLATION.md    # Manual installation guide
    └── package.json
```
