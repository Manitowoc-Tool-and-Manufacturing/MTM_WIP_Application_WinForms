# Test Fix Workspace

**Purpose**: Organized workspace for fixing 23 failing integration tests  
**Status**: 113/136 tests passing (83.1%) → Goal: 136/136 (100%)  
**Created**: 2025-10-19  
**Branch**: 002-003-database-layer-complete

---

## 🚀 Quick Start

**New to this workspace? Start here:**

1. **Understand the structure**: Read [TOC.md](TOC.md) (2-minute overview)
2. **See the big picture**: Check [DASHBOARD.md](DASHBOARD.md) (visual progress)
3. **Pick a task**: Start with [Category 1](categories/01-quick-buttons.md) (highest priority)
4. **Use templates**: Copy patterns from [reference/](reference/) folder
5. **Track progress**: Update category files as you fix tests

---

## 📁 Workspace Structure

```
test-fix-workspace/
├── TOC.md                    # Table of contents and navigation hub
├── DASHBOARD.md              # Visual progress tracking and metrics
├── README.md                 # This file
│
├── categories/               # Test failure tracking by category
│   ├── 01-quick-buttons.md         # 10 tests - HIGH priority
│   ├── 02-system-dao.md            # 6 tests - MEDIUM priority
│   ├── 03-helper-validation.md     # 5 tests - LOW priority
│   └── 04-phase1-failures.md       # 2 tests - INVESTIGATION needed
│
├── reference/                # Quick reference documentation
│   ├── mcp-tools-quick.md          # MCP tools quick reference
│   ├── mcp-tools-full.md           # Comprehensive MCP documentation
│   ├── sql-templates.md            # SQL test data templates
│   ├── powershell-commands.md      # PowerShell command reference
│   └── test-patterns.md            # C# test development patterns
│
├── tools/                    # Automation scripts (future)
│   └── [PowerShell scripts placeholder]
│
└── history/                  # Session history and investigations
    ├── CHANGELOG.md                # Quick session index
    ├── sessions/                   # Detailed session logs
    │   └── SESSION-TEMPLATE.md
    └── investigations/             # Deep-dive problem analysis
        └── INVESTIGATION-TEMPLATE.md
```

---

## 🎯 Current Status

### Overall Progress

```
Tests Passing:  113/136 (83.1%)
Tests Failing:  23
Goal:           136/136 (100%)

Progress: [████████████████░░░░] 83.1%
```

### By Category

| Category | Priority | Tests | Fixed | % | Effort Est. |
|----------|----------|-------|-------|---|-------------|
| Quick Buttons | 🔴 HIGH | 10 | 0 | 0% | 3-4 hours |
| System DAO | 🟡 MEDIUM | 6 | 0 | 0% | 2-3 hours |
| Helpers | 🟢 LOW | 5 | 0 | 0% | 2-3 hours |
| Phase 1 Failures | 🟠 INVESTIGATION | 2 | 0 | 0% | 1-2 hours |

**Total Estimated Effort**: 8-12 hours

---

## 🗺️ Recommended Workflow

### For Developers

1. **Start with high priority**: Category 1 (Quick Buttons)
2. **Read category file**: Understand root cause and fix strategy
3. **Use templates**: SQL and C# patterns are ready to copy
4. **Fix tests incrementally**: One test at a time, verify each
5. **Update progress**: Check off tests in category file
6. **Regenerate dashboard**: Update DASHBOARD.md when category complete

### For AI Agents

1. **Follow clarification protocol**: See TOC.md "AI-Human Clarification Protocol"
2. **Use discovery-first workflow**: Verify method signatures before coding
3. **Apply null-safe patterns**: Follow DaoResult checking patterns
4. **Document learnings**: Capture insights in session logs
5. **Respect test isolation**: Ensure tests don't interfere with each other

---

## 📚 Key Documents

### Navigation & Planning

- **[TOC.md](TOC.md)** - Main navigation hub, clarification protocol
- **[DASHBOARD.md](DASHBOARD.md)** - Visual progress tracking, metrics, velocity

