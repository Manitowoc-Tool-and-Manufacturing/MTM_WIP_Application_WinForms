---
description: Generate implementation specifications for Settings System Refactor with clarification question validation
---

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt handles Settings System implementation spec generation. To maximize the value of each premium request:

- **Validate clarifications FIRST** before any generation
- **Complete ALL generation steps** in a single session
- **Generate all missing documents** (plan, tasks, checklists) in one pass
- **Run all validation checks** without stopping between steps
- **Only stop for user input** when unanswered clarification questions block progress

**Do NOT start generation if clarifications are unanswered** - stop immediately and present questions.

---

## Available MCP Tools

This prompt has access to MCP tools from the **mtm-workflow** server:

### Specification Analysis Tools

**analyze_spec_context** - Extract implementation context from specification directory
- **Input:** `feature_dir` = `"c:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/specs/settings-system-refactor"`
- **Output:** Available docs, tech stack, entities, contracts, missing documentation
- **Use:** Beginning generation to understand existing spec structure

**check_checklists** - Validate checklist completion status
- **Input:** `checklist_dir` (absolute path to checklists directory when created)
- **Output:** Total/completed/incomplete counts, overall PASS/FAIL status
- **Use:** After generating checklists to verify quality gates

### Settings System Specific Tools

**validate_dao_patterns** - Check DAO compliance with MTM patterns
**validate_error_handling** - Verify error handling correctness
**check_security** - Scan for security vulnerabilities
**check_xml_docs** - Verify documentation coverage
**validate_ui_scaling** - Check DPI scaling compliance
**generate_ui_fix_plan** - Create UI remediation plans

---

## User Input

```text
$ARGUMENTS
```

User input provides implementation phase or request type. If empty, default to "Generate complete implementation specification".

---

## Execution Flow

### Phase 1: Pre-Generation Validation (CRITICAL - DO NOT SKIP)

**STOP IMMEDIATELY if this phase fails**

1. **Analyze Existing Spec Structure**:
   ```typescript
   analyze_spec_context(
     feature_dir: "c:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/specs/settings-system-refactor"
   )
   ```

2. **Verify Required Documents Exist**:
   - ✅ **MUST EXIST**: `1-Specification.md` (feature specification)
   - ✅ **MUST EXIST**: `2-Clarification-Questions.md` (decision points)
   - ✅ **MUST EXIST**: `5-Implementation-Roadmap.md` (phased delivery plan)
   - ✅ **MUST EXIST**: `Ref-Architectural-Analysis.md` (technical analysis)
   - ✅ **MUST EXIST**: `Ref-Research-Data/settings-entities.json` (entity mapping)
   - ✅ **MUST EXIST**: `Ref-Research-Data/settings-research.json` (system analysis)

3. **Load and Parse Clarification Questions**:
   ```typescript
   read_file("c:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/specs/settings-system-refactor/2-Clarification-Questions.md")
   ```

4. **Extract Unanswered Questions**:
   - Search for questions with `**Status**: Awaiting User Input` or similar markers
   - Identify questions with placeholder answers like `[TO BE DETERMINED]`, `[NEEDS DECISION]`
   - Look for sections marked as `**Clarification Needed**` without resolution

