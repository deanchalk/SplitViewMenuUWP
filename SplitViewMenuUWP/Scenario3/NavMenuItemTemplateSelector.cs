using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SplitViewMenu;

namespace SplitViewMenuUWP.Scenario3
{
    public class NavMenuItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Page1Template { get; set; }
        public DataTemplate OtherPageTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var navItem = item as INavigationMenuItem;
            if (navItem == null)
                return null;
            return navItem.Label == "Page 1" ? Page1Template : OtherPageTemplate;
        }
    }
}