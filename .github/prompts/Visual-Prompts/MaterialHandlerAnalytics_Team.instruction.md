# Instruction: Material Handler Analytics Team Query

## Context
This query retrieves Team Material Handler Analytics data from the Infor Visual ERP database, aggregated by user/shift/team.

**IMPORTANT**: Material Handlers do NOT use LABOR_TICKET or DEPARTMENT tables. They use MT_WIP_TRANSACTIONS for inventory movement tracking.

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
- **FROM_ITEM1** - Source identifier 1
- **FROM_ITEM2** - Source identifier 2
- **TO_ITEM1** - Destination identifier 1
- **TO_ITEM2** - Destination identifier 2
- **FROM_TYPE** - Source type
- **TO_TYPE** - Destination type
- **DATE** - Transaction date/time
- **USER** - User who performed transaction
- **QTY** - Quantity moved

## Table Relationships
1. **MT_WIP_TRANSACTIONS ↔ APPLICATION_USER**
   - Relationship: Many-to-One
   - Join: `MT_WIP_TRANSACTIONS.USER = APPLICATION_USER.NAME`
   - Description: Links transactions to users

## Key Columns
- **MT_WIP_TRANSACTIONS.USER** - Material handler identifier
- **MT_WIP_TRANSACTIONS.DATE** - Transaction timestamp
- **MT_WIP_TRANSACTIONS.QTY** - Quantity moved
- **MT_WIP_TRANSACTIONS.FROM_TYPE** - Source type
- **MT_WIP_TRANSACTIONS.TO_TYPE** - Destination type
- **APPLICATION_USER.NAME** - User name

## Team-Level Aggregations
Since there's no DEPARTMENT structure, aggregate by:
- **User** - Individual material handler
- **Shift** - Extract from DATE (morning/afternoon/night shifts)
- **Date Range** - Daily/weekly/monthly aggregations

## Calculated Metrics
- **Total Transactions** = COUNT(MT_WIP_TRANSACTIONS.ROWID)
- **Total Quantity Moved** = SUM(MT_WIP_TRANSACTIONS.QTY)
- **Unique Parts Handled** = COUNT(DISTINCT PART_ID)
- **Team Size** = COUNT(DISTINCT USER)
- **Average Transactions per User** = Total Transactions / Team Size
- **Average Quantity per User** = Total Quantity / Team Size
- **Transaction Types Distribution** = COUNT by FROM_TYPE and TO_TYPE

## Performance Metrics
- Transactions per user
- Quantity moved per user
- Transaction frequency (transactions per hour/day)
- Movement type breakdown (warehouse→location, location→location, etc.)

## Filtering
- Filter by date range (MT_WIP_TRANSACTIONS.DATE)
- Group by USER for team comparison
- Filter by transaction type (FROM_TYPE, TO_TYPE)

## Constraints
- Read-only access
- No stored procedures
