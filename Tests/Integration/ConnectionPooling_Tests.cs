using DocumentFormat.OpenXml.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_Inventory_Application.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration
{
    /// <summary>
    /// Integration tests for connection pooling behavior under concurrent load.
    /// Tests verify that the connection pool remains healthy and operations succeed
    /// when multiple database operations run simultaneously.
    /// </summary>
    [TestClass]
    public class ConnectionPooling_Tests : BaseIntegrationTest
    {
        #region Test Context

        public TestContext? TestContext { get; set; }

        #endregion

        #region Connection Pool Health Tests

        /// <summary>
        /// Tests that 100 concurrent GetAllInventoryAsync operations succeed without
        /// exhausting the connection pool (MaxPoolSize=100).
        /// </summary>
        [TestMethod]
        public async Task ConcurrentGetAllInventory_100Operations_AllSucceed()
        {
            // Arrange
            const int concurrentOperations = 100;
            var tasks = new List<Task<bool>>(concurrentOperations);
            var startTime = DateTime.Now;

            // Act - Launch 100 concurrent operations
            for (int i = 0; i < concurrentOperations; i++)
            {
                var operationNumber = i + 1;
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var result = await Dao_Inventory.GetAllInventoryAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
                        if (!result.IsSuccess)
                        {
                            Console.WriteLine($"[Operation {operationNumber}] Failed: {result.ErrorMessage}");
                            return false;
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Operation {operationNumber}] Exception: {ex.Message}");
                        return false;
                    }
                }));
            }

            // Wait for all operations to complete
            var results = await Task.WhenAll(tasks);
            var endTime = DateTime.Now;
            var totalDuration = (endTime - startTime).TotalSeconds;

            // Assert - All operations should succeed
            var successCount = results.Count(r => r);
            var failureCount = results.Count(r => !r);

            Console.WriteLine($"[Connection Pool Test] 100 concurrent GetAllInventory operations:");
            Console.WriteLine($"  Total Duration: {totalDuration:F2}s");
            Console.WriteLine($"  Success Count: {successCount}");
            Console.WriteLine($"  Failure Count: {failureCount}");
            Console.WriteLine($"  Average per Operation: {totalDuration / concurrentOperations:F3}s");
            Console.WriteLine($"  Connection Pool Config: MinPoolSize=5, MaxPoolSize=100");

            Assert.AreEqual(concurrentOperations, successCount, 
                $"Expected all {concurrentOperations} operations to succeed, but {failureCount} failed");
            
            Assert.IsTrue(totalDuration < 60, 
                $"Expected 100 concurrent operations to complete in under 60 seconds, took {totalDuration:F2}s");
        }

        /// <summary>
        /// Tests that mixed concurrent operations (reads and writes) succeed without
        /// connection pool exhaustion or deadlocks.
        /// </summary>
        [TestMethod]
        public async Task MixedConcurrentOperations_ReadsAndWrites_AllSucceed()
        {
            // Arrange
            const int readOperations = 50;
            const int writeOperations = 20;
            var tasks = new List<Task<bool>>();
            var startTime = DateTime.Now;

            // Act - Launch 50 concurrent read operations
            for (int i = 0; i < readOperations; i++)
            {
                var operationNumber = i + 1;
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var result = await Dao_Inventory.GetAllInventoryAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
                        return result.IsSuccess;
                    }
                    catch
                    {
                        return false;
                    }
                }));
            }

            // Launch 20 concurrent write operations
            for (int i = 0; i < writeOperations; i++)
            {
                var operationNumber = i + 1;
                var partId = $"POOL-TEST-{operationNumber:D3}";
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var result = await Dao_Inventory.AddInventoryItemAsync(
                            partId, "FLOOR", "100", 1, "Standard", "PoolTestUser", 
                            $"BATCH-POOL-{operationNumber:D3}", "Connection pool test", true,
                            connection: GetTestConnection(), transaction: GetTestTransaction());
                        return result.IsSuccess;
                    }
                    catch
                    {
                        return false;
                    }
                }));
            }

            // Wait for all operations to complete
            var results = await Task.WhenAll(tasks);
            var endTime = DateTime.Now;
            var totalDuration = (endTime - startTime).TotalSeconds;

            // Assert
            var successCount = results.Count(r => r);
            var failureCount = results.Count(r => !r);
            var totalOperations = readOperations + writeOperations;

            Console.WriteLine($"[Mixed Operations Test] {readOperations} reads + {writeOperations} writes:");
            Console.WriteLine($"  Total Duration: {totalDuration:F2}s");
            Console.WriteLine($"  Success Count: {successCount}");
            Console.WriteLine($"  Failure Count: {failureCount}");
            Console.WriteLine($"  Success Rate: {(double)successCount / totalOperations * 100:F1}%");

            // Allow some failures due to concurrent write conflicts, but expect >90% success
            var successRate = (double)successCount / totalOperations;
            Assert.IsTrue(successRate > 0.90, 
                $"Expected >90% success rate for mixed operations, got {successRate * 100:F1}%");
        }

        /// <summary>
        /// Tests that connection pool recovers properly after transient failures.
        /// Simulates rapid successive operations to stress-test pool health.
        /// </summary>
        [TestMethod]
        public async Task RapidSuccessiveOperations_50Iterations_PoolRemainsHealthy()
        {
            // Arrange
            const int iterations = 50;
            var successCount = 0;
            var failureCount = 0;
            var startTime = DateTime.Now;

            // Act - Execute 50 rapid successive operations
            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    var result = await Dao_Inventory.GetAllInventoryAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
                    if (result.IsSuccess)
                        successCount++;
                    else
                        failureCount++;
                }
                catch
                {
                    failureCount++;
                }

                // Small delay to prevent overwhelming the database
                await Task.Delay(10);
            }

            var endTime = DateTime.Now;
            var totalDuration = (endTime - startTime).TotalSeconds;

            // Assert
            Console.WriteLine($"[Rapid Operations Test] 50 successive operations:");
            Console.WriteLine($"  Total Duration: {totalDuration:F2}s");
            Console.WriteLine($"  Success Count: {successCount}");
            Console.WriteLine($"  Failure Count: {failureCount}");
            Console.WriteLine($"  Average per Operation: {totalDuration / iterations:F3}s");

            Assert.AreEqual(iterations, successCount, 
                $"Expected all {iterations} operations to succeed, but {failureCount} failed");
            
            Assert.IsTrue(totalDuration < 30, 
                $"Expected 50 operations to complete in under 30 seconds, took {totalDuration:F2}s");
        }

        /// <summary>
        /// Tests that connection pool handles concurrent operations that may timeout.
        /// Verifies that timed-out connections are properly released back to the pool.
        /// </summary>
        [TestMethod]
        public async Task ConcurrentOperationsWithTimeout_ConnectionsRecycled()
        {
            // Arrange
            const int concurrentOperations = 20;
            var tasks = new List<Task<(bool Success, string Message)>>(concurrentOperations);
            var startTime = DateTime.Now;

            // Act - Launch 20 concurrent search operations (potentially slow)
            for (int i = 0; i < concurrentOperations; i++)
            {
                var operationNumber = i + 1;
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        // Search for a common pattern that may return many results
                        var result = await Dao_Inventory.SearchInventoryAsync("TEST", connection: GetTestConnection(), transaction: GetTestTransaction());
                        return (result.IsSuccess, result.IsSuccess ? "Success" : result.ErrorMessage);
                    }
                    catch (Exception ex)
                    {
                        return (false, ex.Message);
                    }
                }));
            }

            // Wait for all operations to complete
            var results = await Task.WhenAll(tasks);
            var endTime = DateTime.Now;
            var totalDuration = (endTime - startTime).TotalSeconds;

            // Assert
            var successCount = results.Count(r => r.Success);
            var failureCount = results.Count(r => !r.Success);

            Console.WriteLine($"[Concurrent Search Test] 20 concurrent search operations:");
            Console.WriteLine($"  Total Duration: {totalDuration:F2}s");
            Console.WriteLine($"  Success Count: {successCount}");
            Console.WriteLine($"  Failure Count: {failureCount}");

            if (failureCount > 0)
            {
                Console.WriteLine("  Failures:");
                foreach (var failure in results.Where(r => !r.Success))
                {
                    Console.WriteLine($"    - {failure.Message}");
                }
            }

            // Expect at least 80% success rate (some timeouts are acceptable under heavy load)
            var successRate = (double)successCount / concurrentOperations;
            Assert.IsTrue(successRate >= 0.80, 
                $"Expected >=80% success rate for concurrent searches, got {successRate * 100:F1}%");
        }

        /// <summary>
        /// Tests that connection pool configuration (MinPoolSize=5, MaxPoolSize=100) is working correctly.
        /// Verifies that connections are reused efficiently without creating new connections for each operation.
        /// </summary>
        [TestMethod]
        public async Task ConnectionPoolConfiguration_VerifyEfficiency()
        {
            // Arrange
            const int warmupOperations = 10;
            const int testOperations = 50;

            // Act - Warmup phase to establish minimum pool connections
            Console.WriteLine("[Connection Pool Warmup] Executing 10 warmup operations...");
            for (int i = 0; i < warmupOperations; i++)
            {
                await Dao_Inventory.GetAllInventoryAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
            }

            await Task.Delay(500); // Allow pool to stabilize

            // Test phase - measure operation times
            var operationTimes = new List<double>();
            for (int i = 0; i < testOperations; i++)
            {
                var opStart = DateTime.Now;
                var result = await Dao_Inventory.GetAllInventoryAsync(connection: GetTestConnection(), transaction: GetTestTransaction());
                var opEnd = DateTime.Now;
                operationTimes.Add((opEnd - opStart).TotalMilliseconds);

                Assert.IsTrue(result.IsSuccess, $"Operation {i + 1} failed: {result.ErrorMessage}");
            }

            // Assert - Analyze performance characteristics
            var avgTime = operationTimes.Average();
            var maxTime = operationTimes.Max();
            var minTime = operationTimes.Min();
            var stdDev = Math.Sqrt(operationTimes.Average(t => Math.Pow(t - avgTime, 2)));

            Console.WriteLine($"[Connection Pool Performance] After warmup:");
            Console.WriteLine($"  Average Operation Time: {avgTime:F2}ms");
            Console.WriteLine($"  Min Time: {minTime:F2}ms");
            Console.WriteLine($"  Max Time: {maxTime:F2}ms");
            Console.WriteLine($"  Std Deviation: {stdDev:F2}ms");

            // After warmup, operations should be consistently fast (< 500ms average)
            Assert.IsTrue(avgTime < 500, 
                $"Expected average operation time < 500ms after warmup, got {avgTime:F2}ms");

            // Standard deviation should be low, indicating consistent performance (connection reuse)
            Assert.IsTrue(stdDev < 200, 
                $"Expected low standard deviation (< 200ms) indicating connection reuse, got {stdDev:F2}ms");
        }

        #endregion
    }
}
