# Spec-Driven Code Compliance Refactor

**Location**: `.github/prompts/spec-compliance-refactor.prompt.md`

## What It Does

This prompt automatically refactors C# files by discovering and applying ALL patterns from completed specification tasks across your entire `specs/` directory structure.

## Key Differences from database-compliance-reviewer.prompt.md

| Feature | Database Compliance Reviewer | Spec Compliance Refactor |
|---------|------------------------------|--------------------------|
| **Scope** | Fixed rules from one spec (003-database-layer-refresh) | Dynamic rules discovered from ALL specs |
| **Rule Source** | Hardcoded FR-003, FR-006, FR-008, etc. | Automatically extracted from tasks.md files |
| **Adaptability** | Must be manually updated for new specs | Automatically includes new specs when added |
| **Validation** | Assumes rules are implemented | Validates implementation in codebase before applying |
| **Future-Proof** | Requires prompt updates | Self-updating as specs are added |
| **Coverage** | Database-specific patterns | Any pattern from any completed spec task |

## How It Works

### Phase 0: Spec Discovery (10-15 minutes first run, cached thereafter)

1. **Scans** all `specs/` and `specs/Archives/` directories
2. **Reads** every `tasks.md` file
3. **Extracts** completed tasks (marked with `[X]` or `[✓]`)
4. **Validates** each task is actually implemented in the codebase:
   - Searches for expected pattern (should be common)
   - Searches for old pattern (should be rare)
   - Only includes rule if evidence confirms implementation
5. **Builds** `compliance-rules.md` database with:
   - Rule ID and description
   - Source spec and task reference
   - Before/after code patterns
   - Violation detection regex
   - Remediation steps
   - Priority level (P1-P4)

### Phase 1: Analysis (5-10 minutes)

1. **Reads** target file
2. **Applies** ALL rules from compliance database
3. **Detects** violations with line numbers
4. **Reports** findings by priority

### Phase 2: Remediation (Variable time)

1. **Fixes** Priority 1 (CRITICAL) violations first
2. **Fixes** Priority 2 (HIGH) violations
3. **Fixes** Priority 3 (MEDIUM) violations where feasible
4. **Processes** method-by-method with full traceability

### Phase 3: Validation (5 minutes)

1. **Runs** MCP validation tools
2. **Verifies** compilation
3. **Generates** comprehensive change report
4. **Updates** PatchNotes.md
5. **Creates** manual testing checklist

## Usage

### VS Code Command Palette

```
> Run Prompt: spec-compliance-refactor
```

**When prompted**, provide absolute file path:
```
C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Forms\MainForm\MainForm.cs
```

### What You Get

1. **Refactored File(s)**: Target file + dependencies fixed
2. **Change Report**: Detailed markdown report showing:
   - Which specs were consulted
   - Which rules were applied
   - Before/after code snippets
   - Compliance score improvement
3. **Testing Checklist**: Manual validation steps
4. **Updated PatchNotes.md**: If fully compliant
5. **Traceability**: Every change linked to source spec and task

## Example Output

```markdown
# Spec-Driven Refactoring Report

## Summary
- **File**: `Forms/MainForm/MainForm.cs`
- **Rules Applied**: 12 from 3 specifications
- **Violations Fixed**: 23 (5 CRITICAL, 12 HIGH, 6 MEDIUM)
- **Compliance**: 68% → 96%

## Specifications Consulted
1. `specs/Archives/003-database-layer-refresh` (T100-T132)
   - Applied 8 rules, fixed 15 violations
2. `specs/Archives/002-comprehensive-database-layer` (T050-T099)
   - Applied 4 rules, fixed 8 violations

## Changes by Rule

### FR-012: Column Name Pattern (CRITICAL)
**Source**: specs/Archives/003-database-layer-refresh/tasks.md (Task T113)
**Fixed**: 5 violations

Line 234: `row["p_Operation"]` → `row["Operation"]`
Line 456: `drv["p_User"]` → `drv["User"]`
...

[Full report continues...]
```

## Caching and Performance

### First Run
- **Time**: ~15 minutes (spec discovery + analysis + fixes)
- **Output**: `compliance-rules.md` cached in `.github/cache/`

### Subsequent Runs
- **Time**: ~5 minutes (uses cached rules)
- **Cache Invalidation**: Automatic after 24 hours or when specs change

### Force Refresh
Add `--fresh` flag to regenerate compliance rules database

## Advantages

### 1. Self-Updating
When you add a new spec with completed tasks, the prompt automatically:
- Discovers the new spec
- Extracts new compliance rules
- Applies them to files

### 2. Evidence-Based
Only applies rules that are proven to be implemented in the codebase:
- Searches for pattern adoption (>= 3 occurrences)
- Verifies old patterns are rare (<= 2 occurrences)
- Ensures rule is not just theoretical

