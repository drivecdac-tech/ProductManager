using DevExpress.Xpf.Core;
using ProductManager.Reports;
using System;
using System.Windows.Controls;

namespace ProductManager.Views
{
    public partial class ReportWindow : DXWindow
    {
        public ReportWindow()
        {
            InitializeComponent();
            LoadReport(null, null);
        }
        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadReport(dpFrom.SelectedDate, dpTo.SelectedDate);
        }

        private void LoadReport(DateTime? from, DateTime? to)
        {
            var report = new ProductReport(from, to);
            report.CreateDocument();
            Preview.DocumentSource = report;
        }
    }
}