using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ANewReport.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string p = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

    }
}