### Test Categories (Root Cause Analysis)

- **[Category 1: Quick Buttons](categories/01-quick-buttons.md)** - Missing test data setup
- **[Category 2: System DAO](categories/02-system-dao.md)** - Missing test users
- **[Category 3: Helpers](categories/03-helper-validation.md)** - Edge cases and validation
- **[Category 4: Phase 1 Failures](categories/04-phase1-failures.md)** - Investigation needed

### Reference Documentation

- **[MCP Tools Quick Reference](reference/mcp-tools-quick.md)** - 26 tools by category
- **[MCP Tools Full Guide](reference/mcp-tools-full.md)** - Detailed tool documentation
- **[SQL Templates](reference/sql-templates.md)** - Test data setup SQL
- **[PowerShell Commands](reference/powershell-commands.md)** - Build, test, database commands
- **[Test Patterns](reference/test-patterns.md)** - C# integration test templates

### History & Tracking

- **[CHANGELOG](history/CHANGELOG.md)** - Quick session index
- **[Session Template](history/sessions/SESSION-TEMPLATE.md)** - Document work sessions
- **[Investigation Template](history/investigations/INVESTIGATION-TEMPLATE.md)** - Deep-dive analysis

---

## 🛠️ Tools & Resources

### MCP Tools (mtm-workflow server)

**Most useful for test fixing**:
- `generate_test_seed_sql` - Create test data SQL from JSON
- `verify_test_seed` - Validate test data exists
- `validate_dao_patterns` - Check DAO code compliance
- `validate_build` - Verify compilation after changes

[Full tool reference →](reference/mcp-tools-quick.md)

### PowerShell Commands

**Most common**:
```powershell
# Build Debug configuration
dotnet build MTM_Inventory_Application.csproj -c Debug

# Run Category 1 tests
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Connect to test database
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test
```

[Full command reference →](reference/powershell-commands.md)

### SQL Templates

**Test user creation**:
```sql
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID;
```

[Full template collection →](reference/sql-templates.md)

---

## 🎓 Learning Resources

### New to Integration Testing?

