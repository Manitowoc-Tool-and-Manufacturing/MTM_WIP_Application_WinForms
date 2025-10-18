---
description: "Prompt for generating an AGENTS.md file for a repository"
mode: "agent"
---

# Create high‑quality AGENTS.md file

You are a code agent. Your task is to create a complete, accurate AGENTS.md at the root of this repository that follows the public guidance at https://agents.md/.

AGENTS.md is an open format designed to provide coding agents with the context and instructions they need to work effectively on a project.

## What is AGENTS.md?

AGENTS.md is a Markdown file that serves as a "README for agents" - a dedicated, predictable place to provide context and instructions to help AI coding agents work on your project. It complements README.md by containing detailed technical context that coding agents need but might clutter a human-focused README.

## Key Principles

- **Agent-focused**: Contains detailed technical instructions for automated tools
- **Complements README.md**: Doesn't replace human documentation but adds agent-specific context
- **Standardized location**: Placed at repository root (or subproject roots for monorepos)
- **Open format**: Uses standard Markdown with flexible structure
- **Ecosystem compatibility**: Works across 20+ different AI coding tools and agents

## File Structure and Content Guidelines

### 1. Required Setup

- Create the file as `AGENTS.md` in the repository root
- Use standard Markdown formatting
- No required fields - flexible structure based on project needs

### 2. Essential Sections to Include

#### Project Overview

- Brief description of what the project does
- Architecture overview if complex
- Key technologies and frameworks used

#### Setup Commands

- Installation instructions
- Environment setup steps
- Dependency management commands
- Database setup if applicable

#### Development Workflow

- How to start development server
- Build commands
- Watch/hot-reload setup
- Package manager specifics (npm, pnpm, yarn, etc.)

#### Testing Instructions

- How to run tests (unit, integration, e2e)
- Test file locations and naming conventions
- Coverage requirements
- Specific test patterns or frameworks used
- How to run subset of tests or focus on specific areas

#### Code Style Guidelines

- Language-specific conventions
- Linting and formatting rules
- File organization patterns
- Naming conventions
- Import/export patterns

#### Build and Deployment

- Build commands and outputs
- Environment configurations
- Deployment steps and requirements
- CI/CD pipeline information

### 3. Optional but Recommended Sections

#### Instruction Files System

- Document any `.github/instructions/` or similar instruction files
- List available instruction files with brief descriptions
- Explain how to use instruction files during development
- Include discovery-first workflows or other key patterns
- Show example task references to instruction files

**Example format:**
```markdown
## Instruction Files System

**Location**: `.github/instructions/`

### Available Instruction Files
1. **[domain].instructions.md** - [Brief description]
2. **[another-domain].instructions.md** - [Brief description]

### Using Instruction Files
- Always check task references for relevant instruction files
- Read instruction files BEFORE implementing
- Apply documented patterns and avoid pitfalls
```

#### Memory Files System (Pitfall Mediation)

- Document any `.github/memory/` memory files
- List available memory files with their focus areas
- Explain relationship between memory files and instruction files
- Note that memory files capture lessons learned and pitfall patterns

**Example format:**
```markdown
## Memory Files (Pitfall Mediation)

**Location**: `.github/memory/`

Memory files capture lessons learned, common pitfalls, and evolving patterns. They complement instruction files with real-world experience.

### Available Memory Files
1. **[domain].memory.instructions.md** - [Pitfalls and lessons learned]
2. **[another-domain].memory.instructions.md** - [Patterns discovered in practice]

### Using Memory Files
- Read memory files to understand common pitfalls before starting work
- Memory files are updated as new lessons are learned
- Use with corresponding instruction files for complete guidance
```

#### Security Considerations

- Security testing requirements
- Secrets management
- Authentication patterns
- Permission models

#### Monorepo Instructions (if applicable)

- How to work with multiple packages
- Cross-package dependencies
- Selective building/testing
- Package-specific commands

#### Pull Request Guidelines

- Title format requirements
- Required checks before submission
- Review process
- Commit message conventions

#### Debugging and Troubleshooting

- Common issues and solutions
- Logging patterns
- Debug configuration
- Performance considerations

## Example Template

Use this as a starting template and customize based on the specific project:

```markdown
# AGENTS.md

## Project Overview

[Brief description of the project, its purpose, and key technologies]

## Setup Commands

- Install dependencies: `[package manager] install`
- Start development server: `[command]`
- Build for production: `[command]`

## Development Workflow

- [Development server startup instructions]
- [Hot reload/watch mode information]
- [Environment variable setup]

## Testing Instructions

- Run all tests: `[command]`
- Run unit tests: `[command]`
- Run integration tests: `[command]`
- Test coverage: `[command]`
- [Specific testing patterns or requirements]

## Code Style

- [Language and framework conventions]
- [Linting rules and commands]
- [Formatting requirements]
- [File organization patterns]

## Build and Deployment

- [Build process details]
- [Output directories]
- [Environment-specific builds]
- [Deployment commands]

## Pull Request Guidelines

- Title format: [component] Brief description
- Required checks: `[lint command]`, `[test command]`
- [Review requirements]

## Additional Notes

- [Any project-specific context]
- [Common gotchas or troubleshooting tips]
- [Performance considerations]
```

