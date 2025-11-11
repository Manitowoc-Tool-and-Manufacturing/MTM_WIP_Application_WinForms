# BMAD Quick Start Guide

**Time to Complete:** 5 minutes  
**What You'll Learn:** Basic BMAD agent commands, when to use which agent, how to activate agents in VS Code

---

## Prerequisites

- [ ] VS Code open in MTM project root
- [ ] BMAD installed (\.bmad-core/\ folder exists)
- [ ] GitHub Copilot extension installed (optional but recommended)

---

## What is BMAD?

BMAD provides **specialized AI agents** for different development roles. Each agent has deep knowledge of its domain and access to specific tools and templates.

**Think of agents as expert consultants** you bring in for specific tasks.

---

## Core Agents Reference

| Agent | Role | When To Use | Example Command |
|-------|------|-------------|-----------------|
| **@pm** | Product Manager | Create PRDs, define user stories | \@pm Create a PRD for inventory export\ |
| **@architect** | System Architect | Design architecture, select tech | \@architect Design the print system\ |
| **@dev** | Developer | Write code, implement features | \@dev Implement this spec\ |
| **@qa** | QA Engineer | Review quality, test strategies | \@qa *review {story}\ |
| **@sm** | Scrum Master | Create dev stories from epics | \@sm Create next story\ |
| **@po** | Product Owner | Validate docs alignment | \@po Run master checklist\ |

---

## Quick Start Workflow

### Step 1: Activate an Agent
- [ ] In VS Code chat, type \@\ to see available agents
- [ ] Select the agent you need (e.g., \@dev\)
- [ ] Ask your question or give a command

```
@dev What files should I look at for the transaction viewer?
```

**Why**: Agents load specialized context relevant to their role

---

### Step 2: Use Agent Commands

Each agent has special commands (prefixed with \*\):

```
@dev *help
```

**Common Commands:**
- \*help\ - Show all available commands for this agent
- \*create-doc {template}\ - Generate document from template
- \*shard-doc {file}\ - Split large docs into manageable pieces
- \@qa *risk {story}\ - Assess implementation risks
- \@qa *review {story}\ - Comprehensive quality review

---

### Step 3: Provide Context

Agents work best with clear context. Show them:

**Good Examples:**
```
@dev Review specs/006-print-and-export/spec.md and implement the next incomplete task from tasks.md

@architect I need to add Excel export to the Transaction Viewer. Review Documentation/BROWNFIELD_ARCHITECTURE.md and recommend an approach.

@qa *risk specs/006-print-and-export/tasks.md - Focus on the export functionality
```

**Why**: Specific file paths and context help agents give accurate answers

---

### Step 4: Iterative Refinement

Agents can refine their output:

```
@dev Can you make this more efficient?
@dev Add error handling to this code
@qa Are there any security concerns?
```

**Why**: Agents maintain conversation context and improve iteratively

---

## Agent Selection Guide

### "Which agent should I use?"

**Planning/Design Phase:**
- Starting from idea? → **@pm** (creates PRD)
- Have PRD, need architecture? → **@architect**
- Need to validate alignment? → **@po**

**Development Phase:**
- Writing code? → **@dev**
- Creating dev stories from epic? → **@sm**
- Need code review? → **@qa**

**Quality Phase:**
- Risk assessment? → **@qa *risk**
- Test strategy? → **@qa *design**
- Final review? → **@qa *review**

---

## Common Workflows

### **Workflow 1: Quick Feature Implementation**

```
Step 1: @dev Review specs/[feature]/spec.md
Step 2: @dev Implement the next incomplete task from tasks.md
Step 3: @qa *review specs/[feature]/spec.md
Step 4: @dev Address QA feedback
```

### **Workflow 2: Refactoring Code**

```
Step 1: @dev Generate pre-refactor report for Forms/SomeForm.cs
Step 2: Review report, approve changes
Step 3: @dev Apply refactoring with atomic commits
Step 4: @qa *review the refactored code
```

### **Workflow 3: Create Architecture**

```
Step 1: @pm Create PRD for [feature idea]
Step 2: @architect *create-doc architecture-tmpl.yaml
Step 3: @po Validate PRD and architecture alignment
Step 4: @sm Create dev stories from architecture
```

