using System.ComponentModel;
using UPS.WPFDemoApp.Models;

namespace UPS.WPFDemoApp.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public enum StatusEnum { active = 1, inactive=0 }
        public enum GenderEnum { Male , Female }
        protected void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}