## Working Example from agents.md

Here's a real example from the agents.md website:

```markdown
# Sample AGENTS.md file

## Dev environment tips

- Use `pnpm dlx turbo run where <project_name>` to jump to a package instead of scanning with `ls`.
- Run `pnpm install --filter <project_name>` to add the package to your workspace so Vite, ESLint, and TypeScript can see it.
- Use `pnpm create vite@latest <project_name> -- --template react-ts` to spin up a new React + Vite package with TypeScript checks ready.
- Check the name field inside each package's package.json to confirm the right name—skip the top-level one.

## Testing instructions

- Find the CI plan in the .github/workflows folder.
- Run `pnpm turbo run test --filter <project_name>` to run every check defined for that package.
- From the package root you can just call `pnpm test`. The commit should pass all tests before you merge.
- To focus on one step, add the Vitest pattern: `pnpm vitest run -t "<test name>"`.
- Fix any test or type errors until the whole suite is green.
- After moving files or changing imports, run `pnpm lint --filter <project_name>` to be sure ESLint and TypeScript rules still pass.
- Add or update tests for the code you change, even if nobody asked.

## PR instructions

- Title format: [<project_name>] <Title>
- Always run `pnpm lint` and `pnpm test` before committing.
```

## Implementation Steps

### Step 1: Check for Existing AGENTS.md

1. **Check if AGENTS.md exists** at repository root
2. **If exists**: Enter UPDATE MODE (Step 2)
3. **If missing**: Enter CREATE MODE (Step 3)

### Step 2: UPDATE MODE - Verify and Update Existing AGENTS.md

When AGENTS.md already exists, perform comprehensive verification and updates:

#### 2.1 Read and Parse Current AGENTS.md
- Read the entire existing AGENTS.md file
- Parse all sections and their content
- Extract all commands, paths, version numbers, and configurations
- Identify the "Last updated" date if present

#### 2.2 Verify Current Project State
- **Scan project files** to determine actual state:
  - Check package.json, *.csproj, requirements.txt, go.mod, etc. for actual dependencies
  - Check .github/instructions/ for instruction files
  - Check actual directory structure vs documented structure
  - Verify build commands by checking project configuration files
  - Check test frameworks and commands
  - Verify database configurations
  - Check CI/CD workflows

#### 2.3 Identify Inaccuracies and Gaps
Compare documented vs actual state:
- **Outdated versions**: Framework versions, package versions, tool versions
- **Wrong commands**: Commands that no longer work or have changed
- **Missing sections**: New instruction files, new build steps, new testing approaches
- **Incorrect paths**: File paths that have moved or been renamed
- **Deprecated practices**: Old patterns that have been replaced
- **Missing workflows**: New development patterns not documented

#### 2.4 Update Strategy - 100% Replacement Rule
**CRITICAL**: Never patch or comment out. Always replace completely.

For each inaccuracy found:
- **Outdated content**: Delete old content entirely, write new content from scratch
- **Wrong information**: Replace entire paragraph/section with accurate information
- **Missing sections**: Add complete new sections with full detail
- **Incorrect examples**: Delete incorrect examples, provide new accurate examples
- **Deprecated patterns**: Remove deprecated content entirely, add current patterns

**Example of correct approach:**
```
❌ WRONG (Patching):
## Testing
<!-- OLD: npm test -->
pnpm test

✅ CORRECT (Complete replacement):
## Testing

**Test Framework**: Vitest with React Testing Library

```powershell
# Run all tests
pnpm test

# Run tests in watch mode
pnpm test:watch

# Run tests with coverage
pnpm test:coverage

# Run specific test file
pnpm test path/to/test.spec.ts
```
```

#### 2.5 Section-by-Section Verification Checklist

For each section, verify:

- [ ] **Project Overview**
  - Accurate description of current project state
  - Current key technologies (not outdated versions)
  - Current architecture (reflect any refactoring)

- [ ] **Instruction Files System** (if applicable)
  - Scan .github/instructions/ directory
  - List ALL instruction files (not just some)
  - Verify each file still exists
  - Add any new instruction files
  - Remove references to deleted instruction files
  - Update descriptions if file content has changed

- [ ] **Memory Files System** (if applicable)
  - Scan .github/memory/ directory
  - List ALL memory files (pitfall mediation, lessons learned)
  - Verify each memory file still exists
  - Add any new memory files discovered
  - Note which memory files complement which instruction files
  - Update descriptions if patterns have evolved

- [ ] **Setup Commands**
  - Test each command works as documented
  - Update package manager if changed (npm→pnpm, etc.)
  - Verify database setup steps are current
  - Check environment variable requirements

- [ ] **Development Workflow**
  - Verify start commands work
  - Check build commands produce expected output
  - Validate watch/hot-reload setup
  - Update any changed development practices

