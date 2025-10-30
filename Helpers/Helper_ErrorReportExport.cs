using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Helpers
{
    /// <summary>
    /// Helper class for exporting error report data to various file formats.
    /// Supports CSV and Excel (XLSX) export with proper UTF-8 encoding and special character handling.
    /// </summary>
    public static class Helper_ErrorReportExport
    {
        #region CSV Export

        /// <summary>
        /// Exports a DataTable to CSV format with UTF-8 encoding and proper field escaping.
        /// </summary>
        /// <param name="dataTable">The DataTable containing error report data to export.</param>
        /// <param name="filePath">The full file path where the CSV file should be saved.</param>
        /// <returns>True if export succeeded, false if an error occurred.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dataTable or filePath is null.</exception>
        /// <exception cref="IOException">Thrown when file cannot be written.</exception>
        public static async Task<bool> ExportToCsvAsync(DataTable dataTable, string filePath)
        {
            ArgumentNullException.ThrowIfNull(dataTable);
            ArgumentNullException.ThrowIfNull(filePath);

            try
            {
                var csv = new StringBuilder();

                // Write header row
                var headers = new List<string>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    headers.Add(EscapeCsvField(column.ColumnName));
                }
                csv.AppendLine(string.Join(",", headers));

                // Write data rows
                foreach (DataRow row in dataTable.Rows)
                {
                    var fields = new List<string>();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        var value = row[column]?.ToString() ?? string.Empty;
                        fields.Add(EscapeCsvField(value));
                    }
                    csv.AppendLine(string.Join(",", fields));
                }

                // Write to file with UTF-8 BOM for Excel compatibility
                await File.WriteAllTextAsync(filePath, csv.ToString(), new UTF8Encoding(true));

                LoggingUtility.LogApplicationInfo($"[Export] Successfully exported {dataTable.Rows.Count} error reports to CSV: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                LoggingUtility.LogApplicationInfo($"[Export] Failed to export error reports to CSV: {filePath}");
                return false;
            }
        }

        /// <summary>
        /// Escapes a CSV field by wrapping in quotes if it contains commas, newlines, or quotes.
        /// Internal quotes are doubled per CSV standard (RFC 4180).
        /// </summary>
        /// <param name="field">The field value to escape.</param>
        /// <returns>Escaped field value suitable for CSV format.</returns>
        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return string.Empty;

            // Check if field needs escaping (contains comma, quote, or newline)
            if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
            {
                // Escape quotes by doubling them
                field = field.Replace("\"", "\"\"");
                // Wrap in quotes
                return $"\"{field}\"";
            }

            return field;
        }

        #endregion

        #region Excel Export

        /// <summary>
        /// Exports a DataTable to Excel (XLSX) format using ClosedXML.
        /// Applies basic formatting: bold headers, freeze panes, auto-fit columns.
        /// </summary>
        /// <param name="dataTable">The DataTable containing error report data to export.</param>
        /// <param name="filePath">The full file path where the Excel file should be saved.</param>
        /// <returns>True if export succeeded, false if an error occurred.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dataTable or filePath is null.</exception>
        /// <exception cref="IOException">Thrown when file cannot be written.</exception>
        public static async Task<bool> ExportToExcelAsync(DataTable dataTable, string filePath)
        {
            ArgumentNullException.ThrowIfNull(dataTable);
            ArgumentNullException.ThrowIfNull(filePath);

            try
            {
                await Task.Run(() =>
                {
                    using (var workbook = new XLWorkbook())
                    {
                        // Add worksheet with data
                        var worksheet = workbook.Worksheets.Add("Error Reports");
                        
                        // Insert data table
                        var table = worksheet.Cell(1, 1).InsertTable(dataTable);
                        
                        // Format header row
                        var headerRow = table.HeadersRow();
                        headerRow.Style.Font.Bold = true;
                        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                        
                        // Freeze header row
                        worksheet.SheetView.FreezeRows(1);
                        
                        // Auto-fit columns for readability
                        worksheet.Columns().AdjustToContents();
                        
                        // Limit max column width to prevent overly wide columns
                        foreach (var column in worksheet.ColumnsUsed())
                        {
                            if (column.Width > 50)
                                column.Width = 50;
                        }
                        
                        // Save workbook
                        workbook.SaveAs(filePath);
                    }
                });

                LoggingUtility.LogApplicationInfo($"[Export] Successfully exported {dataTable.Rows.Count} error reports to Excel: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                LoggingUtility.LogApplicationInfo($"[Export] Failed to export error reports to Excel: {filePath}");
                return false;
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Creates a filtered DataTable containing only the selected rows from the source DataTable.
        /// Used for exporting selected reports rather than all filtered reports.
        /// </summary>
        /// <param name="sourceTable">The source DataTable containing all data.</param>
        /// <param name="selectedRowIndices">Array of row indices to include in the filtered table.</param>
        /// <returns>A new DataTable containing only the selected rows.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sourceTable is null.</exception>
        /// <exception cref="ArgumentException">Thrown when selectedRowIndices is null or empty.</exception>
        public static DataTable CreateFilteredDataTable(DataTable sourceTable, int[] selectedRowIndices)
        {
            ArgumentNullException.ThrowIfNull(sourceTable);
            if (selectedRowIndices == null || selectedRowIndices.Length == 0)
                throw new ArgumentException("Selected row indices cannot be null or empty.", nameof(selectedRowIndices));

            // Clone structure
            var filteredTable = sourceTable.Clone();

            // Import selected rows
            foreach (int rowIndex in selectedRowIndices)
            {
                if (rowIndex >= 0 && rowIndex < sourceTable.Rows.Count)
                {
                    filteredTable.ImportRow(sourceTable.Rows[rowIndex]);
                }
            }

            return filteredTable;
        }

        #endregion
    }
}
