# Instruction: Material Handler Analytics General Query

## Context
This query retrieves General Material Handler Analytics data from the Infor Visual ERP database.

**IMPORTANT**: Material Handlers do NOT use LABOR_TICKET. They use the custom MT_WIP_TRANSACTIONS table for tracking inventory transactions.

## Tables
- MT_WIP_TRANSACTIONS (Custom MTM table for material handler transactions)
- APPLICATION_USER (for user information)
- PART (for part details, optional)

## Primary Keys
- MT_WIP_TRANSACTIONS.ROWID (Primary Key)
- APPLICATION_USER.NAME (Primary Key)

## Table Structure
**MT_WIP_TRANSACTIONS** contains:
- **ROWID** - Unique transaction identifier
- **PART_ID** - Part/item being moved
- **FROM_ITEM1** - Source identifier 1 (warehouse, location, etc.)
- **FROM_ITEM2** - Source identifier 2
- **TO_ITEM1** - Destination identifier 1
- **TO_ITEM2** - Destination identifier 2
- **FROM_TYPE** - Source type (warehouse, location, etc.)
- **TO_TYPE** - Destination type
- **DATE** - Transaction date/time
- **USER** - User who performed transaction
- **QTY** - Quantity moved

## Table Relationships
1. **MT_WIP_TRANSACTIONS ↔ APPLICATION_USER**
   - Relationship: Many-to-One
   - Join: `MT_WIP_TRANSACTIONS.USER = APPLICATION_USER.NAME`
   - Description: Links transactions to users

2. **MT_WIP_TRANSACTIONS ↔ PART (optional)**
   - Relationship: Many-to-One
   - Join: `MT_WIP_TRANSACTIONS.PART_ID = PART.ID`
   - Description: Links to part details if needed

## Key Columns
- **MT_WIP_TRANSACTIONS.USER** - Material handler identifier
- **MT_WIP_TRANSACTIONS.DATE** - Transaction timestamp
- **MT_WIP_TRANSACTIONS.QTY** - Quantity moved
- **MT_WIP_TRANSACTIONS.FROM_TYPE** - Source type
- **MT_WIP_TRANSACTIONS.TO_TYPE** - Destination type
- **MT_WIP_TRANSACTIONS.PART_ID** - Part moved
- **APPLICATION_USER.NAME** - User name

## Calculated Metrics
- **Total Transactions** = COUNT(MT_WIP_TRANSACTIONS.ROWID) per user
- **Total Quantity Moved** = SUM(MT_WIP_TRANSACTIONS.QTY) per user
- **Transaction Types** = Breakdown by FROM_TYPE and TO_TYPE
- **Parts Handled** = COUNT(DISTINCT PART_ID) per user
- **Transactions per Day** = COUNT grouped by DATE and USER

## Filtering
- Filter by date range (MT_WIP_TRANSACTIONS.DATE)
- Filter by user (MT_WIP_TRANSACTIONS.USER)
- Filter by transaction type (FROM_TYPE, TO_TYPE)

## Constraints
- Read-only access
- No stored procedures
