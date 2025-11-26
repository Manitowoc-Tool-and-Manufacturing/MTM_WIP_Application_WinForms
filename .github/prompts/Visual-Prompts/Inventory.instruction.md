# Instruction: Inventory Query

## Context
This query retrieves Inventory data from the Infor Visual ERP database.

## Tables
- PART
- PART_LOCATION
- PART_WAREHOUSE
- LOCATION
- WAREHOUSE
- INVENTORY_TRANS (for transaction history - optional)

**NOTE**: MTM does not use Visual TRACE feature for lot/serial tracking.

## Primary Keys
- PART.ID (Primary Key)
- PART_LOCATION.PART_ID, PART_LOCATION.WAREHOUSE_ID, PART_LOCATION.LOCATION_ID (Composite Primary Key)
- PART_WAREHOUSE.WAREHOUSE_ID, PART_WAREHOUSE.PART_ID (Composite Primary Key)
- INVENTORY_TRANS.TRANSACTION_ID (Primary Key - if using transaction history)

## Table Relationships
1. **PART ↔ PART_LOCATION**
   - Relationship: One-to-Many (One part can exist in multiple locations)
   - Join: `PART.ID = PART_LOCATION.PART_ID`
   - Foreign Key: FKEY0092 (PART_LOCATION.PART_ID → PART.ID)

2. **PART ↔ PART_WAREHOUSE**
   - Relationship: One-to-Many (One part can be stocked in multiple warehouses)
   - Join: `PART.ID = PART_WAREHOUSE.PART_ID`
   - Description: Warehouse-level inventory settings

3. **PART_LOCATION ↔ LOCATION**
   - Relationship: Many-to-One
   - Join: `PART_LOCATION.WAREHOUSE_ID = LOCATION.WAREHOUSE_ID AND PART_LOCATION.LOCATION_ID = LOCATION.ID`
   - Foreign Keys:
     - FKEY0119 (PART_LOCATION.LOCATION_ID → LOCATION.ID)
     - FKEY0119 (PART_LOCATION.WAREHOUSE_ID → LOCATION.WAREHOUSE_ID)

4. **LOCATION ↔ WAREHOUSE**
   - Relationship: Many-to-One
   - Join: `LOCATION.WAREHOUSE_ID = WAREHOUSE.ID`
   - Foreign Key: FKEY0117 (LOCATION.WAREHOUSE_ID → WAREHOUSE.ID)

5. **INVENTORY_TRANS (for history)**
   - Related through PART_ID, WAREHOUSE_ID, LOCATION_ID
   - Foreign Key: FK_INVTRANS_TO_SITE (INVENTORY_TRANS.SITE_ID → SITE.ID)
   - Use for transaction history and audit trail

## Key Columns
- **PART.ID** - Part identifier
- **PART.DESCRIPTION** - Part description
- **PART.STOCK_UM** - Stock unit of measure
- **PART_LOCATION.QTY_ON_HAND** - Current quantity on hand
- **PART_LOCATION.QTY_ALLOCATED** - Allocated quantity
- **PART_LOCATION.QTY_IN_INSPECTION** - Quantity in inspection
- **PART_WAREHOUSE.MIN_QTY** - Minimum quantity threshold
- **PART_WAREHOUSE.MAX_QTY** - Maximum quantity threshold
- **LOCATION.ID** - Location identifier
- **WAREHOUSE.ID** - Warehouse identifier
- **INVENTORY_TRANS.TRANSACTION_DATE** - Transaction timestamp
- **INVENTORY_TRANS.TRANSACTION_TYPE** - Type of transaction

## Calculated Fields
- **Available Quantity** = QTY_ON_HAND - QTY_ALLOCATED
- **Reorder Status** = Compare QTY_ON_HAND to MIN_QTY/MAX_QTY

## Constraints
- Read-only access
- No stored procedures
