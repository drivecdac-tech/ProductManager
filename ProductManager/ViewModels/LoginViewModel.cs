using ProductManager.Commands;
using ProductManager.Services;
using System.Windows;

namespace ProductManager.ViewModels
{
    public class LoginViewModel
    {
        private readonly AuthService _authService;
        private readonly WindowService _windowService;

        public string Username { get; set; }
        public string Password { get; set; }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthService();
            _windowService = new WindowService();
            LoginCommand = new RelayCommand(Login);
        }
        private void Login()
        {
            var user = _authService.Login(Username, Password);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }

            App.CurrentUser = user;
            _windowService.ShowMainAndCloseLogin();
        }
    }
}

