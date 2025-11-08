# Spec-Driven Refactor - Quick Reference

## One-Liner
**Automatically refactor C# files by discovering and applying patterns from ALL completed spec tasks.**

## Usage
```
> Run Prompt: spec-compliance-refactor
Input: C:\...\Forms\MainForm\MainForm.cs
```

## What It Does (4 Phases)

### Phase 0: Discovery (10-15 min, cached)
- Scans ALL specs (active + archived)
- Extracts completed tasks (`[X]` or `[✓]`)
- Validates implementation in codebase
- Builds compliance rules database

### Phase 1: Analysis (5-10 min)
- Reads target file + dependencies
- Applies ALL rules from database
- Detects violations by priority

### Phase 2: Remediation (Variable)
- Fixes P1 (CRITICAL) violations first
- Fixes P2 (HIGH) violations
- Fixes P3 (MEDIUM) where feasible
- Method-by-method processing

### Phase 3: Validation (5 min)
- Runs MCP tools
- Verifies compilation
- Generates change report
- Updates PatchNotes.md

## Priority Levels

| Priority | Category | Examples | Impact |
|----------|----------|----------|---------|
| **P1** | CRITICAL | Column naming, transactions | Runtime errors, data corruption |
| **P2** | HIGH | Model_Dao_Result, Service_ErrorHandler | Spec compliance, architecture |
| **P3** | MEDIUM | Service_DebugTracer, XML docs | Code quality, maintainability |
| **P4** | LOW | Naming conventions, comments | Style, readability |

## Output

1. ✅ **Refactored Files** (target + dependencies)
2. 📊 **Change Report** (before/after, spec references)
3. ✅ **Testing Checklist** (manual validation steps)
4. 📝 **PatchNotes.md** (updated if compliant)
5. 🔗 **Traceability** (every change → spec → task)

## Key Features

### 🚀 Self-Updating
Automatically includes new specs when added

### 🔍 Evidence-Based
Only applies validated-as-implemented patterns

### 📈 Comprehensive
Covers ALL specs, ALL pattern types

### 🎯 Prioritized
Critical violations fixed first

### 🔗 Traceable
Links every change to source spec

## Common Scenarios

### Scenario 1: Modernize Legacy File
```
Input: Forms/Legacy/OldDialog.cs
Output: 25 patterns applied from 5 specs
Compliance: 45% → 94%
```

### Scenario 2: New Developer Onboarding
```
Input: Forms/NewFeature/NewForm.cs
Output: 15 patterns applied, preventing common mistakes
Compliance: 72% → 96%
```

### Scenario 3: Pre-Deployment Check
```
Input: Controls/MainForm/Control_NewTab.cs
Output: 12 patterns applied, 3 issues documented
Compliance: 83% → 92%
```

## vs. database-compliance-reviewer

| Feature | database-compliance-reviewer | spec-compliance-refactor |
|---------|------------------------------|--------------------------|
| Rule Source | Hardcoded from 1 spec | Discovered from ALL specs |
| Adaptability | Manual updates | Automatic |
| Scope | Database patterns only | Any pattern type |
| Future-Proof | ❌ Requires updates | ✅ Self-updating |

## Caching

### First Run
- Time: ~15 minutes
- Generates: `compliance-rules.md`
- Cached: `.github/cache/`

### Subsequent Runs
- Time: ~5 minutes
- Uses: Cached rules (24h TTL)

### Force Refresh
```
Add --fresh flag (future enhancement)
```

## Safety Features

✅ Incremental fixes (method-by-method)  
✅ Compilation verification  
✅ MCP tool validation  
✅ Auto-rollback on failure  
✅ Manual testing checklist  

## When NOT to Use

❌ Brand new prototype code  
❌ Third-party integration files  
❌ Code with intentional deviations  

## Configuration

### Exclude Rules
`.github/config/spec-refactor-exclude.json`
```json
{
  "excludeRules": ["FR-004"],
  "excludeFiles": ["Tests/**/*.cs"]
}
```

### Customize Priorities
Edit `compliance-rules.md` directly

### Add Custom Rules
Add manually to `compliance-rules.md`

## Troubleshooting

### Cache Stale?
Delete `.github/cache/compliance-rules.md`

### Rule Wrong?
Edit `compliance-rules.md` to adjust pattern

### Compilation Fails?
Check logs - prompt should auto-rollback

## Example Report Snippet

```markdown
## Summary
- Rules Applied: 12 from 3 specifications
- Violations Fixed: 23 (5 CRITICAL, 12 HIGH, 6 MEDIUM)
- Compliance: 68% → 96%

## FR-012: Column Name Pattern (CRITICAL)
Line 234: row["p_Operation"] → row["Operation"]
Line 456: drv["p_User"] → drv["User"]

Impact: Prevents runtime crash
Source: specs/Archives/003-database-layer-refresh/tasks.md (T113)
```

## Performance

| Operation | Time | Notes |
|-----------|------|-------|
| First Run | 15 min | Includes spec discovery |
| Cached Run | 5 min | Uses compliance-rules.md |
| Per File | Variable | Depends on violations |

## MCP Tools Used

Phase 0: `analyze_spec_context`, `check_checklists`  
Phase 1: N/A  
Phase 2: N/A  
Phase 3: `validate_dao_patterns`, `validate_error_handling`, `check_xml_docs`, `analyze_performance`, `check_security`, `validate_build`

## Next Steps After Refactoring

1. ✅ Review change report
2. ✅ Execute manual testing checklist
3. ✅ Review git diff
4. ✅ Commit with generated message template
5. ✅ Deploy with confidence

---

**For Full Documentation**: See `spec-compliance-refactor.README.md`  
**Prompt File**: `.github/prompts/spec-compliance-refactor.prompt.md`  
**Created**: 2025-10-24
