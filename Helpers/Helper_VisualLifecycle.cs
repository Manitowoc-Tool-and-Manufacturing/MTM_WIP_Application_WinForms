using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers
{
    /// <summary>
    /// Helper for processing Visual ERP inventory transactions into lifecycle events.
    /// </summary>
    public static class Helper_VisualLifecycle
    {
        /// <summary>
        /// Processes raw transaction data into a structured lifecycle view.
        /// Groups related transfers and identifies receipts/shipments.
        /// </summary>
        /// <param name="rawData">Raw DataTable from INVENTORY_TRANS.</param>
        /// <returns>DataTable with processed lifecycle events.</returns>
        public static DataTable ProcessTransactions(DataTable rawData)
        {
            var resultTable = new DataTable();
            resultTable.Columns.Add("Trans ID", typeof(string));
            resultTable.Columns.Add("Date", typeof(DateTime));
            resultTable.Columns.Add("Time", typeof(string));
            resultTable.Columns.Add("Action", typeof(string));
            resultTable.Columns.Add("Qty", typeof(decimal));
            resultTable.Columns.Add("Warehouse", typeof(string));
            resultTable.Columns.Add("Flow", typeof(string));
            resultTable.Columns.Add("Reference", typeof(string));
            resultTable.Columns.Add("User", typeof(string));
            resultTable.Columns.Add("Part ID", typeof(string));
            resultTable.Columns.Add("RowType", typeof(string)); // For styling: receipt, transfer-out, shipment, etc.

            if (rawData == null || rawData.Rows.Count == 0)
                return resultTable;

            // Convert to list of objects for easier manipulation
            var transactions = rawData.AsEnumerable().Select(row => new
            {
                Id = row.Field<int>("TRANSACTION_ID"),
                Date = row.Field<DateTime?>("CREATE_DATE") ?? row.Field<DateTime>("TRANSACTION_DATE"),
                Type = row.Field<string>("TYPE"),
                Qty = row.Field<decimal>("QTY"),
                Warehouse = row.Field<string>("WAREHOUSE_ID"),
                Location = row.Field<string>("LOCATION_ID"),
                User = row.Field<string>("USER_ID"),
                PartId = row.Field<string>("PART_ID"),
                WorkOrder = row.Field<string>("WORKORDER_BASE_ID"),
                CustOrder = row.Field<string>("CUST_ORDER_ID"),
                PurcOrder = row.Field<string>("PURC_ORDER_ID"),
                Row = row
            }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Id).ToList();

            var usedIndices = new HashSet<int>();

            // 1. Process Receipts (Work Orders)
            for (int i = 0; i < transactions.Count; i++)
            {
                var t = transactions[i];
                if (usedIndices.Contains(i)) continue;

                if (t.Type == "I" && !string.IsNullOrEmpty(t.WorkOrder))
                {
                    AddRow(resultTable, t.Id.ToString(), t.Date, "RECEIPT", t.Qty, t.Warehouse ?? "", 
                        $"{t.PartId} -> {t.Location}", $"WO: {t.WorkOrder}", t.User ?? "", t.PartId ?? "", "receipt");
                    usedIndices.Add(i);
                }
            }

            // 2. Process Shipments (Customer Orders)
            for (int i = 0; i < transactions.Count; i++)
            {
                var t = transactions[i];
                if (usedIndices.Contains(i)) continue;

                if (t.Type == "O" && !string.IsNullOrEmpty(t.CustOrder))
                {
                    AddRow(resultTable, t.Id.ToString(), t.Date, "SHIPMENT", t.Qty, t.Warehouse ?? "",
                        $"{t.PartId}: {t.Location} -> Customer", $"CO: {t.CustOrder}", t.User ?? "", t.PartId ?? "", "shipment");
                    usedIndices.Add(i);
                }
            }

            // 3. Process Transfers (Pair OUT with IN)
            // We look for an OUT transaction, then search for a matching IN transaction
            // that happened shortly after (or same time) with same Qty and Part.
            for (int i = 0; i < transactions.Count; i++)
            {
                var tOut = transactions[i];
                if (usedIndices.Contains(i)) continue;

                if (tOut.Type == "O" && string.IsNullOrEmpty(tOut.CustOrder))
                {
                    int bestMatchIndex = -1;
                    double minTimeDiff = double.MaxValue;

                    // Look for matching IN
                    for (int j = 0; j < transactions.Count; j++)
                    {
                        if (i == j || usedIndices.Contains(j)) continue;
                        var tIn = transactions[j];

                        if (tIn.Type == "I" && 
                            string.IsNullOrEmpty(tIn.WorkOrder) && 
                            string.IsNullOrEmpty(tIn.PurcOrder) &&
                            tIn.PartId == tOut.PartId &&
                            Math.Abs(tIn.Qty) == Math.Abs(tOut.Qty))
                        {
                            // Check time difference (IN should be >= OUT, usually very close)
                            // Since we sorted Descending, tOut (index i) might be "later" in list if it happened "earlier" in time?
                            // Wait, list is sorted by Date DESC. So index 0 is newest.
                            // If tOut is at i, and tIn is at j.
                            // If tIn happened AFTER tOut, tIn.Date >= tOut.Date.
                            // So tIn should be at a LOWER index (or same) if sorted DESC.
                            
                            // Let's just check absolute time difference
                            var diff = (tIn.Date - tOut.Date).TotalSeconds;
                            
                            // We expect them to be very close, usually within seconds.
                            // Allow up to 5 minutes (300 seconds)
                            if (Math.Abs(diff) < 300)
                            {
                                if (Math.Abs(diff) < minTimeDiff)
                                {
                                    minTimeDiff = Math.Abs(diff);
                                    bestMatchIndex = j;
                                }
                            }
                        }
                    }

                    if (bestMatchIndex != -1)
                    {
                        var tIn = transactions[bestMatchIndex];
                        AddRow(resultTable, $"{tOut.Id}/{tIn.Id}", tOut.Date, "TRANSFER", tOut.Qty, tOut.Warehouse ?? "",
                            $"{tOut.PartId}: {tOut.Location} -> {tIn.Location}", "Transfer", tOut.User ?? "", tOut.PartId ?? "", "transfer-out"); // Using transfer-out style for combined
                        usedIndices.Add(i);
                        usedIndices.Add(bestMatchIndex);
                    }
                }
            }

            // 4. Process Remaining (Solo) Transactions
            for (int i = 0; i < transactions.Count; i++)
            {
                if (usedIndices.Contains(i)) continue;
                var t = transactions[i];

                string action = t.Type == "I" ? "IN (Solo)" : "OUT (Solo)";
                string flow = $"{t.PartId} @ {t.Location}";
                string refText = !string.IsNullOrEmpty(t.WorkOrder) ? $"WO: {t.WorkOrder}" :
                                 !string.IsNullOrEmpty(t.CustOrder) ? $"CO: {t.CustOrder}" :
                                 !string.IsNullOrEmpty(t.PurcOrder) ? $"PO: {t.PurcOrder}" : "";
                string style = t.Type == "I" ? "transfer-in" : "transfer-out";

                AddRow(resultTable, t.Id.ToString(), t.Date, action, t.Qty, t.Warehouse ?? "", flow, refText, t.User ?? "", t.PartId ?? "", style);
            }

            // Sort final result by Date DESC
            DataView dv = resultTable.DefaultView;
            dv.Sort = "Date DESC, [Trans ID] DESC";
            return dv.ToTable();
        }

        private static void AddRow(DataTable dt, string id, DateTime date, string action, decimal qty, 
            string warehouse, string flow, string reference, string user, string partId, string rowType)
        {
            dt.Rows.Add(id, date, date.ToString("HH:mm"), action, qty, warehouse, flow, reference, user, partId, rowType);
        }
    }
}
