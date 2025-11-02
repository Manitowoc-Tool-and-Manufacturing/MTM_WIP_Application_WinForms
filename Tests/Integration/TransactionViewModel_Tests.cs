using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTM_WIP_Application_Winforms.Models;
using System.Data;

namespace MTM_WIP_Application_Winforms.Tests.Integration;

/// <summary>
/// Integration tests for TransactionViewModel operations.
/// Tests Excel export functionality and view model orchestration.
/// </summary>
[TestClass]
public class TransactionViewModel_Tests : BaseIntegrationTest
{
    #region Export Methods Tests

    /// <summary>
    /// Tests that ExportToExcelAsync creates a valid Excel file with correct data.
    /// Validates: file creation, Excel format, row count matches input, column headers present.
    /// </summary>
    [TestMethod]
    public async Task ExportToExcelAsync_ValidData_CreatesExcelFile()
    {
        // Arrange
        var viewModel = new TransactionViewModel();
        
        // Create sample transactions
        var transactions = new List<Model_Transactions>
        {
            new Model_Transactions
            {
                ID = 1,
                TransactionType = TransactionType.IN,
                BatchNumber = "BATCH001",
                PartID = "PART001",
                FromLocation = "RECEIVING",
                ToLocation = "FLOOR",
                Operation = "100",
                Quantity = 50,
                Notes = "Test transaction 1",
                User = "TestUser",
                ItemType = "WIP",
                DateTime = DateTime.Now.AddDays(-1)
            },
            new Model_Transactions
            {
                ID = 2,
                TransactionType = TransactionType.TRANSFER,
                BatchNumber = "BATCH002",
                PartID = "PART002",
                FromLocation = "FLOOR",
                ToLocation = "SHIPPING",
                Operation = "110",
                Quantity = 25,
                Notes = "Test transaction 2",
                User = "TestUser",
                ItemType = "WIP",
                DateTime = DateTime.Now
            }
        };

        // Create temp file path
        var tempFilePath = Path.Combine(Path.GetTempPath(), $"TransactionExport_Test_{Guid.NewGuid()}.xlsx");

        try
        {
            // Act
            var result = await viewModel.ExportToExcelAsync(tempFilePath, transactions);

            // Assert
            Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
            Assert.IsNotNull(result.Data, "Expected file path in result");
            Assert.AreEqual(tempFilePath, result.Data, "File path should match input");
            
            // Verify file was created
            Assert.IsTrue(File.Exists(tempFilePath), "Excel file should exist");
            
            // Verify file has content (not empty)
            var fileInfo = new FileInfo(tempFilePath);
            Assert.IsTrue(fileInfo.Length > 0, "Excel file should not be empty");
            
            // Verify file is a valid Excel file by checking extension and attempting to open
            Assert.AreEqual(".xlsx", fileInfo.Extension, "File should have .xlsx extension");
            
            // Basic validation that ClosedXML can read the file
            using (var workbook = new ClosedXML.Excel.XLWorkbook(tempFilePath))
            {
                Assert.IsNotNull(workbook, "ClosedXML should be able to open the file");
                Assert.IsTrue(workbook.Worksheets.Count > 0, "Workbook should have at least one worksheet");
                
                var worksheet = workbook.Worksheets.First();
                Assert.IsNotNull(worksheet, "Worksheet should not be null");
                
                // Verify row count (header + data rows)
                var usedRowCount = worksheet.LastRowUsed()?.RowNumber() ?? 0;
                Assert.AreEqual(transactions.Count + 1, usedRowCount, 
                    $"Expected {transactions.Count + 1} rows (1 header + {transactions.Count} data), got {usedRowCount}");
                
                // Verify column headers exist (row 1)
                var headerRow = worksheet.Row(1);
                Assert.IsNotNull(headerRow, "Header row should exist");
                
                // Verify key columns are present
                var headers = new List<string>();
                for (int col = 1; col <= 12; col++) // 12 columns expected
                {
                    var cell = headerRow.Cell(col);
                    if (!string.IsNullOrEmpty(cell.GetValue<string>()))
                    {
                        headers.Add(cell.GetValue<string>());
                    }
                }
                
                Assert.IsTrue(headers.Count >= 10, "Should have at least 10 column headers");
                Assert.IsTrue(headers.Contains("Transaction Type"), "Should have 'Transaction Type' column");
                Assert.IsTrue(headers.Contains("Part Number"), "Should have 'Part Number' column");
                Assert.IsTrue(headers.Contains("Quantity"), "Should have 'Quantity' column");
            }
        }
        finally
        {
            // Cleanup - delete temp file
            if (File.Exists(tempFilePath))
            {
                try
                {
                    File.Delete(tempFilePath);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }

    /// <summary>
    /// Tests that ExportToExcelAsync handles null/empty transaction list gracefully.
    /// </summary>
    [TestMethod]
    public async Task ExportToExcelAsync_EmptyTransactions_CreatesFileWithHeadersOnly()
    {
        // Arrange
        var viewModel = new TransactionViewModel();
        var transactions = new List<Model_Transactions>(); // Empty list
        var tempFilePath = Path.Combine(Path.GetTempPath(), $"TransactionExport_Empty_{Guid.NewGuid()}.xlsx");

        try
        {
            // Act
            var result = await viewModel.ExportToExcelAsync(tempFilePath, transactions);

            // Assert
            Assert.IsTrue(result.IsSuccess, $"Expected success, got failure: {result.ErrorMessage}");
            Assert.IsTrue(File.Exists(tempFilePath), "Excel file should exist even with no data");
            
            // Verify file has headers only
            using (var workbook = new ClosedXML.Excel.XLWorkbook(tempFilePath))
            {
                var worksheet = workbook.Worksheets.First();
                var usedRowCount = worksheet.LastRowUsed()?.RowNumber() ?? 0;
                Assert.AreEqual(1, usedRowCount, "Should have only header row with no data");
            }
        }
        finally
        {
            if (File.Exists(tempFilePath))
            {
                try { File.Delete(tempFilePath); } catch { }
            }
        }
    }

    #endregion
}
