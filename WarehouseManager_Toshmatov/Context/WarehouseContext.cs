using Microsoft.EntityFrameworkCore;
using System;
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
            optionsBuilder.UseSqlServer(Config.ConnectionConfig);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Supplier)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.SupplierId);

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Данилиха", ContactPerson = "Трефилов Ф.М.", Phone = "+7 (342) 248-04-28", Email = "daniliha.ru" },
                new Supplier { Id = 2, Name = "Лилия Опт", ContactPerson = "Сабитова Л.Р.", Phone = "+7 (342) 285-56-40", Email = "tpk59.ru" },
                new Supplier { Id = 3, Name = "Областная продовольственная компания", ContactPerson = "Бусаров А.Н.", Phone = "+7 (342) 285-55-25.", Email = "mail.ru " }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, ProductName = "Молоко ", Quantity = 100, Price = 65.5m, OrderDate = DateTime.Now.AddDays(-5), IsCompleted = false, SupplierId = 1 },
                new Order { Id = 2, ProductName = "Хлеб ржаной", Quantity = 200, Price = 45.0m, OrderDate = DateTime.Now.AddDays(-3), IsCompleted = true, SupplierId = 1 },
                new Order { Id = 3, ProductName = "Картофель ", Quantity = 500, Price = 35.0m, OrderDate = DateTime.Now.AddDays(-2), IsCompleted = false, SupplierId = 2 },
                new Order { Id = 4, ProductName = "Сыр", Quantity = 150, Price = 450.0m, OrderDate = DateTime.Now.AddDays(-1), IsCompleted = false, SupplierId = 3 }
            );
        }
    }
}