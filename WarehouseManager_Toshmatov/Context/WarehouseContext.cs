using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using WarehouseManager_Toshmatov.Classes.Database;
using WarehouseManager_Toshmatov.Models;

namespace WarehouseManager_Toshmatov.Context
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.ConnectionConfig, mysqlOptions =>
            {
                mysqlOptions.ServerVersion(new Version(8, 0, 11), ServerType.MySql);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настроим связи
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Supplier)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SupplierId);

            // Добавление поставщиков
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "ООО ПродуктСнаб", ContactPerson = "Иванов И.И.", Phone = "+7(495)123-45-67", Email = "info@productsnab.ru" },
                new Supplier { Id = 2, Name = "АгроТрейд", ContactPerson = "Петров П.П.", Phone = "+7(495)234-56-78", Email = "sales@agrotrade.ru" },
                new Supplier { Id = 3, Name = "Мясной Двор", ContactPerson = "Сидоров С.С.", Phone = "+7(495)345-67-89", Email = "order@meatyard.ru" }
            );

            // Добавление заказы
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, ProductName = "Молоко 1л", Quantity = 100, Price = 65.5m, OrderDate = DateTime.Now.AddDays(-5), IsCompleted = false, SupplierId = 1 },
                new Order { Id = 2, ProductName = "Хлеб ржаной", Quantity = 200, Price = 45.0m, OrderDate = DateTime.Now.AddDays(-3), IsCompleted = true, SupplierId = 1 },
                new Order { Id = 3, ProductName = "Картофель 1кг", Quantity = 500, Price = 35.0m, OrderDate = DateTime.Now.AddDays(-2), IsCompleted = false, SupplierId = 2 },
                new Order { Id = 4, ProductName = "Говядина 1кг", Quantity = 150, Price = 450.0m, OrderDate = DateTime.Now.AddDays(-1), IsCompleted = false, SupplierId = 3 }
            );
        }
    }
}