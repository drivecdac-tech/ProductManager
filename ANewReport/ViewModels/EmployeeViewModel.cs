using ANewReport.Commands;
using ANewReport.Data;
using ANewReport.Models;
using ANewReport.Views;
using System.Collections.ObjectModel;

namespace ANewReport.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; }
        public RelayCommand LoadEmployeesCommand { get; }

        public EmployeeViewModel()
        {
            using (var db = new AppDbContext())
                Employees = new ObservableCollection<Employee>(db.Employees);
            LoadEmployeesCommand = new RelayCommand(() =>
            {
                new ReportWindow().ShowDialog();
            });
        }
    }
}
