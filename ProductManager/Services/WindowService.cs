using ProductManager.Views;
using System.Windows;

namespace ProductManager.Services
{
    public class WindowService
    {
        public void ShowMainAndCloseLogin()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Close login window safely
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext is ViewModels.LoginViewModel)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
