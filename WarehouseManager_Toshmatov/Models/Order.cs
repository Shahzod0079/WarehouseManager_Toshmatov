using System;
using WarehouseManager_Toshmatov.Classes;

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
                OnPropertyChanged("TotalAmout");
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
            get { return Quantity * Price;  }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                OnPropertyChanged("isCompleted");
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
        public Supplier Supplier
        {
            get { return supplier; }
            set
            {
                supplier = value;
                OnPropertyChanged("Supplier");
            }
        }

    }
}
