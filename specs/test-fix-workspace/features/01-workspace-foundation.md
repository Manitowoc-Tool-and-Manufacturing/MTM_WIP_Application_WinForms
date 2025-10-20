# Feature 1: Test Fix Workspace Foundation

**Created**: 2025-10-19  
**Purpose**: Establish core workspace structure, navigation, and collaboration protocols

---

## Feature Overview

Create the foundational structure for the Test Fix Workspace that provides clear navigation, establishes file organization patterns, and implements a clarification protocol for AI-human collaboration. This foundation enables all other workspace features to build upon a consistent, maintainable structure.

---

## Current Situation

**Problem**: Currently working with monolithic checklist files where finding information requires reading 1000+ lines, progress updates touch the same file repeatedly, and historical context is mixed with active work.

**Need**: A modular, folder-based organization where each piece of information has a single home, navigation is intuitive, and developers can find what they need in under 1 minute.

---

## User Needs

### Primary Users

**Developers fixing tests**: Need to quickly navigate to relevant information without searching through large files. Want clear folder structure that separates active work from reference materials.

**AI Agents (GitHub Copilot)**: Need clear guidance on when to ask clarifying questions and how to format them so humans can answer quickly without confusion.

**Project Managers/Tech Leads**: Need to understand workspace organization at a glance and know where to find status information.

---

## What Users Need to Accomplish

### For Developers

1. **Navigate workspace in under 30 seconds**: Find any category, reference, or tool from TOC
2. **Understand workspace purpose immediately**: Know why folders are organized this way
3. **Access quick start commands**: Get essential test commands without searching
4. **See current status at top**: Know test count and next priority without opening dashboard

### For AI Agents

1. **Know when to ask questions**: Clear triggers for when clarification is needed vs making reasonable assumptions
2. **Format questions consistently**: Use standard template that's easy for humans to parse
3. **Provide context efficiently**: Explain why the question matters in non-technical terms
4. **Offer clear options**: Present 2-4 choices with plain language explanations

---

## Success Outcomes

### For Navigation

- Developers find any workspace file in under 30 seconds
- TOC provides complete map of workspace in under 200 lines
- Current status visible at top of TOC (no need to open dashboard)
- Quick start commands copy-pasteable directly from TOC

### For Collaboration

- AI clarification questions answered in under 2 minutes
- Zero back-and-forth due to unclear question formatting
- Humans can choose option by single letter (A/B/C) or provide custom answer
- Context provided in plain language without technical jargon

### For Maintainability

- New developers understand workspace in under 5 minutes
- Folder structure is self-documenting (names explain purpose)
- Adding new categories or tools doesn't require restructuring
- README-style documentation stays under 250 lines

---

## Key Components

### Folder Structure

```
test-fix-workspace/
├── TOC.md                          # Main entry point
├── DASHBOARD.md                    # Status metrics (separate feature)
├── categories/                     # Test category files (separate feature)
├── reference/                      # Reference materials (separate feature)
├── tools/                          # Automation scripts (separate feature)
└── history/                        # Session logs (separate feature)
```

### TOC.md Requirements

Must include these sections:

1. **Quick Status** (top of file):
   - Current test count: X/136 passing (Y%)
   - Next priority: Which category to work on
   - Last updated: Timestamp

2. **Quick Start Commands**:
   ```powershell
   # Run all tests
   dotnet test
   
   # Run specific category
   dotnet test --filter "FullyQualifiedName~Dao_QuickButtons"
   
   # Update progress after fixing tests
   .\tools\update-progress.ps1
   ```

3. **Workspace Navigation**:
   - Link to each category file with one-line description
   - Link to reference materials
   - Link to tools with safety notes
   - Link to history/changelog

4. **Clarification Protocol**:
   - When AI should ask questions (specific triggers)
   - Question format template
   - How humans should respond
   - Examples of good vs bad questions

5. **Workspace Purpose**:
   - Why this structure exists
   - How it improves on monolithic approach
   - When to use each folder
   - How to add new content

### Clarification Protocol Template

