using ProductManager.Commands;
using ProductManager.Data;
using ProductManager.Models;
using ProductManager.Views;
using System.Collections.ObjectModel;

namespace ProductManager.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; }

        public RelayCommand ShowReportCommand { get; }

        public ProductViewModel()
        {
            using (var db = new AppDbContext())
                Products = new ObservableCollection<Product>(db.Products);

            ShowReportCommand = new RelayCommand(() =>
            {
                new ReportWindow("Product").ShowDialog();
            });
        }
    }

}
