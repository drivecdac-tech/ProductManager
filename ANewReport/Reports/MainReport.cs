using ANewReport.Data;
using ANewReport.Models;
using ANewReport.Reports;
using DevExpress.Drawing;
using DevExpress.Drawing.Printing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class MainReport : XtraReport
{
    public MainReport(DateTime? fromDate, DateTime? toDate)
    {
        PaperKind = DXPaperKind.A4;
        Margins = new DXMargins(40, 40, 40, 40);

        List<Employee> employees;
        List<User> users;

        using (var db = new AppDbContext())
        {
            employees = db.Employees.ToList();
            users = db.Users.ToList();
        }

        float printableWidth = PageWidth - Margins.Left - Margins.Right;

        var mainBand = new ReportHeaderBand
        {
            CanGrow = true,
            HeightF = 10
        };

        var empSub = new XRSubreport
        {
            ReportSource = new EmployeeReport(employees),
            WidthF = printableWidth,
            HeightF = 10,
            CanGrow = true,
            LocationF = new PointF(0, 0)
        };

        var userSub = new XRSubreport
        {
            ReportSource = new UserReport(users),
            WidthF = printableWidth,
            HeightF = 10,
            CanGrow = true,
            LocationF = new PointF(0, 50)
        };

        mainBand.Controls.Add(empSub);
        mainBand.Controls.Add(userSub);

        Bands.Add(mainBand);
    }
}