5. **Critical Decision Gate**:
   
   **IF unanswered questions exist**:
   - **STOP GENERATION IMMEDIATELY**
   - Present unanswered questions to user in this format:
   
   ```markdown
   ## ⚠️ CLARIFICATION QUESTIONS MUST BE ANSWERED BEFORE PROCEEDING
   
   The following questions from `2-Clarification-Questions.md` require answers before implementation specification can be generated:
   
   ---
   
   ### Question [ID]: [Category] - [Title]
   
   **Context**: [Brief context from clarification doc]
   
   **What we need to know**: [Specific question text]
   
   **Options to Consider**:
   
   | Option | Description | Implications |
   |--------|-------------|--------------|
   | A | [Option A description] | [Impact of choosing A] |
   | B | [Option B description] | [Impact of choosing B] |
   | C | [Option C description] | [Impact of choosing C] |
   | Custom | Your own answer | Provide specific details |
   
   **Impact**: [Priority level from doc - High/Medium/Low]
   
   **Your answer**: _[Awaiting response]_
   
   ---
   
   [Repeat for each unanswered question]
   
   ---
   
   ## How to Respond
   
   Please provide answers in this format:
   ```
   Q[ID]: [Option letter or "Custom: your answer"]
   ```
   
   Example:
   ```
   Q1.1: Option A
   Q1.2: Custom: Use hybrid approach with database for critical settings, files for preferences
   Q2.1: Option B
   ```
   
   **After providing answers**, I will update `2-Clarification-Questions.md` and proceed with implementation specification generation.
   ```
   
   - **WAIT for user to provide all answers**
   - **DO NOT PROCEED** until all questions are answered
   
   **IF all questions are answered**:
   - Continue to Phase 2

---

### Phase 2: Document Generation

**Prerequisites**: All clarification questions answered, existing specs validated

1. **Determine Missing Documents**:
   - Check which implementation documents don't exist yet:
     - `3-Plan.md` (detailed implementation plan)
     - `4-Implementation-Guide.md` (developer handbook)
     - `6-Validation-Testing.md` (QA strategy)
     - `7-Checklist.md` (quality gates)
     - `8-Tasks.md` (actionable task breakdown)

2. **Generate Plan Document** (`3-Plan.md` if missing):
   
   **Structure**:
   ```markdown
   # Settings System Refactor - Implementation Plan
   
   **Created**: [DATE]
   **Status**: Ready for Implementation
   **Based on**: 1-Specification.md, 5-Implementation-Roadmap.md, 2-Clarification-Questions.md (ANSWERED)
   
   ## Executive Summary
   
   [Synthesize from existing specs, roadmap, and answered clarifications]
   
   ## Implementation Phases (From 5-Implementation-Roadmap.md)
   
   ### Phase 1: Foundation & Database Migration
   [Copy from roadmap, add specific decisions from clarification answers]
   
   ### Phase 2: Core Infrastructure
   [Copy from roadmap, add specific decisions from clarification answers]
   
   ### Phase 3: UI Modernization
   [Copy from roadmap, add specific decisions from clarification answers]
   
   ### Phase 4: Advanced Features
   [Copy from roadmap, add specific decisions from clarification answers]
   
   ### Phase 5: Integration & Polish
   [Copy from roadmap, add specific decisions from clarification answers]
   
   ## Clarification Resolutions
   
   [For each answered clarification question]:
   - **Q[ID]**: [Question summary]
   - **Decision**: [Chosen option/answer]
   - **Implementation Impact**: [How this affects the plan]
   - **Affected Phases**: [Which phases are impacted]
   
   ## Dependency Matrix
   
   | Task | Depends On | Blocks | Phase |
   |------|-----------|--------|-------|
   [Generate from roadmap and clarification impacts]
   
   ## Risk Register
   
   | Risk | Probability | Impact | Mitigation | Owner |
   |------|------------|--------|------------|-------|
   [From roadmap + new risks from clarification answers]
   
   ## Decision Log
   
   | Date | Decision | Rationale | Impact |
   |------|----------|-----------|--------|
   [Record all clarification question answers as decisions]
   ```

