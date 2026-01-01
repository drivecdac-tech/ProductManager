using DevExpress.Xpf.Core;
using DevExpress.XtraReports.UI;
using ProductManager.Reports;
using System.Windows;

namespace ProductManager.Views
{
    public partial class ReportWindow : DXWindow
    {
        public ReportWindow(string reportType)
        {
            InitializeComponent();

            XtraReport report = new ProductReport();
            report.CreateDocument();

            Preview.DocumentSource = report;
        }

    }
}
