using WarehouseManager_Toshmatov.Classes;
using WarehouseManager_Toshmatov.ViewModels.Views;


namespace WarehouseManager_Toshmatov.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Warehouse vm_Warehouse = new VM_Warehouse();

        public VM_Pages()
        {
            MainWindow.init.frame.Navigate(new Main(vm_Warehouse));
        }

        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.Close();
                });
            }
        }
    }
}