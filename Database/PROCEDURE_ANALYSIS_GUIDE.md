# Stored Procedure Analysis Guide

**What is this?** This guide explains the terms used in `procedure-transaction-analysis.csv` in simple English.

**Why does this matter?** Understanding these terms helps you know what each stored procedure does and how to work with it safely.

---

## Column Definitions

### ProcedureName
**What it means:** The name of the stored procedure (a saved database command).

**Example:** `inv_inventory_Add_Item`

**Why it matters:** This is how you identify which procedure you're looking at.

---

### Domain
**What it means:** The category or area of the application this procedure belongs to.

**Examples:**
- `inventory` - Managing parts and stock
- `logging` - Recording errors and history
- `master-data` - Basic settings like locations and part numbers
- `system` - User accounts and app settings
- `users` - User preferences and themes
- `uncategorized` - Special or maintenance procedures

**Why it matters:** Helps you find related procedures quickly.

---

### DetectedPattern
**What it means:** What type of work pattern the procedure follows.

**The 4 patterns:**

1. **READ_ONLY**
   - Only reads data from the database
   - Doesn't change anything
   - Like looking at a report
   - **Safest type** - can't break anything

2. **SINGLE_STEP**
   - Makes one change to the database
   - Like adding one item or updating one record
   - Simple and straightforward
   - **Pretty safe** - only one thing happens

3. **MULTI_STEP_SEQUENTIAL**
   - Makes several changes in order, one after another
   - Like: first insert a record, then update a counter
   - All steps happen in sequence
   - **Moderately complex** - needs careful testing

4. **MULTI_STEP_CONDITIONAL**
   - Makes changes based on conditions (if/then logic)
   - Like: "if this exists, update it; otherwise, insert it"
   - Has decision-making built in
   - **Most complex** - needs extra attention and testing

**Why it matters:** Tells you how complicated the procedure is and how carefully you need to test it.

---

### RecommendedStrategy
**What it means:** The best approach to use when working with this procedure.

**The 3 strategies:**

1. **StandardQuery**
   - Used for READ_ONLY procedures
   - Just fetch data and display it
   - No special handling needed

2. **StandardTransaction**
   - Used for SINGLE_STEP and MULTI_STEP_SEQUENTIAL procedures
   - Wrap changes in a transaction (safety wrapper)
   - If something fails, undo all changes automatically
   - Like a "save point" in a video game

3. **ComplexTransaction**
   - Used for MULTI_STEP_CONDITIONAL procedures
   - Needs extra testing and validation
   - Has multiple decision points
   - Requires careful error handling at each step

**Why it matters:** Tells developers how to call this procedure safely from the application.

---

### Confidence
**What it means:** How sure we are about the pattern detection (0-100%).

**How to read it:**
- **95%** = Very confident (READ_ONLY procedures are obvious)
- **90%** = Confident (SINGLE_STEP procedures are clear)
- **85%** = Pretty confident (multi-step procedures are complex but identifiable)

**Why it matters:** Lower confidence means a human should double-check the analysis.

---

### ProcedureType
**What it means:** What kind of database operations the procedure does.

**The 4 types:**

1. **SELECT_ONLY**
   - Only reads data (SELECT statements)
   - No changes to database
   - Example: Getting a list of users

2. **DML_ONLY**
   - Only makes changes (INSERT, UPDATE, DELETE)
   - Doesn't return data to display
   - Example: Deleting an old record

3. **MIXED**
   - Both reads AND writes data
   - Example: Update inventory then show the new totals

4. **UNKNOWN**
   - We couldn't figure it out (rare)
   - Needs manual review

**Why it matters:** Helps you know if the procedure will give you data back or just make changes.

---

### InsertCount
**What it means:** How many times the procedure adds new records (INSERT statements).

**Example:** `2` means it inserts 2 new records

**Why it matters:** More inserts = more complexity and more things that can go wrong.

---

### UpdateCount
**What it means:** How many times the procedure changes existing records (UPDATE statements).

**Example:** `1` means it updates 1 existing record

**Why it matters:** Updates need to be careful not to change the wrong records.

---

### DeleteCount
**What it means:** How many times the procedure removes records (DELETE statements).

**Example:** `1` means it deletes 1 record

**Why it matters:** Deletes are dangerous - you can't easily undo them!

---

### TotalDMLOps
**What it means:** Total number of changes (INSERT + UPDATE + DELETE combined).

**DML = Data Manipulation Language** (fancy term for changing data)

**Examples:**
- `0` = No changes (read-only)
- `1` = One change (simple)
- `4` = Four changes (complex)
- `9` = Nine changes (very complex!)

**Why it matters:** Higher numbers = more complexity = needs more testing.

---

### HasTransactionHandling
**What it means:** Does the procedure have safety features built in?

**Values:**
- **True** = Has safety features (START TRANSACTION, COMMIT, ROLLBACK)
- **False** = No safety features

**What transactions do:**
- START TRANSACTION = Begin tracking changes
- COMMIT = Save all changes permanently
- ROLLBACK = Undo all changes if something fails

**Example:** If you're transferring inventory between locations and an error happens halfway through, ROLLBACK prevents ending up with inventory in both places or neither place.

**Why it matters:** Procedures that change data SHOULD have transactions. If False and TotalDMLOps > 0, that's a problem.

---

### HasValidation
**What it means:** Does the procedure check inputs before using them?

