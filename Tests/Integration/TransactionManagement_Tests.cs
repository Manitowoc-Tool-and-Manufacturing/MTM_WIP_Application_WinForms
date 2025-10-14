using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration
{
    /// <summary>
    /// Integration tests for transaction management and rollback behavior.
    /// Tests verify that multi-step database operations maintain atomicity
    /// and properly roll back on failures without leaving partial updates.
    /// </summary>
    [TestClass]
    public class TransactionManagement_Tests
    {
        #region Test Context

        public TestContext? TestContext { get; set; }

        #endregion

        #region Transaction Rollback Tests

        /// <summary>
        /// Tests that TransferInventoryQuantityAsync properly rolls back on failure,
        /// leaving zero orphaned records.
        /// </summary>
        [TestMethod]
        public async Task TransferInventoryQuantityAsync_MidOperationFailure_RollsBackCompletely()
        {
            // Arrange - Add test inventory item
            var batchNumber = "TXN-ROLLBACK-001";
            var partId = "TXN-PART-001";
            var operation = "100";
            var originalLocation = "FLOOR";
            var invalidLocation = "INVALID_LOCATION_DOES_NOT_EXIST";
            var transferQuantity = 10;
            var originalQuantity = 10;

            var addResult = await Dao_Inventory.AddInventoryItemAsync(
                partId, originalLocation, operation, originalQuantity, "Standard", 
                "TxnTestUser", batchNumber, "Transaction test", true);
            Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");

            // Get baseline inventory state
            var baselineSearch = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation);
            Assert.IsTrue(baselineSearch.IsSuccess, "Failed to get baseline inventory state");
            var baselineRowCount = baselineSearch.Data?.Rows.Count ?? 0;

            Console.WriteLine($"[Transaction Rollback Test] Baseline state:");
            Console.WriteLine($"  Batch: {batchNumber}, Part: {partId}, Operation: {operation}");
            Console.WriteLine($"  Location: {originalLocation}, Quantity: {originalQuantity}");
            Console.WriteLine($"  Baseline Row Count: {baselineRowCount}");

            // Act - Attempt transfer to invalid location (should fail mid-operation)
            var transferResult = await Dao_Inventory.TransferInventoryQuantityAsync(
                batchNumber, partId, operation, transferQuantity, originalQuantity, 
                invalidLocation, "TxnTestUser");

            // Assert - Transfer should fail
            Assert.IsFalse(transferResult.IsSuccess, 
                "Expected transfer to fail with invalid location");
            Console.WriteLine($"[Transaction Rollback Test] Transfer failed as expected: {transferResult.ErrorMessage}");

            // Verify complete rollback - no partial updates
            var postFailureSearch = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation);
            Assert.IsTrue(postFailureSearch.IsSuccess, "Failed to get post-failure inventory state");

            var postFailureRowCount = postFailureSearch.Data?.Rows.Count ?? 0;
            Console.WriteLine($"[Transaction Rollback Test] Post-failure state:");
            Console.WriteLine($"  Row Count: {postFailureRowCount}");

            // Row count should remain unchanged
            Assert.AreEqual(baselineRowCount, postFailureRowCount,
                "Expected row count to remain unchanged after failed transfer (complete rollback)");

            // Verify original record still exists at original location
            bool foundAtOriginalLocation = false;
            if (postFailureSearch.Data != null)
            {
                foreach (DataRow row in postFailureSearch.Data.Rows)
                {
                    if (row["BatchNumber"].ToString() == batchNumber &&
                        row["Location"].ToString() == originalLocation)
                    {
                        foundAtOriginalLocation = true;
                        Console.WriteLine($"[Transaction Rollback Test] ✓ Original record found at {originalLocation}");
                        break;
                    }
                }
            }
            Assert.IsTrue(foundAtOriginalLocation,
                "Expected original inventory record to still exist at original location after rollback");

            // Verify NO record exists at invalid location (no partial insert)
            bool foundAtInvalidLocation = false;
            if (postFailureSearch.Data != null)
            {
                foreach (DataRow row in postFailureSearch.Data.Rows)
                {
                    if (row["BatchNumber"].ToString() == batchNumber &&
                        row["Location"].ToString() == invalidLocation)
                    {
                        foundAtInvalidLocation = true;
                        break;
                    }
                }
            }
            Assert.IsFalse(foundAtInvalidLocation,
                "Expected zero records at invalid location after rollback (no orphaned data)");

            Console.WriteLine($"[Transaction Rollback Test] ✓ Complete rollback verified - zero orphaned records");
        }

        /// <summary>
        /// Tests that successful multi-step transfers commit atomically.
        /// All steps succeed together or fail together.
        /// </summary>
        [TestMethod]
        public async Task TransferInventoryQuantityAsync_ValidTransfer_CommitsAtomically()
        {
            // Arrange
            var batchNumber = "TXN-COMMIT-002";
            var partId = "TXN-PART-002";
            var operation = "100";
            var originalLocation = "FLOOR";
            var newLocation = "SHIPPING";
            var transferQuantity = 5;
            var originalQuantity = 10;

            var addResult = await Dao_Inventory.AddInventoryItemAsync(
                partId, originalLocation, operation, originalQuantity, "Standard",
                "TxnTestUser", batchNumber, "Transaction test", true);
            Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");

            Console.WriteLine($"[Transaction Commit Test] Initial state:");
            Console.WriteLine($"  Batch: {batchNumber}, Part: {partId}");
            Console.WriteLine($"  Original Location: {originalLocation}, Quantity: {originalQuantity}");
            Console.WriteLine($"  Transfer to: {newLocation}, Quantity: {transferQuantity}");

            // Act - Perform valid transfer
            var transferResult = await Dao_Inventory.TransferInventoryQuantityAsync(
                batchNumber, partId, operation, transferQuantity, originalQuantity,
                newLocation, "TxnTestUser");

            // Assert - Transfer should succeed
            Assert.IsTrue(transferResult.IsSuccess,
                $"Expected transfer to succeed, got failure: {transferResult.ErrorMessage}");
            Console.WriteLine($"[Transaction Commit Test] ✓ Transfer succeeded");

            // Verify atomic commit - both locations updated correctly
            var postTransferSearch = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation);
            Assert.IsTrue(postTransferSearch.IsSuccess, "Failed to get post-transfer inventory state");

            bool foundAtOriginalLocation = false;
            bool foundAtNewLocation = false;
            int remainingAtOriginal = 0;
            int quantityAtNew = 0;

            if (postTransferSearch.Data != null)
            {
                foreach (DataRow row in postTransferSearch.Data.Rows)
                {
                    if (row["BatchNumber"].ToString() == batchNumber)
                    {
                        var location = row["Location"].ToString();
                        var quantity = Convert.ToInt32(row["Quantity"]);

                        if (location == originalLocation)
                        {
                            foundAtOriginalLocation = true;
                            remainingAtOriginal = quantity;
                        }
                        else if (location == newLocation)
                        {
                            foundAtNewLocation = true;
                            quantityAtNew = quantity;
                        }
                    }
                }
            }

            Console.WriteLine($"[Transaction Commit Test] Post-transfer state:");
            Console.WriteLine($"  At {originalLocation}: {(foundAtOriginalLocation ? $"Found, Quantity={remainingAtOriginal}" : "Not found")}");
            Console.WriteLine($"  At {newLocation}: {(foundAtNewLocation ? $"Found, Quantity={quantityAtNew}" : "Not found")}");

            // Verify quantities are correct (atomic commit of both updates)
            if (transferQuantity < originalQuantity)
            {
                Assert.IsTrue(foundAtOriginalLocation,
                    $"Expected partial quantity to remain at {originalLocation}");
                Assert.AreEqual(originalQuantity - transferQuantity, remainingAtOriginal,
                    $"Expected {originalQuantity - transferQuantity} to remain at {originalLocation}");
            }

            Assert.IsTrue(foundAtNewLocation,
                $"Expected transferred inventory at {newLocation}");
            Assert.AreEqual(transferQuantity, quantityAtNew,
                $"Expected {transferQuantity} at {newLocation}");

            Console.WriteLine($"[Transaction Commit Test] ✓ Atomic commit verified - all quantities correct");
        }

        /// <summary>
        /// Tests that concurrent transfers to the same part handle conflicts properly
        /// without leaving inconsistent state.
        /// </summary>
        [TestMethod]
        public async Task ConcurrentTransfers_SamePart_MaintainConsistency()
        {
            // Arrange
            var batchNumber = "TXN-CONCURRENT-003";
            var partId = "TXN-PART-003";
            var operation = "100";
            var originalLocation = "FLOOR";
            var originalQuantity = 100;

            var addResult = await Dao_Inventory.AddInventoryItemAsync(
                partId, originalLocation, operation, originalQuantity, "Standard",
                "TxnTestUser", batchNumber, "Concurrent test", true);
            Assert.IsTrue(addResult.IsSuccess, "Failed to add test inventory item");

            Console.WriteLine($"[Concurrent Transfer Test] Initial quantity: {originalQuantity}");

            // Act - Attempt 5 concurrent transfers of 10 units each
            var transferTasks = new[]
            {
                Dao_Inventory.TransferInventoryQuantityAsync(
                    batchNumber, partId, operation, 10, originalQuantity, "SHIPPING", "TxnUser1"),
                Dao_Inventory.TransferInventoryQuantityAsync(
                    batchNumber, partId, operation, 10, originalQuantity, "RECEIVING", "TxnUser2"),
                Dao_Inventory.TransferInventoryQuantityAsync(
                    batchNumber, partId, operation, 10, originalQuantity, "STORAGE", "TxnUser3"),
                Dao_Inventory.TransferInventoryQuantityAsync(
                    batchNumber, partId, operation, 10, originalQuantity, "INSPECTION", "TxnUser4"),
                Dao_Inventory.TransferInventoryQuantityAsync(
                    batchNumber, partId, operation, 10, originalQuantity, "HOLD", "TxnUser5")
            };

            var results = await Task.WhenAll(transferTasks);

            // Assert - Some transfers may fail due to conflicts, but data should remain consistent
            var successCount = 0;
            var failureCount = 0;
            foreach (var result in results)
            {
                if (result.IsSuccess)
                    successCount++;
                else
                    failureCount++;
            }

            Console.WriteLine($"[Concurrent Transfer Test] Results:");
            Console.WriteLine($"  Successful Transfers: {successCount}");
            Console.WriteLine($"  Failed Transfers: {failureCount}");

            // Verify total quantity remains consistent
            var finalSearch = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, operation);
            Assert.IsTrue(finalSearch.IsSuccess, "Failed to get final inventory state");

            int totalQuantity = 0;
            if (finalSearch.Data != null)
            {
                foreach (DataRow row in finalSearch.Data.Rows)
                {
                    if (row["BatchNumber"].ToString() == batchNumber)
                    {
                        totalQuantity += Convert.ToInt32(row["Quantity"]);
                    }
                }
            }

            Console.WriteLine($"[Concurrent Transfer Test] Final total quantity: {totalQuantity}");
            
            // Total quantity should equal original (no quantity lost or gained)
            Assert.AreEqual(originalQuantity, totalQuantity,
                "Expected total quantity to remain consistent after concurrent transfers");

            Console.WriteLine($"[Concurrent Transfer Test] ✓ Consistency maintained despite concurrent operations");
        }

        #endregion
    }
}
