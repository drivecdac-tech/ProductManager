using System.Windows;

namespace ProductManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenReports_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow("Product");
            reportWindow.ShowDialog();
        }
    }
}