**Values:**
- **True** = Checks inputs (like "is this part number valid?")
- **False** = Doesn't check inputs

**What validation looks like:**
```sql
IF p_PartNumber IS NULL OR p_PartNumber = '' THEN
    SET p_Status = -2;
    SET p_ErrorMsg = 'Part number is required';
END IF;
```

**Why it matters:** Validation prevents bad data from getting into the database.

---

### Rationale
**What it means:** A quick summary explaining how we detected the pattern.

**Example:** `Pattern: SINGLE_STEP | DML Ops: 1 | Type: DML_ONLY`

**Translation:** "We think this is a single-step procedure because it has 1 data change and only modifies data without reading it."

**Why it matters:** Helps you understand the reasoning behind the classification.

---

### DeveloperCorrection
**What it means:** A column for developers to fill in if the automatic detection was wrong.

**How to use it:**
1. Look at the DetectedPattern
2. Read the actual procedure code
3. If the pattern is wrong, write the correct pattern here
4. If the pattern is right, leave it blank

**Example:** If we said SINGLE_STEP but it's actually MULTI_STEP_CONDITIONAL, write that in this column.

**Why it matters:** Computers can make mistakes. Human review catches those mistakes before we make changes.

---

### Notes
**What it means:** Free-form space for any comments or observations.

**Examples:**
- "This procedure is used only during month-end closing"
- "Needs optimization - runs slowly with large datasets"
- "Part of critical inventory transfer workflow"

**Why it matters:** Captures important context that doesn't fit in other columns.

---

## Quick Reference: Pattern Types

| Pattern | Complexity | Changes DB? | Has Logic? | Example |
|---------|-----------|-------------|------------|---------|
| READ_ONLY | Simple ⭐ | No | No | Get list of parts |
| SINGLE_STEP | Simple ⭐⭐ | Yes (1 change) | No | Add one part |
| MULTI_STEP_SEQUENTIAL | Medium ⭐⭐⭐ | Yes (2+ changes) | No | Add part, update count |
| MULTI_STEP_CONDITIONAL | Complex ⭐⭐⭐⭐ | Yes (2+ changes) | Yes | If part exists update, else insert |

---

## Quick Reference: Recommended Strategies

| Strategy | Use For | What Developer Does |
|----------|---------|---------------------|
| StandardQuery | READ_ONLY | Just call it and display results |
| StandardTransaction | SINGLE_STEP, MULTI_STEP_SEQUENTIAL | Use transaction helpers, check status codes |
| ComplexTransaction | MULTI_STEP_CONDITIONAL | Extra validation, test all branches, careful error handling |

---

## How to Read the CSV

**Example row:**
```csv
"inv_inventory_Add_Item","inventory","MULTI_STEP_SEQUENTIAL","StandardTransaction","85","MIXED","2","2","0","4","True","True","Pattern: MULTI_STEP_SEQUENTIAL | DML Ops: 4 | Type: MIXED","",""
```

**Translation:**
- **Name:** `inv_inventory_Add_Item`
- **Category:** Inventory procedures
- **Pattern:** Makes several changes in sequence (not conditional)
- **How to use it:** Use standard transaction handling
- **Confidence:** 85% sure about this classification
- **What it does:** Both reads and writes (MIXED)
- **Changes:** 2 inserts + 2 updates = 4 total changes
- **Safety features:** Yes, has transactions ✓
- **Input checking:** Yes, validates inputs ✓
- **Summary:** 4 data changes, sequential pattern, mixed type
- **Corrections:** None needed (blank)
- **Notes:** None (blank)

**What this means:** This is a moderately complex procedure that adds inventory items. It makes 4 database changes in a specific order. It has good safety features and input checking. Use it with standard transaction handling.

---

## FAQs

### Q: What if Confidence is low?
**A:** Have a developer manually review the procedure code to confirm the pattern is correct.

### Q: What if HasTransactionHandling is False but TotalDMLOps > 0?
**A:** That's a red flag! The procedure changes data without safety features. Should be fixed.

### Q: What's the difference between MULTI_STEP_SEQUENTIAL and MULTI_STEP_CONDITIONAL?
**A:** 
- **SEQUENTIAL** = Always does all steps in order (step 1, step 2, step 3)
- **CONDITIONAL** = Does different steps based on conditions (if this, then that)

### Q: Why do some READ_ONLY procedures have HasTransactionHandling = True?
**A:** Some read-only procedures use transactions for data consistency (seeing a "snapshot" of data at one moment in time). It's okay but not always necessary.

### Q: What should I do with the DeveloperCorrection column?
**A:** During the review phase (T106a), developers should:
1. Read each procedure's code
2. Check if DetectedPattern matches what the code actually does
3. If wrong, write the correct pattern in DeveloperCorrection
4. If right, leave blank

---

## Summary

This CSV file is like a **report card for stored procedures**. It tells you:
- What each procedure does
- How complex it is
- What safety features it has
- How confident we are in our analysis
- Where humans need to double-check

Use this information to:
- Understand procedures without reading all the code
- Find procedures that need extra testing
- Identify procedures that need safety improvements
- Plan refactoring work (which procedures to fix first)

---

**Generated:** October 15, 2025  
**Part of:** Phase 2.5 Database Layer Standardization (Task T103)  
**Related files:** `compliance-report.csv`, `database-schema-snapshot.json`