- [ ] **Testing Instructions**
  - Verify test commands work
  - Check test framework versions
  - Update test file patterns if changed
  - Validate coverage commands
  - Update integration test procedures

- [ ] **Code Style Guidelines**
  - Check linter configuration is current
  - Verify formatter settings
  - Update naming conventions if changed
  - Reflect any new coding standards

- [ ] **Build and Deployment**
  - Verify build commands work
  - Check output directories are correct
  - Update deployment procedures if changed
  - Validate CI/CD information

- [ ] **Database/Data Access Patterns** (if applicable)
  - Verify connection patterns are current
  - Check stored procedure conventions
  - Update DAO patterns if refactored
  - Validate migration procedures

- [ ] **Security Considerations**
  - Update credential management practices
  - Check authentication patterns
  - Verify secrets management approach

- [ ] **Pull Request Guidelines**
  - Verify PR title format is current
  - Check required checks match CI/CD
  - Update review process if changed

#### 2.6 Perform Complete Rewrite
Using verified information:
- Delete the entire existing AGENTS.md file content
- Write a completely new AGENTS.md from scratch using verified information
- Update "Last updated" date to current date
- Ensure NO outdated information survives
- Include ALL relevant sections (don't skip sections that still apply)

### Step 3: CREATE MODE - Generate New AGENTS.md

When AGENTS.md doesn't exist, create from scratch:

#### 3.1 Analyze the project structure to understand:

   - Programming languages and frameworks used
   - Package managers and build tools
   - Testing frameworks
   - Project architecture (monorepo, single package, etc.)

#### 3.2 Scan for instruction and memory files:
   - Check .github/instructions/*.instructions.md (stable patterns)
   - Check .github/memory/*.memory.instructions.md (pitfall mediation, lessons learned)
   - Check .specify/instructions/*.instructions.md
   - Check vscode-userdata references in copilot-instructions.md

#### 3.3 Identify key workflows by examining:

   - package.json scripts
   - Makefile or other build files
   - CI/CD configuration files
   - Documentation files

#### 3.4 Create comprehensive sections covering:

   - All essential setup and development commands
   - Instruction files system (if instruction files exist)
   - Testing strategies and commands
   - Code style and conventions
   - Build and deployment processes

#### 3.5 Include specific, actionable commands that agents can execute directly

#### 3.6 Test the instructions by ensuring all commands work as documented

#### 3.7 Keep it focused on what agents need to know, not general project information

### Step 4: Final Validation

Whether updating or creating:
- **Verify all commands** can be executed successfully
- **Check all file paths** exist
- **Validate version numbers** match actual installed versions
- **Test build procedures** work as documented
- **Confirm instruction file references** point to existing files
- **Confirm memory file references** point to existing files
- **Update "Last updated" date** to current date

### Step 5: Report Changes (UPDATE MODE only)

If updating existing AGENTS.md, provide a summary of changes:
- List sections that were updated
- Note new sections added
- List removed outdated content
- Highlight any breaking changes in commands
- Confirm total rewrite was performed (not patching)

**Example report format:**
```markdown
## AGENTS.md Update Report

**Previous version**: 2025-10-14
**Current version**: 2025-10-18

### Sections Updated
- **Instruction Files System**: Added 1 new instruction file (integration-testing.instructions.md)
- **Memory Files System**: NEW SECTION - Added documentation for 7 memory files
- **Testing Instructions**: Updated test commands to reflect MSTest framework
- **Database Operations Pattern**: Fixed incorrect stored procedure parameter naming

### Sections Added
- Memory Files (Pitfall Mediation) - Complete new section

### Content Removed
- Outdated Avalonia/MVVM references (replaced with WinForms patterns)
- Incorrect connection string examples (replaced with environment-aware patterns)

### Breaking Changes
- None - all existing commands still work

### Verification Status
✅ All commands tested and verified
✅ All file paths confirmed to exist
✅ All instruction file references validated
✅ All memory file references validated
✅ Build procedures tested successfully
```

## Best Practices

- **Be specific**: Include exact commands, not vague descriptions
- **Use code blocks**: Wrap commands in backticks for clarity
- **Include context**: Explain why certain steps are needed
- **Stay current**: Update as the project evolves
- **Test commands**: Ensure all listed commands actually work
- **Consider nested files**: For monorepos, create AGENTS.md files in subprojects as needed

## Monorepo Considerations

For large monorepos:

- Place a main AGENTS.md at the repository root
- Create additional AGENTS.md files in subproject directories
- The closest AGENTS.md file takes precedence for any given location
- Include navigation tips between packages/projects

## Final Notes

- AGENTS.md works with 20+ AI coding tools including Cursor, Aider, Gemini CLI, and many others
- The format is intentionally flexible - adapt it to your project's needs
- Focus on actionable instructions that help agents understand and work with your codebase
- This is living documentation - update it as your project evolves

When creating the AGENTS.md file, prioritize clarity, completeness, and actionability. The goal is to give any coding agent enough context to effectively contribute to the project without requiring additional human guidance.
