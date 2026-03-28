using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WarehouseManager_Toshmatov.Classes;
using WarehouseManager_Toshmatov.Context;
using WarehouseManager_Toshmatov.Models;

namespace WarehouseManager_Toshmatov.ViewModels
{
    public class VM_Warehouse : Notification
    {
        public WarehouseContext dbContext = new WarehouseContext();
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }

        private ObservableCollection<Order> allOrders;
        private string searchText;
        private Supplier selectedSupplier;
        private string totalOrdersSum;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                ApplyFilters();
            }
        }

        public Supplier SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                OnPropertyChanged("SelectedSupplier");
                ApplyFilters();
            }
        }
        public string TotalOrdersSum
        {
            get { return totalOrdersSum; }
            set
            {
                totalOrdersSum = value;
                OnPropertyChanged("TotalOrdersSum");
            }
        }

        public VM_Warehouse()
        {
            dbContext.Database.EnsureCreated();

            //Загружаем данные с учетом свзяей
            allOrders = new ObservableCollection<Order>(
                .Include(object => o.OrderDate)
                .OrderByDescending(o => o.OrderDate)
                .ToList());

            Orders = new ObservableCollection<Order>(allOrders);

            //Загружаем поставщиков
            Suppliers = new ObservableCollection<Supplier>(
                dbContext.Suppliers.OrderBy(s => s.Name).ToList());

            //Расчет общей суммы заказов
            UpdateTotalSum();
        }

        private void ApplyFilters()
        {
            var filtered = allOrders.AsEnumerable();

            //Фильтрируем по названию продукта
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(o =>
                o.ProductName != null &&
                o.ProductName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) => 0); 
            }

            //Фильтрируем по поставщику
            if (selectedSupplier != null)
            {
                filtered = filtered.Where(o => o.SupplierId == selectedSupplier.SupplierId);
            }

            Orders.Clear();
            foreach (var o in filtered)
            {
                Orders.Add(order);
            }

            UpdateTotalSum();
        }

        //Обновляем общую сумму (агрегатная функция)
        private void UpdateTotalSum()
        {
            var total = Orders.Sum(o => o.TotalAmount);
            TotalOrdersSum = $"Общая сумма заказов:  {total:C}";
        }
        //Команда добавления заказа 
        public RealyCommand OnAddOrder
        {
            get
            {
                return new RealyCommand(object =>
                {
                    try
                    {
                        Orders newOrder = new Prder()
                        {
                            Orderdate = DateTime.Now,
                            IsCompleted = false,
                        };

                        Orders.Add(newOrder);
                        allOrders.Add(newOrder);
                        dbContext.Orders.Add(newOrder);
                        dbContext.SaveChanges();
                        UpdateTotalSum();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException?.Message ?? ex.Message,);
                    }
                });
            }
        }

        //Сброс фильтров 
        public RealyCommand OnResetFilters
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    SearchText = string.Empty;
                    SelectedSupplier = null;
                });
            }
        }
    }
}