using ProductManager.Models;
using ProductManager.Security;
using ProductManager.Views;
using System;
using System.Diagnostics;
using System.Windows;

namespace ProductManager
{
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var hash = PasswordHasher.Hash("admin123");
            Debug.WriteLine(hash);

            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
