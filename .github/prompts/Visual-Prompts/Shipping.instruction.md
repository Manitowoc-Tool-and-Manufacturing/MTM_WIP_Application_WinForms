# Instruction: Shipping Query

## Context
This query retrieves Shipping data from the Infor Visual ERP database.

## Tables
- SHIPPER (Packlist/Shipment)
- SHIPPER_LINE
- CUSTOMER_ORDER
- CUST_ORDER_LINE
- CUSTOMER
- PART
- CARTON
- BOL (Bill of Lading)
- CARRIER

## Primary Keys
- SHIPPER.PACKLIST_ID (Primary Key)
- SHIPPER_LINE.PACKLIST_ID, SHIPPER_LINE.LINE_NO (Composite Primary Key)
- CUSTOMER_ORDER.ID (Primary Key)
- CUST_ORDER_LINE.CUST_ORDER_ID, CUST_ORDER_LINE.LINE_NO (Composite Primary Key)
- CUSTOMER.ID (Primary Key)
- CARTON.ID (Primary Key)
- BOL.ID (Primary Key)

## Table Relationships
1. **SHIPPER ↔ CUSTOMER_ORDER**
   - Relationship: Many-to-One (Multiple shipments per order)
   - Join: `SHIPPER.CUST_ORDER_ID = CUSTOMER_ORDER.ID`
   - Foreign Key: FKEY0157 (SHIPPER.CUST_ORDER_ID → CUSTOMER_ORDER.ID)

2. **SHIPPER ↔ SHIPPER_LINE**
   - Relationship: One-to-Many (One shipment has many line items)
   - Join: `SHIPPER.PACKLIST_ID = SHIPPER_LINE.PACKLIST_ID`
   - Foreign Key: FKEY0163 (SHIPPER_LINE.PACKLIST_ID → SHIPPER.PACKLIST_ID)

3. **SHIPPER_LINE ↔ CUST_ORDER_LINE**
   - Relationship: Many-to-One
   - Join: `SHIPPER_LINE.CUST_ORDER_ID = CUST_ORDER_LINE.CUST_ORDER_ID AND SHIPPER_LINE.CUST_ORDER_LINE_NO = CUST_ORDER_LINE.LINE_NO`
   - Foreign Key: FKEY0161 (SHIPPER_LINE.CUST_ORDER_ID, CUST_ORDER_LINE_NO → CUST_ORDER_LINE)

4. **CUSTOMER_ORDER ↔ CUSTOMER**
   - Relationship: Many-to-One
   - Join: `CUSTOMER_ORDER.CUSTOMER_ID = CUSTOMER.ID`
   - Foreign Key: FKEY0065 (CUSTOMER_ORDER.CUSTOMER_ID → CUSTOMER.ID)

5. **CUST_ORDER_LINE ↔ PART**
   - Relationship: Many-to-One
   - Join: `CUST_ORDER_LINE.PART_ID = PART.ID`
   - Foreign Key: FKEY0100 (CUST_ORDER_LINE.PART_ID → PART.ID)

6. **SHIPPER ↔ CARTON (via SHIPPER_LINK)**
   - Relationship: One-to-Many
   - Description: Links shipments to cartons for tracking

7. **SHIPPER ↔ BOL**
   - Relationship: Many-to-One (Multiple shipments on one BOL)
   - Join: `SHIPPER.BOL_ID = BOL.ID`
   - Description: Bill of lading for freight

8. **BOL ↔ CARRIER**
   - Relationship: Many-to-One
   - Join: `BOL.CARRIER_ID = CARRIER.ID`

## Key Columns
- **SHIPPER.PACKLIST_ID** - Shipment/Packlist identifier
- **SHIPPER.CUST_ORDER_ID** - Customer order number
- **SHIPPER.SHIP_DATE** - Date shipped
- **SHIPPER.CREATE_DATE** - Date created
- **SHIPPER.USER_ID** - User who created shipment
- **SHIPPER.BOL_ID** - Bill of lading number
- **SHIPPER.CARRIER_ID** - Shipping carrier
- **SHIPPER_LINE.LINE_NO** - Line item number
- **SHIPPER_LINE.PART_ID** - Part shipped
- **SHIPPER_LINE.SHIPPED_QTY** - Quantity shipped
- **SHIPPER_LINE.USER_SHIPPED_QTY** - User-entered quantity
- **CUSTOMER_ORDER.CUSTOMER_ID** - Customer identifier
- **CUSTOMER_ORDER.ORDER_DATE** - Order date
- **CUSTOMER.NAME** - Customer name
- **PART.DESCRIPTION** - Part description
- **CARTON.TRACKING_NO** - Tracking number
- **CARTON.TOTAL_WEIGHT** - Carton weight
- **CARRIER.NAME** - Carrier name

## Calculated Fields
- **Backorder Quantity** = ORDERED_QTY - SHIPPED_QTY
- **Ship Complete** = All lines fully shipped (Yes/No)
- **Days to Ship** = SHIP_DATE - ORDER_DATE

## Additional Related Tables
- **SHIPPER_LINE_DEL** - Delivery schedule details
- **SHIPPER_INVOICE** - Invoice linkage
- **INTRA_SHIPPER_LINE** - International shipment details
- **PALLET_DETAIL** - Pallet tracking

## Constraints
- Read-only access
- No stored procedures
