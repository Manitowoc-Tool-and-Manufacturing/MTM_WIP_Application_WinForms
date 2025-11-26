# Instruction: Die Tool Discovery Query

## Context
This query retrieves Die Tool Discovery data from the Infor Visual ERP database.

**IMPORTANT**: Dies ARE tracked in the PART table but use a specific naming convention and custom User Defined fields for tracking.

## Die Identification System
Dies use part numbers with the pattern: **FGT{4-digit-number}-01**
- Example: FGT0025-01, FGT0100-01, FGT1234-01
- **Exception**: FGT0001-01 indicates NO die exists for that location

## Tables
- PART (for die records)
- User Defined Fields (UDF) on PART table

## User Defined Field Mapping

### Die Records (FGT part numbers)
When looking at a die record (PART.ID starting with 'FGT'):
- **USER_5** - Part number associated with the die
- **USER_9** - Coil used for the die
- **USER_2** - Die's physical location

### Part Records (Regular part numbers)
When looking at a regular part record to find its die:
- **USER_1** - Die number (FGT format) associated with this part
- **USER_7** - Customer name
- **USER_9** - Coil information

## Primary Keys
- PART.ID (Primary Key)

## Die Query Logic
1. Filter PART table WHERE PART.ID LIKE 'FGT____-01'
2. Exclude PART.ID = 'FGT0001-01' (indicates no die)
3. Join to User Defined fields to get:
   - Associated part (USER_5)
   - Coil information (USER_9)
   - Physical location (USER_2)

## Key Columns
- **PART.ID** - Die identifier (FGT format)
- **PART.DESCRIPTION** - Die description
- **PART.USER_5** - Part number associated with this die
- **PART.USER_9** - Coil used for this die
- **PART.USER_2** - Die's physical location
- **PART.TYPE** - Part type (should be die/tooling type)
- **PART.STATUS** - Die status

## Filtering Logic
- Filter: `PART.ID LIKE 'FGT____-01'`
- Exclude: `PART.ID <> 'FGT0001-01'`
- Consider PART.ACTIVE_FLAG for active dies only

## Constraints
- Read-only access
- No stored procedures
