# Test Checklist Refactor & Migration Questions

**Date**: 2025-10-19  
**Purpose**: Clarify how to refactor final-test-checklist.md and migrate incomplete tasks from other spec folders

---

## Task Migration Context

**Incomplete tasks found:**
- `002-003-database-layer-complete/tasks.md`: **19 incomplete** (Roslyn analyzer, manual testing, perf validation)
- `002-003-001-developer-tools-suite/tasks.md`: **41 incomplete** (Parameter Prefix UI, Schema Inspector)
- `002-comprehensive-database-layer/tasks.md`: **109 incomplete** (many likely outdated/superseded)

---

## Question 1: Primary Organization Principle

**Context**: Checklist currently mixes investigation notes, historical logs, and working tasks.

**Options:**
- **A) Hybrid Workflow Structure** ⭐ RECOMMENDED
  - Main: Active test categories + fixes
  - Middle: Quick references
  - End: Appendices (history, investigation, tools reference)

- **B) Pure Category Organization**
  - Group by failure type only (Skipped/QB/System/Helper)
  - All other content in appendices

- **C) Priority-Driven Structure**
  - Organize by urgency (Critical/High/Medium/Low), regardless of test type

**Your Answer: ____**

---

## Question 2: Which Incomplete Tasks Should Be Migrated?

**Options:**
- **A) Test-Related Only** ⭐ RECOMMENDED
  - Migrate ONLY tasks directly related to fixing the 23 failing tests
  - Ignore UI features, analyzers, documentation tasks

- **B) Current Branch Scope**
  - Test fixes + tasks that support testing (seed data, schema validation, integration harness)

- **C) All Relevant & Safe**
  - Everything relevant to database layer AND won't break current branch (includes Roslyn, docs, perf)

- **D) Nothing - Focus Only on Current 23 Tests**
  - Don't migrate anything. Keep checklist focused solely on fixing existing test failures

**Your Answer: ____**

---

## Question 3: How Should "DEFERRED" Tasks Be Handled?

**Context**: Found T113c, T113d marked "⏸️ DEFERRED - Complete T113-T704 first" (Developer role UI features)

**Options:**
- **A) Skip Deferred Tasks** ⭐ RECOMMENDED
  - Don't migrate anything marked DEFERRED. They're explicitly postponed.

- **B) Move to "Future Enhancements" Appendix**
  - Include deferred tasks in separate section for future reference

**Your Answer: ____**

---

## Question 4: MCP Tools Section Placement

**Context**: Full MCP tools list (28 tools) with descriptions currently at end of document

**Options:**
- **A) Split Reference** ⭐ RECOMMENDED
  - Top: Quick reference (tool names + 1-line descriptions)
  - Appendix: Full descriptions with examples

- **B) All at Top**
  - Move entire MCP tools section to top as reference material

- **C) All in Appendix**
  - Keep all MCP tools documentation at end

- **D) Separate MCP-TOOLS-GUIDE.md File**
  - Extract all MCP content to separate document, reference it in checklist

**Your Answer: ____**

---

## Question 5: Session Log & History Management

**Context**: Two session logs (Part 1, Part 2) embedded in document with detailed findings

**Options:**
- **A) Condensed Progress + Appendix History** ⭐ RECOMMENDED
  - Main doc: Brief "Progress Summary" section
  - Full session logs in Appendix B

- **B) Separate CHANGELOG.md**
  - Move all session history to new CHANGELOG.md file

- **C) Keep Inline**
  - Keep session logs where they are in main document flow

**Your Answer: ____**

---

## Question 6: Detailed Investigation Notes Section

**Context**: "Detailed Investigation Notes" has redundant SQL examples and manual test templates

**Options:**
- **A) Remove Redundancy, Keep Templates** ⭐ RECOMMENDED
  - Delete redundant explanations (already in categories)
  - Keep SQL/test templates in "Appendix C: Testing Templates"

- **B) Remove Entirely**
  - Delete the entire section - info is duplicated in category sections

- **C) Convert to Quick Reference**
  - Keep only essential commands/queries in condensed "Quick Reference" section

**Your Answer: ____**

---

## Question 7: Commands Reference Placement

**Context**: PowerShell commands section currently near bottom of document

**Options:**
- **A) Top Quick Start Section** ⭐ RECOMMENDED
  - Move essential commands to top "Quick Start" section
  - Full command reference in appendix

- **B) Keep Near Bottom**
  - Leave commands where they are

- **C) Embed in Relevant Sections**
  - Put test commands in test sections, DB commands in DB sections, etc.

**Your Answer: ____**

---

## Question 8: How to Handle Potentially Superseded Tasks?

**Context**: 002-comprehensive-database-layer has 109 incomplete tasks, many may be outdated/completed in later specs

**Options:**
- **A) Manual Review with Context** ⭐ RECOMMENDED
  - Show me each task with context, I'll decide keep/discard
  - Focus on testing/database tasks only

- **B) Skip 002-comprehensive Entirely**
  - Assume 002-comprehensive tasks are superseded by 002-003
  - Only migrate from 002-003 folders

- **C) AI Filtering**
  - Use Joyride to filter by keywords (test, integration, DAO, database)
  - Migrate only matching tasks automatically

**Your Answer: ____**

---

## Quick Answer Format

Just respond with your answers like this:

```
Q1: A
Q2: A
Q3: A
Q4: A
Q5: A
Q6: A
Q7: A
Q8: A
```

Or say **"proceed with recommendations"** if you agree with all the ⭐ RECOMMENDED options (all A's).

---

## Recommended Structure Preview

If you choose all "A" options, the refactored checklist will look like:

```
# Final Test Checklist

## 1. Quick Start
- Essential commands
- How to run tests
- How to use MCP tools

## 2. Status Dashboard
- Current: 113/136 passing
- Failures by category
- Next priorities

## 3. Test Failure Categories
### Category 1: Skipped Tests (✅ COMPLETE)
### Category 2: Quick Button Failures (🔴 10 tests)
### Category 3: System DAO Failures (🟡 6 tests)
### Category 4: Helper & Validation (🟢 5 tests)
### Category 5: New Failures from Phase 1 (2 tests)

## 4. MCP Tools Quick Reference
- Tool names + one-line descriptions

## 5. Progress Summary
- Phases completed
- Current phase status
- Metrics

## Appendices
### Appendix A: Session History
### Appendix B: Testing Templates
### Appendix C: Full MCP Tools Reference
### Appendix D: Commands Reference
```
