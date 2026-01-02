using ANewReport.Services;
using System.Windows;

namespace ANewReport.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var authservice= new AuthService();
            var user = authservice.Login(txtUsername.Text, txtPassword.Password);
            if(user != null)
            {
                App.CurrentUser = user;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.","Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
