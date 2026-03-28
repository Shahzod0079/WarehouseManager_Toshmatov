using WarehouseManager_Toshmatov.Classes;
using System;
using System.Windows;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseManager_Toshmatov.ViewModels;

namespace WarehouseManager_Toshmatov.Models
{
    public class Order : Notification
    {
        public int Id { get; set; }

        private string productName;
        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                OnPropertyChanged("ProductName");
                OnPropertyChanged("TotalAmount");
            }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
                OnPropertyChanged("TotalAmount");
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
                OnPropertyChanged("TotalAmount");
            }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                orderDate = value;
                OnPropertyChanged("OrderDate");
            }
        }

        public decimal TotalAmount
        {
            get { return Quantity * Price; }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                OnPropertyChanged("IsCompleted");
                OnPropertyChanged("StatusText");
            }
        }

        public string StatusText
        {
            get { return IsCompleted ? "Выполнен" : "В обработке"; }
        }

        private int? supplierId;
        public int? SupplierId
        {
            get { return supplierId; }
            set
            {
                supplierId = value;
                OnPropertyChanged("SupplierId");
            }
        }

        private Supplier supplier;
        public virtual Supplier Supplier
        {
            get { return supplier; }
            set
            {
                supplier = value;
                OnPropertyChanged("Supplier");
            }
        }

        //  редактирования
        private bool isEnable;
        [NotMapped]
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";
            }
        }

        [NotMapped]
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsEnable = !IsEnable;

                    if (!IsEnable)
                    {
                        var vm = MainWindow.init.DataContext as VM_Pages;
                        vm?.vm_Warehouse.dbContext.SaveChanges();
                    }
                });
            }
        }

        // изменения статуса
        [NotMapped]
        public RealyCommand OnToggleStatus
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsCompleted = !IsCompleted;

                    var vm = MainWindow.init.DataContext as VM_Pages;
                    vm?.vm_Warehouse.dbContext.SaveChanges();
                });
            }
        }

        // удаления
        [NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить заказ?",
                        "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var vm = MainWindow.init.DataContext as VM_Pages;
                        if (vm != null)
                        {
                            vm.vm_Warehouse.Orders.Remove(this);
                            vm.vm_Warehouse.dbContext.Orders.Remove(this);
                            vm.vm_Warehouse.dbContext.SaveChanges();
                            vm.vm_Warehouse.UpdateTotalSum();
                        }
                    }
                });
            }
        }
    }
}