3. **Generate Implementation Guide** (`4-Implementation-Guide.md` if missing):
   
   **Structure**:
   ```markdown
   # Settings System Refactor - Implementation Guide
   
   **Created**: [DATE]
   **Audience**: Developers, QA Engineers
   **Prerequisites**: 1-Specification.md, 3-Plan.md, 5-Implementation-Roadmap.md
   
   ## Quick Start
   
   ### Before You Begin
   - [ ] Read 1-Specification.md (feature overview)
   - [ ] Review 3-Plan.md (implementation phases)
   - [ ] Check 7-Checklist.md (quality gates)
   - [ ] Understand clarification decisions in 3-Plan.md
   
   ### Development Environment Setup
   [Based on .github/instructions/csharp-dotnet8.instructions.md patterns]
   
   ## Phase-by-Phase Implementation
   
   ### Phase 1: Foundation & Database Migration
   
   #### Database Schema Updates
   [Specific SQL scripts and migration patterns]
   
   #### DAO Layer Updates
   [Code patterns from .github/instructions/mysql-database.instructions.md]
   
   #### Testing Requirements
   [Manual validation checklist from .github/instructions/testing-standards.instructions.md]
   
   [Repeat for all 5 phases]
   
   ## Code Patterns and Examples
   
   ### Settings Persistence (Based on Q1.1 Answer)
   [Specific code examples based on clarification answer]
   
   ### Theme System Integration (Based on Q2.x Answers)
   [Specific code examples based on clarification answers]
   
   ### Quick Button Management (Based on Q3.x Answers)
   [Specific code examples based on clarification answers]
   
   ## Validation Workflows
   
   ### MCP Tool Usage
   - `validate_dao_patterns` - After DAO updates
   - `validate_ui_scaling` - After UI changes
   - `check_security` - Before each phase completion
   - `check_xml_docs` - Before code review
   
   ## Troubleshooting
   
   [Common issues and resolutions]
   ```

4. **Generate Validation & Testing Document** (`6-Validation-Testing.md` if missing):
   
   **Structure**:
   ```markdown
   # Settings System Refactor - Validation & Testing Strategy
   
   **Created**: [DATE]
   **Status**: Ready for QA
   **Test Approach**: Manual validation with MCP tool support
   
   ## Test Categories
   
   ### Phase 1: Database Migration Testing
   [Test scenarios based on clarification answers for persistence strategy]
   
   ### Phase 2: Core Infrastructure Testing
   [Test scenarios for settings management, validation, caching]
   
   ### Phase 3: UI Testing
   [Test scenarios based on UI clarification answers]
   
   ### Phase 4: Advanced Features Testing
   [Test scenarios for import/export, search, theme customization]
   
   ### Phase 5: Integration Testing
   [End-to-end testing across all features]
   
   ## Test Scenarios
   
   [For each phase, generate specific test cases]:
   
   ### Scenario [ID]: [Name]
   **Phase**: [Phase number]
   **Priority**: [High/Medium/Low]
   **Prerequisites**: [Setup requirements]
   
   **Steps**:
   1. [Step with expected result]
   2. [Step with expected result]
   
   **Validation**:
   - [ ] [Specific check]
   - [ ] [Specific check]
   
   **MCP Tools**:
   - Run `[tool_name]` to validate [aspect]
   
   **Success Criteria**: [From 1-Specification.md]
   
   ## Regression Testing
   
   [Critical paths that must continue working]
   
   ## Performance Testing
   
   [Benchmarks from 5-Implementation-Roadmap.md]
   ```

