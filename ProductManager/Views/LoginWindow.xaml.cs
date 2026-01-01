using ProductManager.Services;
using System.Windows;

namespace ProductManager.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var authService = new AuthService();

            var user = authService.Login(
                txtUsername.Text,
                txtPassword.Password
            );

            if (user == null)
            {
                MessageBox.Show("Invalid username or password",
                                "Login Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            // Optional: store current user
            App.CurrentUser = user;

            var mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

    }
}
