using ANewReport.Models;
using DevExpress.Drawing;
using DevExpress.Drawing.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ANewReport.Reports
{
    public class EmployeeReport : XtraReport
    {
        public EmployeeReport(List<Employee> employees)
        {
            DataSource = employees;
            PaperKind = DXPaperKind.A4;
            Margins = new DXMargins(30, 30, 30, 30);
            var reportHeader = new ReportHeaderBand { HeightF = 80 };
            var pageHeader = new PageHeaderBand();
            var detailBand = new DetailBand();

            if (!employees.Any())
                detailBand.Visible = false;

            Bands.AddRange(new Band[] { reportHeader, pageHeader, detailBand });// Add bands to the report
            float contentWidth = PageWidth - Margins.Left - Margins.Right-20;

            reportHeader.Controls.Add(new XRLabel
            {
                Text = "Employee Report",
                Font = new Font("Arial", 16, FontStyle.Bold),
                BoundsF = new RectangleF(0, 0, contentWidth, 40),
                TextAlignment = TextAlignment.MiddleCenter
            });

            reportHeader.Controls.Add(new XRLabel
            {
                Text = $"Generated on {DateTime.Now:dd-MMM-yyyy hh:mm tt}",
                Font = new Font("Arial", 10, FontStyle.Italic),
                BoundsF = new RectangleF(0, 40, contentWidth, 20),
                TextAlignment = TextAlignment.MiddleRight,
                CanGrow = false,
                WordWrap = false
            });

            // 5. Page header (column headers)
            var headerTable = CreateTable(isHeader: true);
            pageHeader.Controls.Add(headerTable);
            pageHeader.HeightF = headerTable.HeightF;

            // 6. Detail band (data rows)
            var detailTable = CreateTable(isHeader: false);
            detailBand.Controls.Add(detailTable);
            detailBand.HeightF = detailTable.HeightF;
        }
        private XRTable CreateTable(bool isHeader)
        {
            float width = PageWidth - Margins.Left - Margins.Right;

            var table = new XRTable
            {
                WidthF = width,
                Borders = BorderSide.All
            };

            var row = new XRTableRow { HeightF = 30 };

            if (isHeader)
            {
                row.Cells.Add(CreateCell("Sr No", null, true));
                row.Cells.Add(CreateCell("Name", null, true));
                row.Cells.Add(CreateCell("City", null, true));
                row.Cells.Add(CreateCell("Hire Date", null, true));
                row.Cells.Add(CreateCell("Salary", null, true));
            }
            else
            {
                row.Cells.Add(CreateCell("", null, false, true));
                row.Cells.Add(CreateCell("", "[Name]", false));
                row.Cells.Add(CreateCell("", "[City]", false));
                row.Cells.Add(CreateCell("", "FormatString('{0:dd-MM-yyyy}', [HireDate])", false));
                row.Cells.Add(CreateCell("", "FormatString('{0:C}', [Salary])", false));
            }

            table.Rows.Add(row);

            // ✔ FIX: Set column widths
            row.Cells[0].WidthF = width * 0.08f;
            row.Cells[1].WidthF = width * 0.24f;
            row.Cells[2].WidthF = width * 0.20f;
            row.Cells[3].WidthF = width * 0.18f;
            row.Cells[4].WidthF = width * 0.20f;

            return table; // ❌ NO AdjustSize()
        }



        private XRTableCell CreateCell(string header, string binding, bool isHeader, bool isSerial = false)
        {
            var cell = new XRTableCell
            {
                Text = isHeader ? header : "",
                Font = isHeader ? new Font("Arial", 10, FontStyle.Bold) : new Font("Arial", 10, FontStyle.Regular),
                BackColor = isHeader ? Color.LightGray : Color.Transparent,
                Padding = new PaddingInfo(5, 5, 0, 0),
                Borders = BorderSide.All,
                TextAlignment = TextAlignment.MiddleLeft
            };
            if (isHeader)
            {
                cell.TextAlignment = TextAlignment.MiddleCenter;
                return cell;
            }
            if (isSerial)
            {
                cell.TextAlignment = TextAlignment.MiddleCenter;
                cell.Summary = new XRSummary
                {
                    Func = SummaryFunc.RecordNumber,
                    Running = SummaryRunning.Report
                };
            }
            else
            {
                cell.ExpressionBindings.Add(
                    new ExpressionBinding("BeforePrint", "Text", binding));
            }
            return cell;
        }

        }

    }

    /*
    public class EmployeeReport : XtraReport
    {
        public EmployeeReport(DateTime? fromDate, DateTime? toDate)
        {
            List<Employee> employees;
            using (var db = new Data.AppDbContext())
            {
                var query = db.Employees.AsQueryable();
                if (fromDate.HasValue)
                    query = query.Where(e => e.HireDate >= fromDate.Value);
                if (toDate.HasValue)
                    query = query.Where(e => e.HireDate <= toDate.Value);
                employees = query.ToList();
            }
            this.DataSource = employees;
            PaperKind = DXPaperKind.A4;
            Margins = new DXMargins(40, 40, 40, 40);
            var reportHeader = new ReportHeaderBand { HeightF = 80 };
            var pageHeader = new PageHeaderBand();
            var detailBand = new DetailBand();

            if (!employees.Any())
                detailBand.Visible = false;

            Bands.AddRange(new Band[] { reportHeader, pageHeader, detailBand });// Add bands to the report
            float contentWidth = PageWidth - Margins.Left - Margins.Right;

            reportHeader.Controls.Add(new XRLabel
            {
                Text = "Employee Report",
                Font = new Font("Arial", 16, FontStyle.Bold),
                BoundsF = new RectangleF(0, 0, contentWidth, 40),
                TextAlignment = TextAlignment.MiddleCenter
            });

            reportHeader.Controls.Add(new XRLabel
            {
                Text = $"Generated on {DateTime.Now:dd-MMM-yyyy hh:mm tt}",
                Font = new Font("Arial", 10, FontStyle.Italic),
                BoundsF = new RectangleF(0, 40, contentWidth-10, 20),
                TextAlignment = TextAlignment.MiddleRight,
                CanGrow = false,
                WordWrap = false
            });

            string filterText = "Complete Report";
            if (fromDate.HasValue || toDate.HasValue)
            {
                filterText = $"From: {fromDate:dd-MM-yyyy} To: {toDate:dd-MM-yyyy}";
            }
            reportHeader.Controls.Add(new XRLabel
            {
                Text = filterText,
                Font = new Font("Arial", 10, FontStyle.Italic),
                BoundsF = new RectangleF(0, 60, contentWidth, 20),
                TextAlignment = TextAlignment.MiddleCenter
            });

            // 5. Page header (column headers)
            var headerTable = CreateTable(isHeader: true);
            pageHeader.Controls.Add(headerTable);
            pageHeader.HeightF = headerTable.HeightF;

            // 6. Detail band (data rows)
            var detailTable = CreateTable(isHeader: false);
            detailBand.Controls.Add(detailTable);
            detailBand.HeightF = detailTable.HeightF;
        }

        private XRTable CreateTable(bool isHeader)
        {
            float width = PageWidth - Margins.Left - Margins.Right;

            var table = new XRTable
            {
                WidthF = width,
                Borders = BorderSide.All
            };

            var row = new XRTableRow { HeightF = 30 };

            if (isHeader)
            {
                row.Cells.Add(CreateCell("Sr No", null, true));
                row.Cells.Add(CreateCell("Name", null, true));
                row.Cells.Add(CreateCell("City", null, true));
                row.Cells.Add(CreateCell("Hire Date", null, true));
                row.Cells.Add(CreateCell("Salary", null, true));
            }
            else
            {
                row.Cells.Add(CreateCell("", null, false, true)); // serial
                row.Cells.Add(CreateCell("", "[Name]", false));
                row.Cells.Add(CreateCell("", "[City]", false));
                row.Cells.Add(CreateCell("", "FormatString('{0:dd-MM-yyyy}', [HireDate])", false));
                row.Cells.Add(CreateCell("", "FormatString('{0:C}', [Salary])", false));
            }

            table.Rows.Add(row);
            table.AdjustSize();
            return table;
        }


        private XRTableCell CreateCell(string header, string binding, bool isHeader, bool isSerial = false)
        {
            var cell = new XRTableCell
            {
                Text = isHeader ? header : "",
                Font = isHeader ? new Font("Arial", 10, FontStyle.Bold) : new Font("Arial", 10, FontStyle.Regular),
                BackColor = isHeader ? Color.LightGray : Color.Transparent,
                Padding = new PaddingInfo(5, 5, 0, 0),
                Borders = BorderSide.All,
                TextAlignment = TextAlignment.MiddleLeft
            };
            if (isHeader)
            {
                cell.TextAlignment = TextAlignment.MiddleCenter;
                return cell;
            }
            if (isSerial)
            {
                cell.TextAlignment = TextAlignment.MiddleCenter;
                cell.Summary = new XRSummary
                {
                    Func = SummaryFunc.RecordNumber,
                    Running = SummaryRunning.Report
                };
            }
            else
            {
                cell.ExpressionBindings.Add(
                    new ExpressionBinding("BeforePrint", "Text", binding));
            }
            return cell;
        }
    }
}
    */