```markdown
## When I Need Clarification

If I encounter ambiguity while working on fixes, I will STOP and ask you a question in this format:

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

### When I Should Ask vs Assume

**I WILL ask when**:
- Fix strategy has multiple approaches with different trade-offs
- Test data setup could be done in stored procedure OR C# helper
- Scope decision affects other categories (e.g., migrate tasks or skip)

**I will NOT ask when**:
- Reasonable industry standard exists (follow .NET testing conventions)
- Documentation clearly states preference (follow BaseIntegrationTest patterns)
- Choice has no significant impact (file naming, comment style)
```

---

## Workspace Organization Rules

### File Placement Guidelines

- **TOC.md**: Only navigation, status, and protocols. No detailed content.
- **categories/**: Test failure tracking only. No reference material.
- **reference/**: Read-only documentation. No progress tracking.
- **tools/**: Executable scripts only. No documentation.
- **history/**: Historical logs only. No active work.

### File Size Limits

- TOC.md: Under 300 lines
- Category files: Under 300 lines each
- Reference files: Under 500 lines each
- Tool scripts: Under 200 lines with comments

### Linking Rules

- Use relative links between workspace files
- Absolute paths only for C# code files
- Link to sections using anchors: `[Section](#section-name)`
- External links must include context (what they point to)

---

## Special Requirements

### Plain Language Requirement

All documentation must:
- Avoid technical jargon in user-facing sections
- Explain acronyms on first use (e.g., DAO = Data Access Object)
- Use concrete examples instead of abstract descriptions
- Write for someone unfamiliar with the codebase

### Accessibility

- Use proper heading hierarchy (no skipping levels)
- Include alt text for any diagrams
- Code blocks must specify language for syntax highlighting
- Lists use consistent formatting (all bullets OR all numbers)

### Versioning

- TOC includes "Last updated" timestamp
- Major structural changes noted in history/CHANGELOG.md
- Version number not needed (git provides history)

---

## Out of Scope

This feature does NOT include:
- Creating category files (separate feature)
- Writing reference documentation (separate feature)
- Building automation tools (separate feature)
- Organizing historical logs (separate feature)
- Dashboard implementation (separate feature)

Only the **foundation structure** and **navigation hub** (TOC.md).

---

## Assumptions

- Workspace lives in `specs/test-fix-workspace/` folder
- Git branch is `002-003-database-layer-complete`
- Markdown files will be read in VS Code or GitHub
- Developers have basic familiarity with markdown syntax
- Folder structure won't need to change frequently

---

## Dependencies

- None (this is the foundation other features depend on)

---

## Acceptance Criteria

### Structure
- [ ] All 5 folders created (categories, reference, tools, history, root)
- [ ] TOC.md exists in root
- [ ] Folder names are self-documenting
- [ ] README-style guidance in TOC

### TOC Content
- [ ] Quick status section at top (test count, next priority)
- [ ] Quick start commands copy-pasteable
- [ ] Complete navigation with one-line descriptions
- [ ] Clarification protocol with clear examples
- [ ] Workspace purpose explained in under 100 words

### Clarification Protocol
- [ ] Question format template included
- [ ] "When to ask vs assume" guidance clear
- [ ] Example questions provided
- [ ] Response format specified (A/B/C/Custom)
- [ ] Suggested answer with reasoning

### Usability
- [ ] Developer can find any file from TOC in under 30 seconds
- [ ] Status visible without opening other files
- [ ] Commands are copy-paste ready
- [ ] Plain language used throughout (no unnecessary jargon)

### Maintainability
- [ ] TOC under 300 lines
- [ ] Folder structure allows new content without restructuring
- [ ] Linking rules documented
- [ ] File size limits specified

---

## Success Metrics

**Navigation Speed**:
- Time to find category file: < 30 seconds (baseline: 2+ minutes)
- Time to understand workspace: < 5 minutes (baseline: 15+ minutes)

**Collaboration Efficiency**:
- Clarification questions answered: < 2 minutes (baseline: 5+ minutes back-and-forth)
- AI questions formatted correctly: 100% (baseline: ~60%)

**Workspace Quality**:
- TOC lines: < 300 (manageable size)
- Dead links: 0 (full navigation works)
- Files without clear purpose: 0 (everything self-documenting)

---

## Notes for /speckit.specify

This feature is focused solely on the **foundational structure and navigation**. It intentionally excludes:
- Content creation (categories, reference docs, tools)
- Actual test fixing work
- Data migration from existing files

This allows the foundation to be specified and implemented first, with other features building on top of it.

No clarifications should be needed - structure and requirements are concrete and specific.
