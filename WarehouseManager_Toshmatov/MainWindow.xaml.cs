using System.Windows;
using WarehouseManager_Toshmatov.ViewModels;

namespace WarehouseManager_Toshmatov
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            DataContext = new VM_Pages();
        }
    }
}