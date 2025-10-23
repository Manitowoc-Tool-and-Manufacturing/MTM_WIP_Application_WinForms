# Test Fix Workspace - Table of Contents

**Last Updated**: 2025-10-19  
**Purpose**: Navigation hub for organized test fixing workflow  
**Branch**: 002-003-database-layer-complete

---

## 🎯 Quick Status

**Current**: 136/136 passing (100%) 🎉✅  
**Goal**: 136/136 passing (100%) ✅ ACHIEVED!  
**Remaining**: 0 tests  
**All Categories**: ✅ COMPLETE  
**Next Priority**: Production deployment preparation  
**Latest Update**: 2025-10-22 - 🎉 100% TEST COVERAGE ACHIEVED! All 23 originally failing tests fixed.

---

## ⚡ Quick Start Commands

```powershell
# Run all integration tests
dotnet test --filter "FullyQualifiedName~Integration"

# Run specific category
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons"

# Update progress after fixing tests
.\tools\Update-Progress.ps1 -TestName "TestName" -Category 1

# Regenerate dashboard
.\tools\New-Dashboard.ps1
```

---

## 📂 Workspace Navigation

### Test Categories (Active Work)

Track progress for each failure category with checkboxes and fix strategies:

- **[Category 1: Quick Button Failures](categories/01-quick-buttons.md)** - 12 tests ✅ **COMPLETE** (100% passing)
- **[Category 2: System DAO Failures](categories/02-system-dao.md)** - 14 tests ✅ **COMPLETE** (100% passing)
- **[Category 3: Helper & Validation Tests](categories/03-helper-validation.md)** - 5 tests, LOW priority 🟡 **Next to fix**
- **[Category 4: Phase 1 New Failures](categories/04-phase1-failures.md)** - 1 test, INVESTIGATION needed 🟠

### Reference Materials (Read-Only)

Documentation and templates for test development:

- **[MCP Tools Quick Reference](reference/mcp-tools-quick.md)** - Fast lookup of tool names and purposes
- **[MCP Tools Full Guide](reference/mcp-tools-full.md)** - Complete documentation with examples
- **[SQL Test Templates](reference/sql-templates.md)** - Stored procedure testing patterns
- **[PowerShell Commands](reference/powershell-commands.md)** - Build, test, and database commands
- **[Test Development Patterns](reference/test-patterns.md)** - Integration test templates and examples

### Automation Tools (Use with Caution)

⚠️ **Safety Note**: All scripts support `-WhatIf` mode. Always test before applying changes.

- **[Update-Progress.ps1](tools/Update-Progress.ps1)** - Mark tests complete in category files
- **[New-Dashboard.ps1](tools/New-Dashboard.ps1)** - Regenerate dashboard from category files
- **[Invoke-CategoryTests.ps1](tools/Invoke-CategoryTests.ps1)** - Run tests by category with correct filters
- **[Test-WorkspaceStructure.ps1](tools/Test-WorkspaceStructure.ps1)** - Validate workspace integrity
- **[Backup-WorkspaceFile.ps1](tools/Backup-WorkspaceFile.ps1)** - Create timestamped backups

### Progress & History

Track progress and document sessions:

- **[DASHBOARD.md](DASHBOARD.md)** - Visual progress dashboard with metrics
- **[Session History Changelog](history/CHANGELOG.md)** - Index of all work sessions
- **[Session Logs](history/sessions/)** - Detailed documentation of each session
- **[Investigation Deep-Dives](history/investigations/)** - Special investigation findings

---

## 🤝 AI-Human Clarification Protocol

### When I Need Clarification

If I encounter ambiguity while working on fixes, I will **STOP** and ask you a question in this format:

**Question**: [Clear, non-jargon question]

**Why I'm asking**: [Explanation of why this matters]

**Your options**:
- **A**: [Option 1 with plain explanation]
- **B**: [Option 2 with plain explanation]
- **C**: [Option 3 with plain explanation]
- **Custom**: [How to provide your own answer]

**Suggested answer**: [Letter] - [Why this is recommended]

Please respond with: "A" or "B" or "C" or "Custom: [your answer]"

---

### When to Ask vs Assume

**I WILL ask when**:
- Fix strategy has multiple approaches with different trade-offs
- Test data setup could be done in stored procedure OR C# helper
- Scope decision affects other categories (e.g., migrate tasks or skip)
- Root cause is unclear and multiple theories exist
- Decision has significant time/effort implications

**I will NOT ask when**:
- Reasonable industry standard exists (follow .NET testing conventions)
- Documentation clearly states preference (follow BaseIntegrationTest patterns)
- Choice has no significant impact (file naming, comment style)
- Pattern is established in existing code (follow existing conventions)
- Instruction files provide clear guidance

---

### Example Clarification

**Question**: Should we create test data setup helpers in C# or use stored procedures?

**Why I'm asking**: Quick button tests need test user and button records. We could create this data via C# helper methods in BaseIntegrationTest, or via SQL stored procedures that tests call before execution. This affects all future test data setup patterns.

