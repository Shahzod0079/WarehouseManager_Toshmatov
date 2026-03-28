using System.Windows.Controls;
using WarehouseManager_Toshmatov.ViewModels;

namespace WarehouseManager_Toshmatov.Views
{
    public partial class Main : Page
    {
        public Main(VM_Warehouse vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}