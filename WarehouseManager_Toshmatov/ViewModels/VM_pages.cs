using WarehouseManager_Toshmatov.Classes;
using WarehouseManager_Toshmatov.Views;
using System.Windows;
using System;

namespace WarehouseManager_Toshmatov.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Warehouse vm_Warehouse = new VM_Warehouse();

        public VM_Pages()
        {
            try
            {
                if (MainWindow.init == null)
                {
                    MessageBox.Show("MainWindow.init = null");
                    return;
                }

                if (MainWindow.init.frame == null)
                {
                    MessageBox.Show("MainWindow.init.frame = null");
                    return;
                }

                MainWindow.init.frame.Navigate(new Main(vm_Warehouse));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
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