5. **Generate Checklist Document** (`7-Checklist.md` if missing):
   
   **Structure**:
   ```markdown
   # Settings System Refactor - Quality Checklist
   
   **Created**: [DATE]
   **Purpose**: Quality gates for implementation readiness
   
   ## Pre-Implementation Checklist
   
   - [ ] All clarification questions answered (2-Clarification-Questions.md)
   - [ ] Implementation plan reviewed (3-Plan.md)
   - [ ] Team resources allocated (5-Implementation-Roadmap.md)
   - [ ] Database backup procedures tested
   - [ ] Rollback plan documented
   
   ## Phase 1: Foundation & Database Migration
   
   ### Database Schema
   - [ ] Migration scripts created with rollback
   - [ ] Audit tables created
   - [ ] Triggers implemented
   - [ ] Performance benchmarked
   - [ ] Data integrity validated
   
   ### Quality Gates
   - [ ] Zero data loss verified
   - [ ] Audit trail functional
   - [ ] Rollback tested successfully
   
   [Repeat for all 5 phases with specific checklist items]
   
   ## Cross-Phase Quality Requirements
   
   ### Code Quality
   - [ ] Follows .github/instructions/csharp-dotnet8.instructions.md
   - [ ] Passes `validate_dao_patterns` checks
   - [ ] Passes `validate_error_handling` checks
   - [ ] Passes `check_security` scans
   - [ ] XML documentation at 95%+ coverage (`check_xml_docs`)
   
   ### UI Quality
   - [ ] Passes `validate_ui_scaling` checks
   - [ ] Theme integration complete
   - [ ] DPI scaling verified (100%, 125%, 150%, 200%)
   - [ ] Keyboard navigation functional
   
   ### Testing Quality
   - [ ] All test scenarios pass (6-Validation-Testing.md)
   - [ ] Regression tests pass
   - [ ] Performance benchmarks met
   - [ ] User acceptance testing complete
   
   ## Documentation Checklist
   
   - [ ] README.md updated with new structure
   - [ ] User guide updated
   - [ ] Admin documentation complete
   - [ ] Migration guide created
   - [ ] API documentation complete
   
   ## Deployment Readiness
   
   - [ ] Staged rollout plan approved
   - [ ] Monitoring configured
   - [ ] Alerting rules defined
   - [ ] Training materials prepared
   - [ ] Support resources available
   ```

6. **Generate Tasks Document** (`8-Tasks.md` if missing):
   
   **Structure**:
   ```markdown
   # Settings System Refactor - Task Breakdown
   
   **Created**: [DATE]
   **Status**: Ready for Assignment
   **Total Tasks**: [COUNT]
   **Estimated Duration**: 14-19 weeks (from 5-Implementation-Roadmap.md)
   
   ## Task Format
   
   Each task includes:
   - Unique ID (T###)
   - Phase assignment
   - Dependencies
   - Instruction file references
   - Estimated effort
   - MCP validation tools
   
   ## Phase 1: Foundation & Database Migration (2-3 weeks)
   
   ### T101: Create Database Audit Tables
   **Phase**: 1  
   **Dependencies**: None  
   **Priority**: CRITICAL  
   **Effort**: 1-2 days  
   **Reference**: .github/instructions/mysql-database.instructions.md
   
   **Description**:
   Create `app_settings_history` table with audit columns based on clarification Q1.1 answer.
   
   **Acceptance Criteria**:
   - [ ] Table schema matches specification
   - [ ] Triggers functional
   - [ ] Performance impact <50ms
   
   **MCP Validation**:
   - Run `validate_schema` after creation
   
   ---
   
   [Generate 50-100 tasks across all 5 phases]
   
   ## Task Dependencies
   
   ```mermaid
   graph TD
     T101[Database Audit Tables] --> T102[Migration Scripts]
     T102 --> T103[Data Migration]
     T103 --> T201[Service Layer]
     [Continue dependency graph]
   ```
   
   ## Phase Completion Gates
   
   ### Phase 1 Complete When:
   - [ ] Tasks T101-T120 complete
   - [ ] Phase 1 checklist passes (7-Checklist.md)
   - [ ] Phase 1 tests pass (6-Validation-Testing.md)
   
   [Repeat for all phases]
   ```

---

### Phase 3: Post-Generation Validation

1. **Update Clarification Questions Document**:
   - Mark all answered questions with `**Status**: ✅ ANSWERED - [DATE]`
   - Add answers inline with `**Decision**: [Answer]`
   - Add implementation impact notes

2. **Run MCP Checklist Validation**:
   ```typescript
   check_checklists(
     checklist_dir: "c:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/specs/settings-system-refactor"
   )
   ```

3. **Update README.md**:
   - Update document index with newly created files
   - Update "Next Steps" section
   - Mark implementation-ready status

