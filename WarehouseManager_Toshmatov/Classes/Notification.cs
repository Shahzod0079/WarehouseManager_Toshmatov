using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WarehouseManager_Toshmatov.Classes
{
    public class Notification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)  // ← добавить проверку
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}