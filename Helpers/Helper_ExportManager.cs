using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using ClosedXML.Excel;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Lightweight scaffold for export functionality. Concrete implementations (PDF/Excel) will be
/// implemented in follow-up tasks. This class provides a safe helper to write a ready-made PDF
/// stream to disk and placeholder async methods for higher-level export flows.
/// </summary>
public static class Helper_ExportManager
{
    /// <summary>
    /// Persist a PDF stream to disk asynchronously. Creates parent directory when missing.
    /// </summary>
    public static async Task ExportPdfStreamToFileAsync(Stream sourcePdfStream, string destinationPath, CancellationToken cancellationToken = default)
    {
        if (sourcePdfStream is null) throw new ArgumentNullException(nameof(sourcePdfStream));
        if (string.IsNullOrWhiteSpace(destinationPath)) throw new ArgumentException("destinationPath required", nameof(destinationPath));

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ?? Path.GetTempPath());
            await using var fileStream = File.Create(destinationPath);
            sourcePdfStream.Seek(0, SeekOrigin.Begin);
            await sourcePdfStream.CopyToAsync(fileStream, 81920, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            throw;
        }
    }

    /// <summary>
    /// Renders the supplied <see cref="Model_Print_Job"/> to a PDF file using the Microsoft Print to PDF driver.
    /// </summary>
    /// <param name="printJob">Configured print job describing the desired output.</param>
    /// <param name="destinationPath">Absolute or relative destination for the PDF file.</param>
    /// <param name="cancellationToken">Cancellation token to abort the export before printing begins.</param>
    /// <returns>A <see cref="Model_Dao_Result"/> representing success or failure.</returns>
    public static async Task<Model_Dao_Result> ExportToPdfAsync(Model_Print_Job printJob, string destinationPath, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(printJob);

        if (string.IsNullOrWhiteSpace(destinationPath))
        {
            return Model_Dao_Result.Failure("Destination path is required for PDF export.");
        }

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resolvedPath = Path.GetFullPath(destinationPath);
            string? directory = Path.GetDirectoryName(resolvedPath);
            if (string.IsNullOrWhiteSpace(directory))
            {
                return Model_Dao_Result.Failure("Unable to determine the target directory for the PDF export.");
            }

            Directory.CreateDirectory(directory);

            if (!resolvedPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                resolvedPath = Path.ChangeExtension(resolvedPath, ".pdf") ?? resolvedPath + ".pdf";
            }

            if (File.Exists(resolvedPath))
            {
                File.Delete(resolvedPath);
            }

            string pdfPrinterName = "Microsoft Print to PDF";
            bool printerAvailable = PrinterSettings.InstalledPrinters.Cast<string>()
                .Any(name => string.Equals(name, pdfPrinterName, StringComparison.OrdinalIgnoreCase));

            if (!printerAvailable)
            {
                return Model_Dao_Result.Failure("Microsoft Print to PDF is not available on this system.");
            }

            string? originalPrinter = printJob.PrinterName;
            printJob.PrinterName = pdfPrinterName;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var printManager = new Helper_PrintManager(printJob);
                PrintDocument? document = printManager.PreparePrintDocument();
                if (document is null)
                {
                    return Model_Dao_Result.Failure("Unable to prepare print document for PDF export.");
                }

                document.PrinterSettings.PrintToFile = true;
                document.PrinterSettings.PrintFileName = resolvedPath;
                document.PrintController = new StandardPrintController();

                cancellationToken.ThrowIfCancellationRequested();

                await Task.Run(() =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    document.Print();
                }, cancellationToken).ConfigureAwait(false);

                printManager.SyncPageBoundariesFromPrinter();

                if (!File.Exists(resolvedPath))
                {
                    return Model_Dao_Result.Failure("PDF export completed but the expected file was not created.");
                }

                return Model_Dao_Result.Success($"PDF exported to {resolvedPath}");
            }
            finally
            {
                printJob.PrinterName = originalPrinter;
            }
        }
        catch (OperationCanceledException)
        {
            LoggingUtility.Log("[Helper_ExportManager] PDF export cancelled by caller.");
            return Model_Dao_Result.Failure("PDF export was cancelled.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result.Failure("Failed to export PDF.", ex);
        }
    }

    /// <summary>
    /// Exports the selected page range of the supplied print job to an Excel workbook.
    /// </summary>
    /// <param name="printJob">Configured print job describing the desired output.</param>
    /// <param name="destinationPath">Absolute or relative destination for the Excel file.</param>
    /// <param name="cancellationToken">Cancellation token to abort the export prior to writing the file.</param>
    /// <returns>A <see cref="Model_Dao_Result"/> representing success or failure.</returns>
    public static async Task<Model_Dao_Result> ExportToExcelAsync(Model_Print_Job printJob, string destinationPath, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(printJob);

        if (printJob.Data?.Rows.Count is null or 0)
        {
            return Model_Dao_Result.Failure("There is no data available for export.");
        }

        if (string.IsNullOrWhiteSpace(destinationPath))
        {
            return Model_Dao_Result.Failure("Destination path is required for Excel export.");
        }

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            string resolvedPath = Path.GetFullPath(destinationPath);
            string? directory = Path.GetDirectoryName(resolvedPath);
            if (string.IsNullOrWhiteSpace(directory))
            {
                return Model_Dao_Result.Failure("Unable to determine the target directory for the Excel export.");
            }

            Directory.CreateDirectory(directory);

            if (!resolvedPath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                resolvedPath = Path.ChangeExtension(resolvedPath, ".xlsx") ?? resolvedPath + ".xlsx";
            }

            if (File.Exists(resolvedPath))
            {
                File.Delete(resolvedPath);
            }

            List<string> columnsToExport = printJob.ColumnOrder
                .Where(column => printJob.VisibleColumns.Contains(column))
                .ToList();

            if (columnsToExport.Count == 0)
            {
                return Model_Dao_Result.Failure("No columns are selected for export.");
            }

            (int fromPage, int toPage) = ResolvePageRange(printJob);
            if (fromPage > toPage)
            {
                return Model_Dao_Result.Failure("The specified page range is invalid for Excel export.");
            }

            IReadOnlyList<DataRow> rowsToExport = printJob
                .EnumerateRowsForPageRange(fromPage, toPage)
                .ToList();

            if (rowsToExport.Count == 0)
            {
                return Model_Dao_Result.Failure("No rows fall within the selected page range.");
            }

            await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Export");

                // Header row
                for (int columnIndex = 0; columnIndex < columnsToExport.Count; columnIndex++)
                {
                    worksheet.Cell(1, columnIndex + 1).Value = columnsToExport[columnIndex];
                }

                var headerRange = worksheet.Range(1, 1, 1, columnsToExport.Count);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(230, 230, 230);

                // Data rows
                int currentRow = 2;
                foreach (DataRow dataRow in rowsToExport)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    for (int columnIndex = 0; columnIndex < columnsToExport.Count; columnIndex++)
                    {
                        object? value = dataRow[columnsToExport[columnIndex]];
                        var cell = worksheet.Cell(currentRow, columnIndex + 1);
                        if (value is null || value is DBNull)
                        {
                            cell.SetValue(string.Empty);
                        }
                        else if (value is DateTime dateTime)
                        {
                            cell.SetValue(dateTime);
                        }
                        else if (value is bool boolean)
                        {
                            cell.SetValue(boolean);
                        }
                        else if (value is IFormattable formattable)
                        {
                            cell.SetValue(formattable.ToString(null, CultureInfo.CurrentCulture));
                        }
                        else
                        {
                            cell.SetValue(value.ToString() ?? string.Empty);
                        }
                    }

                    currentRow++;
                }

                var dataRange = worksheet.Range(1, 1, rowsToExport.Count + 1, columnsToExport.Count);
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Columns(1, columnsToExport.Count).AdjustToContents();

                workbook.SaveAs(resolvedPath);
            }, cancellationToken).ConfigureAwait(false);

            return Model_Dao_Result.Success($"Excel exported to {resolvedPath}");
        }
        catch (OperationCanceledException)
        {
            LoggingUtility.Log("[Helper_ExportManager] Excel export cancelled by caller.");
            return Model_Dao_Result.Failure("Excel export was cancelled.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result.Failure("Failed to export Excel file.", ex);
        }
    }

    private static (int fromPage, int toPage) ResolvePageRange(Model_Print_Job printJob)
    {
        int maxBoundaryPage = printJob.PageBoundaries.Count > 0
            ? printJob.PageBoundaries.Max(boundary => boundary.PageNumber)
            : 0;

        int maxPage = Math.Max(
            Math.Max(maxBoundaryPage, printJob.TotalPages),
            1);

        return printJob.PageRangeType switch
        {
            Enum_PrintRangeType.CurrentPage => (Clamp(printJob.CurrentPage, 1, maxPage), Clamp(printJob.CurrentPage, 1, maxPage)),
            Enum_PrintRangeType.PageRange =>
                (Clamp(printJob.FromPage, 1, maxPage), Clamp(printJob.ToPage, Clamp(printJob.FromPage, 1, maxPage), maxPage)),
            _ => (1, maxPage)
        };
    }

    private static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }
}
