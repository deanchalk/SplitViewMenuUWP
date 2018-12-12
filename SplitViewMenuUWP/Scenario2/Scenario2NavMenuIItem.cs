using System;
using Windows.UI.Xaml.Controls;
using SplitViewMenu;

namespace SplitViewMenuUWP.Scenario2
{
    public class Scenario2NavMenuIItem : INavigationMenuItem
    {
        public Symbol Symbol { get; set; }
        public char SymbolAsChar => (char) Symbol;
        public string Label { get; set; }
        public object Arguments { get; set; }
        public Type DestinationPage { get; set; }
        public int NotifyCount { get; set; }
    }
}