# Instruction: Receiving Query

## Context
This query retrieves Receiving data from the Infor Visual ERP database.

## Tables
- RECEIVER
- RECEIVER_LINE
- PURCHASE_ORDER
- PURC_ORDER_LINE
- VENDOR
- PART
- WAREHOUSE
- LOCATION

## Primary Keys
- RECEIVER.ID (Primary Key)
- RECEIVER_LINE.RECEIVER_ID, RECEIVER_LINE.LINE_NO (Composite Primary Key)
- PURCHASE_ORDER.ID (Primary Key)
- PURC_ORDER_LINE.PURC_ORDER_ID, PURC_ORDER_LINE.LINE_NO (Composite Primary Key)
- VENDOR.ID (Primary Key)

## Table Relationships
1. **RECEIVER ↔ PURCHASE_ORDER**
   - Relationship: Many-to-One (Multiple receivers can reference one PO)
   - Join: `RECEIVER.PURC_ORDER_ID = PURCHASE_ORDER.ID`
   - Foreign Key: FKEY0167 (RECEIVER.PURC_ORDER_ID → PURCHASE_ORDER.ID)

2. **RECEIVER ↔ RECEIVER_LINE**
   - Relationship: One-to-Many (One receiver has many line items)
   - Join: `RECEIVER.ID = RECEIVER_LINE.RECEIVER_ID`
   - Foreign Key: FKEY0172 (RECEIVER_LINE.RECEIVER_ID → RECEIVER.ID)

3. **RECEIVER_LINE ↔ PURC_ORDER_LINE**
   - Relationship: Many-to-One
   - Join: `RECEIVER_LINE.PURC_ORDER_ID = PURC_ORDER_LINE.PURC_ORDER_ID AND RECEIVER_LINE.PURC_ORDER_LINE_NO = PURC_ORDER_LINE.LINE_NO`
   - Foreign Key: FKEY0171 (RECEIVER_LINE.PURC_ORDER_ID, PURC_ORDER_LINE_NO → PURC_ORDER_LINE)

4. **PURCHASE_ORDER ↔ VENDOR**
   - Relationship: Many-to-One
   - Join: `PURCHASE_ORDER.VENDOR_ID = VENDOR.ID`
   - Foreign Key: FKEY0079 (PURCHASE_ORDER.VENDOR_ID → VENDOR.ID)

5. **PURC_ORDER_LINE ↔ PART**
   - Relationship: Many-to-One
   - Join: `PURC_ORDER_LINE.PART_ID = PART.ID`
   - Foreign Key: FKEY0101 (PURC_ORDER_LINE.PART_ID → PART.ID)

6. **RECEIVER_LINE ↔ LOCATION**
   - Relationship: Many-to-One (for receiving location)
   - Join: `RECEIVER_LINE.WAREHOUSE_ID = LOCATION.WAREHOUSE_ID AND RECEIVER_LINE.LOCATION_ID = LOCATION.ID`

## Key Columns
- **RECEIVER.ID** - Receiver identifier
- **RECEIVER.PURC_ORDER_ID** - Purchase order number
- **RECEIVER.RECEIVED_DATE** - Date received
- **RECEIVER.USER_ID** - User who received
- **RECEIVER.CREATE_DATE** - Date record created
- **RECEIVER.CARRIER_ID** - Shipping carrier
- **RECEIVER.BOL_ID** - Bill of lading
- **RECEIVER_LINE.LINE_NO** - Line item number
- **RECEIVER_LINE.PART_ID** - Part received
- **RECEIVER_LINE.RECEIVED_QTY** - Quantity received
- **RECEIVER_LINE.ACCEPTED_QTY** - Quantity accepted
- **RECEIVER_LINE.REJECTED_QTY** - Quantity rejected
- **PURCHASE_ORDER.VENDOR_ID** - Vendor identifier
- **PURCHASE_ORDER.PO_DATE** - PO date
- **PURCHASE_ORDER.STATUS** - PO status
- **VENDOR.NAME** - Vendor name
- **PART.DESCRIPTION** - Part description
- **LOCATION.ID** - Receiving location

## Calculated Fields
- **Variance** = RECEIVED_QTY - ORDERED_QTY
- **Acceptance Rate** = (ACCEPTED_QTY / RECEIVED_QTY) * 100
- **Outstanding PO Lines** = Lines not fully received

## Additional Related Tables
- **RECEIVER_LINE_DEL** - Delivery schedule details
- **RECEIVER_BINARY** - Attachments/documents
- **CONSIGN_RECEIVER** - Consignment receipts (if applicable)

## Constraints
- Read-only access
- No stored procedures
