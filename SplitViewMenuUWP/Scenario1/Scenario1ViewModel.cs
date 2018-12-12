using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SplitViewMenu;

namespace SplitViewMenuUWP.Scenario1
{
    public class Scenario1ViewModel : INotifyPropertyChanged
    {
        public Scenario1ViewModel()
        {
            MenuItems = new ObservableCollection<SimpleNavMenuItem>();
            InitialPage = typeof (Scenario1Page1);
        }

        public ObservableCollection<SimpleNavMenuItem> MenuItems { get; }
        public Type InitialPage { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}