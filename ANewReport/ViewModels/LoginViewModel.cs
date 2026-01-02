using ANewReport.Commands;
using ANewReport.Services;
using ANewReport.Views;
using System.Windows;
using System.Windows.Input;

namespace ANewReport.ViewModels
{
    public class LoginViewModel
    {
        private readonly IWindowService _windowService;
        private readonly AuthService _authService;
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; }
        public LoginViewModel(IWindowService windowService)
        {
            _windowService = windowService;
            _authService = new AuthService();
            LoginCommand = new RelayCommand(Login);
        }
        private void Login()
        {
            var user = _authService.Login(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password",
                                "Login Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }
            App.CurrentUser = user;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            _windowService.Close();
        }
    }
}