4. **Generate Completion Report**:
   ```markdown
   ## Settings System Specification - Generation Complete
   
   **Date**: [DATE]
   **Status**: ✅ READY FOR IMPLEMENTATION
   
   ### Generated Documents
   - ✅ 3-Plan.md (implementation plan with clarification resolutions)
   - ✅ 4-Implementation-Guide.md (developer handbook)
   - ✅ 6-Validation-Testing.md (QA strategy)
   - ✅ 7-Checklist.md (quality gates)
   - ✅ 8-Tasks.md (task breakdown)
   
   ### Clarification Resolutions
   [List all answered questions and decisions]
   
   ### Next Steps
   1. Review 3-Plan.md for clarification impacts
   2. Assign tasks from 8-Tasks.md
   3. Begin Phase 1 implementation
   4. Use 7-Checklist.md for quality gates
   5. Follow 4-Implementation-Guide.md patterns
   
   ### MCP Tools Available
   - `validate_dao_patterns` - DAO compliance
   - `validate_ui_scaling` - UI/DPI validation
   - `check_security` - Security scanning
   - `check_xml_docs` - Documentation coverage
   - `check_checklists` - Quality gate validation
   ```

---

## Guidelines

### Clarification Answer Processing

When user provides answers:

1. **Parse Answers**:
   - Format: `Q[ID]: [Option/Custom answer]`
   - Validate all required questions answered
   - Handle custom answers with full details

2. **Update Clarification Document**:
   - Add `**Status**: ✅ ANSWERED - [DATE]`
   - Add `**Decision**: [User's answer]`
   - Add `**Rationale**: [If provided]`
   - Add `**Implementation Impact**: [How this affects plan]`

3. **Propagate Decisions**:
   - Reflect in 3-Plan.md (decision log, phase details)
   - Reflect in 4-Implementation-Guide.md (code patterns)
   - Reflect in 6-Validation-Testing.md (test scenarios)
   - Reflect in 7-Checklist.md (quality gates)
   - Reflect in 8-Tasks.md (task descriptions)

### Code Pattern Examples

Base patterns on:
- `.github/instructions/csharp-dotnet8.instructions.md`
- `.github/instructions/mysql-database.instructions.md`
- `.github/instructions/ui-compliance/theming-compliance.instructions.md`
- `.github/instructions/testing-standards.instructions.md`

Customize patterns based on clarification answers.

### MCP Tool Integration

Include MCP validation tools in:
- Task definitions (`8-Tasks.md`)
- Implementation guide (`4-Implementation-Guide.md`)
- Testing strategy (`6-Validation-Testing.md`)
- Quality checklists (`7-Checklist.md`)

### Document Cross-References

Ensure all documents reference each other appropriately:
- Plan → Roadmap, Specification, Clarifications
- Guide → Plan, Instruction files, MCP tools
- Testing → Specification success criteria, Plan phases
- Checklist → All other documents
- Tasks → Plan phases, Instruction files, Checklist items

---

## Error Handling

### If Clarification Questions Not Answered
- **STOP immediately**
- Present questions in user-friendly format
- Wait for responses
- Do NOT proceed with generation

### If Required Documents Missing
- List missing documents
- Explain why they're needed
- Provide guidance on creating them
- Do NOT proceed without core specs

### If MCP Tools Unavailable
- Note which validations cannot be automated
- Provide manual validation alternatives
- Continue generation with warnings

---

## Success Criteria

Generation is successful when:

- [ ] All clarification questions answered
- [ ] All missing implementation documents created
- [ ] All documents cross-reference correctly
- [ ] MCP tool integration complete
- [ ] Clarification decisions propagated throughout
- [ ] Quality checklists comprehensive
- [ ] Task breakdown actionable
- [ ] Implementation guide developer-ready
- [ ] Testing strategy complete

---

**Final Output**: Complete implementation specification package ready for Phase 1 execution.