### 3. Comprehensive
Unlike database-compliance-reviewer which focuses on one spec:
- Covers ALL specs (archived and active)
- Includes ALL pattern types (database, UI, error handling, etc.)
- Adapts to your evolving standards

### 4. Prioritized
Rules are categorized by impact:
- **P1 (CRITICAL)**: Runtime errors, data corruption
- **P2 (HIGH)**: Spec compliance, architecture
- **P3 (MEDIUM)**: Code quality, maintainability
- **P4 (LOW)**: Style, conventions

### 5. Traceable
Every change links back to:
- Source specification
- Specific task ID
- Line number in tasks.md
- Instruction file reference (if any)

## Use Cases

### 1. Legacy Code Modernization
Apply all modern patterns from recent specs to old files:
```
Target: Forms/Legacy/OldInventoryForm.cs
Result: Updated with 25 patterns from 5 specs
```

### 2. New Developer Onboarding
Automatically align new code with established patterns:
```
Target: Forms/NewFeature/NewDialog.cs
Result: Applied 15 patterns, preventing common mistakes
```

### 3. Pre-Deployment Compliance Check
Verify files meet all spec requirements before release:
```
Target: Controls/MainForm/Control_NewTab.cs
Result: 92% compliant, 3 remaining issues documented
```

### 4. Bulk Refactoring
Process multiple files in batch (run prompt repeatedly):
```
1. Forms/MainForm/MainForm.cs → 96% compliant
2. Controls/MainForm/Control_InventoryTab.cs → 94% compliant
3. Data/Dao_Inventory.cs → 98% compliant
```

## Limitations and Considerations

### When NOT to Use

1. **Brand new files**: May over-apply patterns where flexibility is needed
2. **Prototype code**: Wait until patterns stabilize
3. **Third-party integrations**: May conflict with external API requirements

### Manual Review Required

The prompt is thorough but not perfect. Always review:
- Changes to public API signatures
- Business logic alterations
- Performance-critical code sections

### Compilation Safety

The prompt:
- ✅ Verifies compilation after fixes
- ✅ Rolls back on compilation failure
- ✅ Applies fixes incrementally (method-by-method)

## Configuration

### Customize Priority Levels

Edit compliance-rules.md to adjust priority:
```markdown
### Rule FR-008: Service_ErrorHandler Adoption
**Priority**: HIGH → MEDIUM (if your project allows mixed error handling)
```

### Exclude Specific Rules

Add to `.github/config/spec-refactor-exclude.json`:
```json
{
  "excludeRules": ["FR-004", "T115"],
  "excludeFiles": ["Tests/**/*.cs", "Legacy/**/*.cs"]
}
```

### Custom Patterns

Add manual rules to `compliance-rules.md`:
```markdown
## Rule: CUSTOM-001

**Source Spec**: Manual Addition
**Priority**: HIGH

### Description
All public methods must have XML documentation

[Pattern details...]
```

## Maintenance

### When Specs Change

The prompt automatically detects:
- New specs added to `specs/` or `specs/Archives/`
- Tasks marked complete in existing specs
- Pattern adoption in codebase

**Cache invalidates** automatically after 24 hours.

### When Rules Conflict

If two specs define conflicting rules:
1. Newer spec takes precedence (by creation date)
2. More specific rule overrides general rule
3. Conflict documented in report for user decision

## Roadmap

### Future Enhancements

1. **AI Pattern Learning**: Use machine learning to discover implicit patterns
2. **Batch Processing**: Process entire directory trees
3. **Git Integration**: Commit each fix separately with proper messages
4. **CI/CD Integration**: Run as pre-commit hook or CI check
5. **Pattern Suggestions**: AI recommends new rules based on code evolution

## Comparison with Alternatives

### vs. Manual Code Review
- **Speed**: 100x faster (minutes vs. hours)
- **Consistency**: 100% consistent (no human oversight)
- **Coverage**: Exhaustive (checks ALL rules)

### vs. Static Analysis Tools
- **Context-Aware**: Understands your project's specific patterns
- **Spec-Driven**: Enforces requirements from actual specifications
- **Adaptive**: Evolves with your standards

### vs. database-compliance-reviewer.prompt.md
- **Broader**: Covers all pattern types, not just database
- **Dynamic**: Self-updating as specs are added
- **Validated**: Only applies proven-implemented patterns

## Support

### Troubleshooting

**Issue**: Cache is stale but not regenerating
- **Fix**: Delete `.github/cache/compliance-rules.md`

**Issue**: Rule applied incorrectly
- **Fix**: Edit `compliance-rules.md` to adjust pattern

**Issue**: Compilation fails after fixes
- **Fix**: Prompt should auto-rollback; check error logs

### Getting Help

Review the detailed execution logs in the change report for:
- Which rules were applied
- Why specific changes were made
- How to validate changes manually

---

**Created**: 2025-10-24
**Last Updated**: 2025-10-24
**Version**: 1.0
