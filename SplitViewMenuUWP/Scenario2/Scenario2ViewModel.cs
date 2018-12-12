using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SplitViewMenuUWP.Scenario2
{
    public class Scenario2ViewModel : INotifyPropertyChanged
    {
        public Scenario2ViewModel()
        {
            MenuItems = new ObservableCollection<Scenario2NavMenuIItem>();
            InitialPage = typeof (Scenario2Page1);
        }

        public ObservableCollection<Scenario2NavMenuIItem> MenuItems { get; }
        public Type InitialPage { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}