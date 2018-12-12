using Windows.UI.Xaml.Controls;
using SplitViewMenu;

namespace SplitViewMenuUWP.Scenario3
{
    public sealed partial class Scenario3Main : Page
    {
        public Scenario3Main()
        {
            InitializeComponent();
            var mainViewModel = new Scenario3ViewModel();
            mainViewModel.MenuItems.Add(new SimpleNavMenuItem
            {
                Label = "Page 1",
                DestinationPage = typeof (Scenario3Page1),
                Symbol = Symbol.Bookmarks
            });
            mainViewModel.MenuItems.Add(new SimpleNavMenuItem
            {
                Label = "Page 2",
                DestinationPage = typeof (Scenario3Page2),
                Symbol = Symbol.Emoji
            });
            mainViewModel.MenuItems.Add(new SimpleNavMenuItem
            {
                Label = "Page 3",
                DestinationPage = typeof (Scenario3Page3),
                Symbol = Symbol.RotateCamera
            });
            DataContext = mainViewModel;
        }
    }
}