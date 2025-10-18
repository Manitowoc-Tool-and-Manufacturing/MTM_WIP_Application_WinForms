#!/usr/bin/env bash
# MTM Workflow MCP Server Setup - Linux/Mac Script

set -e

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

echo -e "${CYAN}🔧 MTM Workflow MCP Server Setup${NC}"
echo -e "${CYAN}================================${NC}"
echo ""

# Get script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
REPO_ROOT="$(dirname "$SCRIPT_DIR")"
MCP_SERVER_PATH="$SCRIPT_DIR/mtm-workflow/dist/index.js"

# Determine VS Code config path based on OS
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    MCP_CONFIG_PATH="$HOME/Library/Application Support/Code/User/mcp.json"
elif [[ "$OSTYPE" == "linux-gnu"* ]]; then
    # Linux
    MCP_CONFIG_PATH="$HOME/.config/Code/User/mcp.json"
else
    echo -e "${RED}❌ Unsupported operating system: $OSTYPE${NC}"
    exit 1
fi

echo -e "${CYAN}Repository Root:${NC} $REPO_ROOT"
echo -e "${CYAN}MCP Server Path:${NC} $MCP_SERVER_PATH"
echo -e "${CYAN}VS Code Config:${NC}  $MCP_CONFIG_PATH"
echo ""

# Check if server is built
if [ ! -f "$MCP_SERVER_PATH" ]; then
    echo -e "${YELLOW}⚠️  MCP server not built yet. Building now...${NC}"
    
    cd "$SCRIPT_DIR/mtm-workflow"
    
    # Check if node_modules exists
    if [ ! -d "node_modules" ]; then
        echo -e "${CYAN}📦 Installing dependencies...${NC}"
        npm install
    fi
    
    echo -e "${CYAN}🔨 Building TypeScript...${NC}"
    npm run build
    
    echo -e "${GREEN}✅ Build successful!${NC}"
    echo ""
    cd "$SCRIPT_DIR"
fi

# Check if mcp.json exists
if [ ! -f "$MCP_CONFIG_PATH" ]; then
    echo -e "${CYAN}📝 Creating new mcp.json...${NC}"
    
    CONFIG_DIR="$(dirname "$MCP_CONFIG_PATH")"
    mkdir -p "$CONFIG_DIR"
    
    cat > "$MCP_CONFIG_PATH" << EOF
{
  "inputs": [],
  "servers": {
    "mtm-workflow": {
      "command": "node",
      "args": [
        "$MCP_SERVER_PATH"
      ],
      "type": "stdio"
    }
  }
}
EOF
    
    echo -e "${GREEN}✅ Created mcp.json with mtm-workflow server${NC}"
    echo ""
    
    # Open the file in VS Code for user verification
    echo -e "${CYAN}📂 Opening mcp.json for verification...${NC}"
    if command -v code &> /dev/null; then
        code "$MCP_CONFIG_PATH"
    else
        echo -e "${YELLOW}⚠️  'code' command not found. Open manually: $MCP_CONFIG_PATH${NC}"
    fi
else
    echo -e "${CYAN}📝 Updating existing mcp.json...${NC}"
    
    # Create backup before modifying
    BACKUP_PATH="${MCP_CONFIG_PATH}.backup-$(date +%Y%m%d-%H%M%S)"
    cp "$MCP_CONFIG_PATH" "$BACKUP_PATH"
    echo -e "${CYAN}💾 Backup created: $BACKUP_PATH${NC}"
    
    # Use Node.js to update JSON (more reliable than jq)
    node -e "
        const fs = require('fs');
        const configPath = '$MCP_CONFIG_PATH';
        const serverPath = '$MCP_SERVER_PATH';
        
        const config = JSON.parse(fs.readFileSync(configPath, 'utf8'));
        
        if (!config.servers) {
            config.servers = {};
        }
        
        config.servers['mtm-workflow'] = {
            command: 'node',
            args: [serverPath],
            type: 'stdio'
        };
        
        fs.writeFileSync(configPath, JSON.stringify(config, null, 2));
        console.log('Updated configuration');
    "
    
    echo -e "${GREEN}✅ Updated mcp.json successfully${NC}"
    echo ""
    
    # Open the file in VS Code for user verification
    echo -e "${CYAN}📂 Opening mcp.json for verification...${NC}"
    if command -v code &> /dev/null; then
        code "$MCP_CONFIG_PATH"
    else
        echo -e "${YELLOW}⚠️  'code' command not found. Open manually: $MCP_CONFIG_PATH${NC}"
    fi
fi

# Verify configuration
echo -e "${CYAN}🔍 Verifying configuration...${NC}"
if grep -q "mtm-workflow" "$MCP_CONFIG_PATH"; then
    echo -e "${GREEN}✅ Configuration verified!${NC}"
else
    echo -e "${RED}❌ Configuration verification failed${NC}"
    exit 1
fi

# Display success message
echo ""
echo -e "${GREEN}🎉 Setup Complete!${NC}"
echo ""
echo -e "${CYAN}Available MCP Tools:${NC}"
echo "  • check_checklists         - Analyze markdown checklist completion"
echo "  • validate_dao_patterns    - Validate C# DAO files for MTM standards"
echo ""
echo -e "${YELLOW}⚠️  Important: You must restart VS Code for changes to take effect!${NC}"
echo ""
echo -e "${CYAN}Next Steps:${NC}"
echo "  1. Close VS Code completely"
echo "  2. Reopen VS Code"
echo "  3. Ask GitHub Copilot to use the new tools"
echo ""
echo -e "${CYAN}Example usage:${NC}"
echo '  > "Check the checklists in specs/002-003-database-layer-complete/checklists"'
echo ""
echo -e "${CYAN}Documentation:${NC} .mcp/mtm-workflow/README.md"
echo ""
