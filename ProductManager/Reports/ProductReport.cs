using DevExpress.Drawing.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using ProductManager.Data;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Documents;

namespace ProductManager.Reports
{
    public class ProductReport : XtraReport
    {
        public ProductReport(DateTime? fromDate, DateTime? toDate)
        {
            List<Product> data;

            using (var db = new AppDbContext())
            {
                var query = db.Products.AsQueryable();

                if (fromDate.HasValue)
                    query = query.Where(p => p.CreatedDate >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(p => p.CreatedDate <= toDate.Value);

                data = query.ToList();
            }
            DataSource = data;

            PaperKind = DXPaperKind.A4;
            Margins = new Margins(40, 40, 40, 40);

            var reportHeader = new ReportHeaderBand { HeightF = 80 };
            var pageHeader = new PageHeaderBand { HeightF = 30 };
            var detail = new DetailBand { HeightF = 30 };
            if (!data.Any())
            {
                detail.Visible = false;
            }
            Bands.AddRange(new Band[] { reportHeader, pageHeader, detail });

            reportHeader.Controls.Add(new XRLabel
            {
                Text = "Product Report",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlignment = TextAlignment.MiddleCenter,
                BoundsF = new RectangleF(0, 0,
                PageWidth - Margins.Left - Margins.Right, 40)
            });
            reportHeader.Controls.Add(new XRLabel
            {
                Text = $"Generated on: {DateTime.Now:dd-MMM-yyyy HH:mm}",
                Font = new Font("Segoe UI", 9),
                TextAlignment = TextAlignment.MiddleRight,
                BoundsF = new RectangleF(0, 45,
               PageWidth - Margins.Left - Margins.Right - 10, 20),
                CanGrow = false,
                WordWrap = false
            });
            string filterText = "All Dates";

            if (fromDate.HasValue || toDate.HasValue)
            {
                filterText =
                    $"From: {fromDate:dd-MMM-yyyy}    To: {toDate:dd-MMM-yyyy}";
            }

            reportHeader.Controls.Add(new XRLabel
            {
                Text = filterText,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                TextAlignment = TextAlignment.MiddleCenter,
                BoundsF = new RectangleF(
                    0,
                    65,
                    PageWidth - Margins.Left - Margins.Right,
                    15),
                CanGrow = false
            });

            pageHeader.Controls.Add(CreateTable(isHeader: true));

            detail.Controls.Add(CreateTable(isHeader: false));
        }

        private XRTable CreateTable(bool isHeader)
        {
            float width = PageWidth - Margins.Left - Margins.Right;
            //float startX = (PageWidth - width) / 2;
            float startX = 0;
            var table = new XRTable
            {
                BoundsF = new RectangleF(startX, 0, width, 30),
                Borders = BorderSide.All,
                TextAlignment = TextAlignment.MiddleCenter
            };

            var row = new XRTableRow { HeightF = 30 };

            // Sr No
            row.Cells.Add(CreateCell("Sr No", "sumRecordNumber()", isHeader, true));

            row.Cells.Add(CreateCell("Name", "[Name]", isHeader));
            row.Cells.Add(CreateCell("Price", "[Price]", isHeader));
            row.Cells.Add(CreateCell("Stock", "[Stock]", isHeader));
            row.Cells.Add(CreateCell("Created Date", "FormatString('{0:dd-MMM-yyyy}', [CreatedDate])", isHeader));
            table.Rows.Add(row);
            return table;
        }
        private XRTableCell CreateCell(
            string header,
            string binding,
            bool isHeader,
            bool isSerial = false)
        {
            var cell = new XRTableCell
            {
                Text = isHeader ? header : "",
                Font = new Font("Segoe UI", 10,
                    isHeader ? FontStyle.Bold : FontStyle.Regular),
                BackColor = isHeader ? Color.LightGray : Color.Transparent,
                Padding = new PaddingInfo(5, 5, 0, 0),
                Borders = BorderSide.All
            };

            if (!isHeader)
            {
                cell.ExpressionBindings.Add(
                    new ExpressionBinding("BeforePrint", "Text", binding));

                if (isSerial)
                    cell.Summary = new XRSummary
                    {
                        Func = SummaryFunc.RecordNumber,
                        Running = SummaryRunning.Report
                    };
            }

            return cell;

        }

    }
}