- Start with [Test Patterns](reference/test-patterns.md) for C# examples
- Read [Discovery-First Workflow](reference/test-patterns.md#discovery-first-workflow) to avoid mistakes
- Follow [Null-Safe DaoResult Pattern](reference/test-patterns.md#null-safe-daoresult-pattern)

### Understanding Root Causes?

- Each category file has detailed root cause analysis
- MCP tools used for investigation documented
- Fix strategies provided with code examples

### Stuck on a Test?

1. Read the category file for that test
2. Check if test needs test data setup (Categories 1 & 2)
3. Use discovery-first workflow to verify method signature
4. Review similar tests in test-patterns.md
5. Check MCP tools reference for investigation tools

---

## 📊 Success Metrics

### Quality Gates

**Before marking category complete**:
- [ ] All tests in category passing
- [ ] Tests run successfully multiple times (idempotent)
- [ ] No test interference (proper isolation)
- [ ] Category file updated with checkboxes
- [ ] DASHBOARD.md updated with new metrics

### Velocity Tracking

**Historical Data** (Phase 1):
- Tests fixed: 7/9
- Time taken: 2.5 hours
- Average: ~21 minutes per test

**Current Sprint**: TBD (will update after first fixes)

---

## 🔄 Workflow Integration

### With Existing MTM Patterns

- Follows `.github/instructions/integration-testing.instructions.md`
- Uses `BaseIntegrationTest` class patterns
- Respects discovery-first workflow
- Applies null-safe DaoResult checking

### With MCP Tools

- Uses mtm-workflow server tools
- Documented in reference/mcp-tools-*.md
- JSON configs in `.mcp/samples/`
- Integration harness support

### With Test Automation (Future)

- PowerShell scripts placeholder in `tools/`
- Dashboard regeneration automation
- Progress tracking automation
- Test execution automation

---

## 📝 Contributing Guidelines

### Updating Category Files

When fixing a test:
1. Check off the test: `- [x] TestName`
2. Update progress count: `0/10 → 1/10`
3. Update progress bar: `[░░...] → [█░...]`
4. Add notes if needed in "Investigation Notes" section

### Creating Session Logs

For significant work sessions:
1. Copy `history/sessions/SESSION-TEMPLATE.md`
2. Rename: `YYYY-MM-DD-session-name.md`
3. Fill in all sections
4. Add entry to `history/CHANGELOG.md`

### Deep-Dive Investigations

For complex problems:
1. Copy `history/investigations/INVESTIGATION-TEMPLATE.md`
2. Rename: `YYYY-MM-DD-problem-name.md`
3. Document hypothesis testing process
4. Capture root cause analysis

---

## 🚦 Status Indicators

### Priority Levels

- 🔴 **HIGH** - Core features, blocking others, high user impact
- 🟡 **MEDIUM** - Important features, moderate impact
- 🟢 **LOW** - Nice to have, lower priority
- 🟠 **INVESTIGATION** - Unknown root cause, needs analysis

### Test Status

- ✅ **Passing** - Test works correctly
- ❌ **Failing** - Test needs fixing
- ⚠️ **In Progress** - Currently being fixed
- 🚫 **Blocked** - Waiting on dependency

### Completion Tracking

- `[█]` - Work completed
- `[░]` - Work remaining
- Progress bars show visual completion percentage

---

## 🎯 Next Steps

### Immediate (First Session)

1. **Review Category 1**: [categories/01-quick-buttons.md](categories/01-quick-buttons.md)
2. **Create test data helpers**: BaseIntegrationTest setup methods
3. **Fix first test**: AddQuickButton_ValidData_InsertsButton
4. **Verify fix**: Run test multiple times to ensure idempotency
5. **Update progress**: Check off test in category file

### Short Term (This Week)

1. Complete Category 1 (10 tests)
2. Complete Category 2 (6 tests)
3. Start Category 3 investigation
4. Document session in history/

### Long Term (This Sprint)

1. Complete all 4 categories
2. Achieve 136/136 tests passing (100%)
3. Document lessons learned
4. Consider automation scripts (Feature 6)

---

## 📞 Support & Questions

### For Clarifications

- Review TOC.md "AI-Human Clarification Protocol"
- Check if answer is in reference/ documentation
- Ask specific questions with context

### For Technical Issues

- Check category file for root cause analysis
- Review similar tests in test-patterns.md
- Consult MCP tools documentation
- Create investigation deep-dive if complex

---

## 📈 Version History

### Version 1.0 (2025-10-19)

**Initial Workspace Creation**

**Features Implemented**:
- ✅ Feature 1: Workspace Foundation (TOC.md, folder structure)
- ✅ Feature 2: Category Tracking (4 category files)
- ✅ Feature 3: MCP Tools Reference (2-tier documentation)
- ✅ Feature 4: Test Templates (SQL, PowerShell, C# patterns)
- ✅ Feature 5: Progress Dashboard (visual metrics)
- ✅ Feature 6: Automation Scripts (placeholders + templates)
- ✅ Feature 7: Session History (CHANGELOG, templates)

**Documentation**:
- 20+ markdown files created
- 4 categories with root cause analysis
- 26 MCP tools documented
- 50+ PowerShell commands
- 15+ C# test patterns
- 10+ SQL templates

**Estimated Value**: 8-12 hours saved through organized workflow

---

## 🙏 Acknowledgments

**Built on Foundation Of**:
- MTM WIP Application architecture
- Phase 1-2.5 database standardization work
- Integration testing instruction files
- MCP tools ecosystem

**Patterns Derived From**:
- `.github/instructions/integration-testing.instructions.md`
- Existing successful test fixes (Phase 1)
- MTM coding standards and best practices

---

**Workspace Version**: 1.0  
**Last Updated**: 2025-10-19  
**Maintained by**: Development Team  
**Branch**: 002-003-database-layer-complete

---

**[← Back to TOC](TOC.md)** | **[View Dashboard →](DASHBOARD.md)**
