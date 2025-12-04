# Instruction: Inventory Auditing Query

## Context
This query retrieves Inventory Auditing data from the Infor Visual ERP database.

**IMPORTANT**: MTM does NOT use the Visual TRACE feature for lot/serial tracking. Exclude all TRACE-related tables and columns.

## Tables
- INVENTORY_TRANS
- PART
- LOCATION
- WAREHOUSE
- APPLICATION_USER

## Primary Keys
- INVENTORY_TRANS.TRANSACTION_ID (Primary Key)
- PART.ID (Primary Key)

## Table Relationships
1. **INVENTORY_TRANS ↔ PART**
   - Relationship: Many-to-One
   - Join: `INVENTORY_TRANS.PART_ID = PART.ID`
   - Description: Links transactions to specific parts

2. **INVENTORY_TRANS ↔ LOCATION**
   - Relationship: Many-to-One (for FROM and TO locations)
   - Join: `INVENTORY_TRANS.FROM_WAREHOUSE_ID = LOCATION.WAREHOUSE_ID AND INVENTORY_TRANS.FROM_LOCATION_ID = LOCATION.ID`
   - Description: Tracks source and destination locations

5. **INVENTORY_TRANS ↔ APPLICATION_USER**
   - Relationship: Many-to-One
   - Join: `INVENTORY_TRANS.USER_ID = APPLICATION_USER.NAME`
   - Description: Links transactions to users

## Key Columns
- **INVENTORY_TRANS.TRANSACTION_ID** - Unique transaction identifier
- **INVENTORY_TRANS.TRANSACTION_TYPE** - Type of transaction (Receipt, Issue, Transfer, etc.)
- **INVENTORY_TRANS.TRANSACTION_DATE** - Date/time of transaction
- **INVENTORY_TRANS.QTY** - Quantity involved
- **INVENTORY_TRANS.USER_ID** - User who performed transaction
- **INVENTORY_TRANS.REFERENCE** - Reference information
- **INVENTORY_TRANS.FROM_WAREHOUSE_ID** - Source warehouse
- **INVENTORY_TRANS.FROM_LOCATION_ID** - Source location
- **INVENTORY_TRANS.TO_WAREHOUSE_ID** - Destination warehouse
- **INVENTORY_TRANS.TO_LOCATION_ID** - Destination location
- **PART.ID** - Part identifier
- **PART.DESCRIPTION** - Part description
- **LOCATION.ID** - Location identifier
- **WAREHOUSE.ID** - Warehouse identifier

## Transaction Types to Include
- Receipts (receiving inventory)
- Issues (removing from inventory)
- Transfers (moving between locations)
- Adjustments (quantity corrections)
- Physical counts

## Audit Trail Fields
- Transaction ID
- Date/Time
- User
- Transaction Type
- Part
- Quantity
- From/To Locations
- Reference/Notes

**Note**: NO lot/serial tracking - MTM does not use Visual TRACE feature

## Constraints
- Read-only access
- No stored procedures