---

## BMAD + Constitution Integration

**Critical**: BMAD agents enforce MTM project constitution rules automatically.

**If agent refuses a request:**
```
@dev Why can't I use inline SQL here?
```

**Agent will explain:**
> "Constitution Principle I requires all database access via stored procedures. This prevents SQL injection and centralizes business logic. Use Helper_Database_StoredProcedure instead."

**Learn from rejections** - they teach you MTM architectural patterns!

---

## Special Agent Features

### **@dev** - Developer Agent

**File Operations:**
- Can read, create, edit files directly
- Follows MTM naming conventions automatically
- Applies theme integration (Core_Themes)
- Enforces region organization

**Best Practices:**
- Give specific file paths: \Forms/MainForm/MainForm.cs\
- Reference instruction files: \Follow .github/instructions/csharp-dotnet8.instructions.md\
- Ask for validation: \Does this follow MTM patterns?\

---

### **@qa** - Quality Assurance Agent

**Special Commands:**
- \*risk {story}\ - Risk assessment before development
- \*design {story}\ - Test strategy creation
- \*trace {story}\ - Requirements traceability
- \*nfr {story}\ - Non-functional requirements check
- \*review {story}\ - Comprehensive review + quality gate
- \*gate {story}\ - Update quality gate status

**Output Locations:**
- Risk profiles: \docs/qa/assessments/{epic}.{story}-risk-{date}.md\
- Test designs: \docs/qa/assessments/{epic}.{story}-test-design-{date}.md\
- Quality gates: \docs/qa/gates/{epic}.{story}-{slug}.yml\

---

### **@architect** - System Architect

**Specializations:**
- Technology selection
- System design
- API design
- Infrastructure planning
- Brownfield assessment

**Templates:**
- \*create-doc architecture-tmpl.yaml\ - Full architecture
- \*create-doc brownfield-architecture-tmpl.yaml\ - Existing system analysis
- \*create-doc front-end-architecture-tmpl.yaml\ - UI architecture

---

## Pro Tips

### ✅ **DO:**
- Start conversations with clear goals
- Provide file paths when relevant
- Ask agents to explain their reasoning
- Use \*help\ to discover agent capabilities
- Reference existing specs/architecture docs

### ❌ **DON'T:**
- Switch agents mid-task (maintain context)
- Expect agents to know about uncommitted changes
- Ask agents to violate constitution rules
- Give vague requests like "make it better"

---

## Quick Troubleshooting

**Problem:** Agent doesn't have context about my code  
**Solution:** Explicitly reference files: \@dev Review Forms/MainForm.cs and...\

**Problem:** Agent suggests non-MTM patterns  
**Solution:** Remind it: \@dev Follow MTM constitution - use stored procedures only\

**Problem:** Not sure which agent to use  
**Solution:** Ask any agent: \@dev Which agent should handle PRD creation?\

**Problem:** Agent's suggestion violates patterns  
**Solution:** Challenge it: \@dev Doesn't this violate the DAO pattern?\

---

## Next Steps

**Ready to try BMAD?** Choose your path:

- [ ] **New feature** → [Create Feature Pure BMAD](./02-Create-Feature-Pure-BMAD.md)
- [ ] **Refactoring** → [Refactor With BMAD](./03-Refactor-With-BMAD.md)
- [ ] **Command reference** → [Agent Command Reference](./04-Agent-Command-Reference.md)
- [ ] **Use with Speckit** → [Create New Spec With Speckit](../BMAD-Speckit/01-Create-New-Spec-With-Speckit.md)

---

## Key Takeaways

✅ BMAD provides specialized agents for different development roles  
✅ Use \@agent\ syntax to activate agents in VS Code  
✅ Agents enforce MTM constitution rules automatically  
✅ Commands prefixed with \*\ perform specialized tasks  
✅ Provide clear context (file paths, specs) for best results  
✅ Agents maintain conversation context for iterative refinement

---

**Last Updated:** November 11, 2025  
**Estimated Read Time:** 5 minutes  
**Next:** [Create Feature Pure BMAD](./02-Create-Feature-Pure-BMAD.md)
