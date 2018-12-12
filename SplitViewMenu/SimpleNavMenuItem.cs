using System;
using Windows.UI.Xaml.Controls;

namespace SplitViewMenu
{
    public sealed class SimpleNavMenuItem : INavigationMenuItem
    {
        public Symbol Symbol { get; set; }
        public char SymbolAsChar => (char) Symbol;
        public string Label { get; set; }
        public object Arguments { get; set; }
        public Type DestinationPage { get; set; }
    }
}