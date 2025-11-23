using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    public partial class ViewReleaseNotesForm : ThemedForm
    {
        private readonly string _markdownContent;

        public ViewReleaseNotesForm(string markdownContent)
        {
            InitializeComponent();
            _markdownContent = markdownContent;
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView21.EnsureCoreWebView2Async();
                DisplayMarkdown();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }

        private void DisplayMarkdown()
        {
            string htmlContent = ConvertMarkdownToHtml(_markdownContent);
            webView21.NavigateToString(htmlContent);
        }

        private string ConvertMarkdownToHtml(string markdown)
        {
            // Basic Markdown to HTML conversion
            // This is a simplified converter for the specific format of RELEASE_NOTES_USER_FRIENDLY.md
            
            string html = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body { font-family: 'Segoe UI', sans-serif; line-height: 1.6; padding: 20px; color: #333; }
        h1 { color: #0078d4; border-bottom: 1px solid #eee; padding-bottom: 10px; }
        h2 { color: #0078d4; margin-top: 30px; }
        h3 { color: #2b88d8; }
        blockquote { border-left: 4px solid #0078d4; margin: 0; padding-left: 15px; color: #666; background-color: #f9f9f9; padding: 10px; }
        table { border-collapse: collapse; width: 100%; margin: 20px 0; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #f2f2f2; color: #333; }
        tr:nth-child(even) { background-color: #f9f9f9; }
        code { background-color: #f4f4f4; padding: 2px 5px; border-radius: 3px; font-family: Consolas, monospace; }
        pre { background-color: #f4f4f4; padding: 15px; border-radius: 5px; overflow-x: auto; }
        hr { border: 0; height: 1px; background: #ddd; margin: 30px 0; }
        a { color: #0078d4; text-decoration: none; }
        a:hover { text-decoration: underline; }
    </style>
</head>
<body>
";
            
            using (StringReader reader = new StringReader(markdown))
            {
                string? line;
                bool inTable = false;
                bool inCodeBlock = false;

                while ((line = reader.ReadLine()) != null)
                {
                    string processedLine = line;

                    // Handle Code Blocks
                    if (processedLine.StartsWith("```"))
                    {
                        if (inCodeBlock)
                        {
                            html += "</pre>\n";
                            inCodeBlock = false;
                        }
                        else
                        {
                            html += "<pre>\n";
                            inCodeBlock = true;
                        }
                        continue;
                    }

                    if (inCodeBlock)
                    {
                        html += System.Web.HttpUtility.HtmlEncode(processedLine) + "\n";
                        continue;
                    }

                    // Handle Headers
                    if (processedLine.StartsWith("# "))
                        html += $"<h1>{System.Web.HttpUtility.HtmlEncode(processedLine.Substring(2))}</h1>\n";
                    else if (processedLine.StartsWith("## "))
                        html += $"<h2>{System.Web.HttpUtility.HtmlEncode(processedLine.Substring(3))}</h2>\n";
                    else if (processedLine.StartsWith("### "))
                        html += $"<h3>{System.Web.HttpUtility.HtmlEncode(processedLine.Substring(4))}</h3>\n";
                    else if (processedLine.StartsWith("#### "))
                        html += $"<h4>{System.Web.HttpUtility.HtmlEncode(processedLine.Substring(5))}</h4>\n";
                    
                    // Handle Blockquotes
                    else if (processedLine.StartsWith("> "))
                        html += $"<blockquote>{System.Web.HttpUtility.HtmlEncode(processedLine.Substring(2))}</blockquote>\n";
                    
                    // Handle Horizontal Rules
                    else if (processedLine.StartsWith("---"))
                        html += "<hr />\n";
                    
                    // Handle Tables
                    else if (processedLine.StartsWith("|"))
                    {
                        if (!inTable)
                        {
                            html += "<table>\n";
                            inTable = true;
                            // Assume first row is header
                            html += "<tr>";
                            foreach (var cell in processedLine.Split('|', StringSplitOptions.RemoveEmptyEntries))
                            {
                                html += $"<th>{System.Web.HttpUtility.HtmlEncode(cell.Trim())}</th>";
                            }
                            html += "</tr>\n";
                        }
                        else if (processedLine.Contains("---"))
                        {
                            // Skip separator line
                            continue;
                        }
                        else
                        {
                            html += "<tr>";
                            foreach (var cell in processedLine.Split('|', StringSplitOptions.RemoveEmptyEntries))
                            {
                                html += $"<td>{System.Web.HttpUtility.HtmlEncode(cell.Trim())}</td>";
                            }
                            html += "</tr>\n";
                        }
                    }
                    else
                    {
                        if (inTable)
                        {
                            html += "</table>\n";
                            inTable = false;
                        }

                        if (string.IsNullOrWhiteSpace(processedLine))
                        {
                            html += "<br />\n";
                        }
                        else
                        {
                            // Handle Bold
                            processedLine = System.Text.RegularExpressions.Regex.Replace(processedLine, @"\*\*(.*?)\*\*", "<b>$1</b>");
                            
                            // Handle Links [text](url)
                            processedLine = System.Text.RegularExpressions.Regex.Replace(processedLine, @"\[(.*?)\]\((.*?)\)", "<a href=\"$2\">$1</a>");

                            html += $"<p>{processedLine}</p>\n";
                        }
                    }
                }
                
                if (inTable) html += "</table>\n";
            }

            html += "</body></html>";
            return html;
        }
    }
}
