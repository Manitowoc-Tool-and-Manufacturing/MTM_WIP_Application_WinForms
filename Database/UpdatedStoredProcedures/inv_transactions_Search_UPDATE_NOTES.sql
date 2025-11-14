-- Updated Stored Procedure: inv_transactions_Search
-- Purpose: Search transactions with color code and work order columns
-- Feature: Color Code & Work Order Tracking - UPDATED
-- Date: 2025-11-13
-- MySQL Version: 5.7.24
-- Note: This is a minimal update to add ColorCode and WorkOrder columns to SELECT
-- Full procedure logic remains unchanged - only SELECT columns modified

USE mtm_wip_application_winforms;

-- This file documents the required change to inv_transactions_Search
-- Add ColorCode and WorkOrder to the SELECT statement columns:

/*
CHANGE REQUIRED IN EXISTING PROCEDURE:

In the final SELECT statement, add these two columns:
    ColorCode,        -- NEW: Color code column
    WorkOrder         -- NEW: Work order column

Full column list should be:
    SELECT
        ID,
        TransactionType,
        BatchNumber,
        PartID,
        FromLocation,
        ToLocation,
        Operation,
        Quantity,
        Notes,
        User,
        ItemType,
        ReceiveDate,
        ColorCode,        -- NEW
        WorkOrder         -- NEW
    FROM inv_transaction
    WHERE [existing WHERE clause]
    [existing ORDER BY and LIMIT clauses]

This ensures transactions display color code and work order data
for historical tracking and audit purposes.
*/

-- Manual Update Required:
-- 1. Open Database/UpdatedStoredProcedures/ReadyForVerification/transactions/inv_transactions_Search.sql
-- 2. Locate the SELECT statement (line ~100-120)
-- 3. Add ColorCode and WorkOrder columns after ReceiveDate
-- 4. Save and deploy updated procedure
