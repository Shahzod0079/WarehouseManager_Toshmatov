using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManager_Toshmatov.Classes;

namespace WarehouseManager_Toshmatov.Models
{
    public class Supplier : Notification
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("ContactPerson");
            }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public virtual ObservableCollection<Order> Orders { get; set; }
        public string ContactPerson { get; internal set; }

        public Supplier()
        {
            Orders = new ObservableCollection<Order>();
        }
    }
}
