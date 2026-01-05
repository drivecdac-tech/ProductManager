using ANewReport.Reports;
using DevExpress.Xpf.Core;
using System;
using System.Windows.Controls;

namespace ANewReport.Views
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
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
            var report = new MainReport(from, to);
            report.CreateDocument();
            Preview.DocumentSource = report;
        }
    }
}
