using ANewReport.Models;
using DevExpress.Drawing;
using DevExpress.Drawing.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANewReport.Reports
{
    public class UserReport : XtraReport
    {
        public UserReport(List<User> users)
        {
            DataSource = users;
            PaperKind = DXPaperKind.A4;
            Margins = new DXMargins(30, 30, 30, 30);
            var reportHeader = new ReportHeaderBand { HeightF = 80 };
            var pageHeader = new PageHeaderBand();
            var detailBand = new DetailBand();

            if (!users.Any())
                detailBand.Visible = false;

            Bands.AddRange(new Band[] { reportHeader, pageHeader, detailBand });
            float contentWidth = PageWidth - Margins.Left - Margins.Right - 20;

            reportHeader.Controls.Add(new XRLabel
            {
                Text = "User Report",
                Font = new Font("Arial", 16, FontStyle.Bold),
                BoundsF = new RectangleF(0, 0, contentWidth, 40),
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
                //row.Cells.Add(CreateCell("Password ", null, true));
            }
            else
            {
                row.Cells.Add(CreateCell("", null, false, true));
                row.Cells.Add(CreateCell("", "[Username]", false));
                //row.Cells.Add(CreateCell("", "[PasswordHash]", false));
            }
            table.Rows.Add(row);
            row.Cells[0].WidthF = width * 0.08f;
            row.Cells[1].WidthF = width * 0.24f;
            //row.Cells[2].WidthF = width * 0.20f;
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

