using System;
using System.Windows.Input;

namespace ProductManager.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        public RelayCommand(Action execute) => _execute = execute;
        public bool CanExecute(object p) => true;
        public void Execute(object p) => _execute();
        public event EventHandler CanExecuteChanged;
    }
}
