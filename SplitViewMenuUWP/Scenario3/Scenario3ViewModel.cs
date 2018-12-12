using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SplitViewMenu;

namespace SplitViewMenuUWP.Scenario3
{
    public class Scenario3ViewModel
    {
        public Scenario3ViewModel()
        {
            MenuItems = new ObservableCollection<SimpleNavMenuItem>();
            InitialPage = typeof (Scenario3Page1);
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