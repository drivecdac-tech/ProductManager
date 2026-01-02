using ANewReport.Services;
using ANewReport.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ANewReport.Views
{
    public partial class LoginWindow : Window,IWindowService
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext= new LoginViewModel(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = pwdBox.Password;
            }
        }
    }
}