**Your options**:
- **A**: **C# Helper Methods** - Keep test data setup in BaseIntegrationTest C# helpers. Easier to maintain, type-safe, can reuse across tests.
- **B**: **Stored Procedures** - Create sp_CreateTestUsers and sp_CreateTestQuickButtons procedures. Consistent with DAO pattern, can be used outside tests.
- **C**: **Hybrid Approach** - Use stored procedures for complex setup, C# helpers for simple cases. Most flexible but adds decision overhead.

**Suggested answer**: A - C# helpers are easier to maintain and tests already use BaseIntegrationTest base class

---

## 📚 Workspace Purpose

### Why This Structure Exists

**Problem Solved**: Monolithic checklist files (1000+ lines) where finding information requires extensive searching, progress updates touch the same file repeatedly, and historical context mixes with active work.

**Solution**: Modular, folder-based organization where each piece of information has a single home, navigation is intuitive, and developers can find what they need in under 1 minute.

### When to Use Each Folder

**categories/**: Working on specific test fixes, updating progress, checking fix strategies

**reference/**: Looking up tool usage, finding templates, copying commands

**tools/**: Automating repetitive tasks (use with `-WhatIf` first!)

**history/**: Understanding past decisions, finding investigation findings, documenting new sessions

### How to Add New Content

**New test failure discovered?** 
- Add to appropriate category file if it fits existing groups
- Create new category file if it represents new pattern (rare)

**New MCP tool added?**
- Add to both mcp-tools-quick.md and mcp-tools-full.md
- Include examples from actual project usage

**New script created?**
- Add to tools/ folder with complete help documentation
- Ensure safety features (backup, validation, dry-run)
- Link from this TOC

**Session completed?**
- Document in history/sessions/ using template
- Add summary entry to history/CHANGELOG.md
- Update DASHBOARD.md with results

---

## 📏 File Organization Rules

### File Placement

- **TOC.md**: Navigation and protocols only. No detailed content.
- **categories/**: Test failure tracking only. No reference material.
- **reference/**: Read-only documentation. No progress tracking.
- **tools/**: Executable scripts only. No documentation.
- **history/**: Historical logs only. No active work.

### File Size Limits

- TOC.md: Under 300 lines (this file)
- Category files: Under 300 lines each
- Reference files: Under 500 lines each
- Tool scripts: Under 200 lines with comments
- Session logs: No strict limit (comprehensive > brief)

### Linking Rules

- Use relative links between workspace files: `[Category 1](categories/01-quick-buttons.md)`
- Absolute paths only for C# code files: `Data/Dao_QuickButtons.cs`
- Link to sections using anchors: `[Section](#section-name)`
- External links must include context: `[MySQL Docs](url) - Parameter binding syntax`

---

## 🎓 Plain Language Commitment

All workspace documentation follows these principles:

- **Avoid jargon**: Use clear terms or explain technical concepts on first use
- **Explain acronyms**: DAO = Data Access Object, SP = Stored Procedure
- **Use concrete examples**: Show real code/commands instead of abstract descriptions
- **Write for newcomers**: Assume reader is unfamiliar with specific patterns

**Example**:
- ❌ "Refactor DAO to leverage SP params pattern"
- ✅ "Update the Data Access Object to pass parameters to stored procedures using the p_ prefix convention"

---

## 🔧 Workspace Maintenance

### Regular Updates

**After each test fixing session**:
1. Update category file checkboxes for fixed tests
2. Regenerate DASHBOARD.md to reflect progress
3. Document session in history/ if significant

**Weekly**:
1. Run Test-WorkspaceStructure.ps1 to validate integrity
2. Review and archive completed categories
3. Update velocity metrics in DASHBOARD.md

**When structure changes**:
1. Update this TOC.md immediately
2. Fix broken links in all files
3. Document change rationale in history/CHANGELOG.md

---

## 📞 Getting Help

### Workspace Issues

- **Can't find file?** Use VS Code search (Ctrl+Shift+F) or check this TOC
- **Link broken?** Run Test-WorkspaceStructure.ps1 to find all broken links
- **Math doesn't add up?** Run New-Dashboard.ps1 to recalculate from category files
- **Script failing?** Run with `-WhatIf` first and check Get-Help for usage

### Test Fixing Issues

- **Don't know how to test SP?** See [SQL Test Templates](reference/sql-templates.md)
- **Need PowerShell command?** See [PowerShell Commands](reference/powershell-commands.md)
- **Writing integration test?** See [Test Development Patterns](reference/test-patterns.md)
- **Which MCP tool to use?** See [MCP Tools Quick Reference](reference/mcp-tools-quick.md)

---

## ✅ Quality Checklist

Before considering workspace "complete":

- [ ] All 23 test failures addressed in category files
- [ ] All category files have checkbox progress tracking
- [ ] All reference documentation is complete and accurate
- [ ] All automation scripts have safety features and help docs
- [ ] All significant sessions documented in history/
- [ ] DASHBOARD.md reflects current accurate status
- [ ] No broken links (validated with Test-WorkspaceStructure.ps1)
- [ ] File size limits respected
- [ ] Navigation from TOC takes < 30 seconds to any file

---

**Workspace Version**: 1.0  
**Created**: 2025-10-19  
**Maintained by**: Development Team
