using Windows.UI.Xaml.Controls;

namespace SplitViewMenuUWP.Scenario2
{
    public sealed partial class Scenario2Main
    {
        public Scenario2Main()
        {
            InitializeComponent();
            var mainViewModel = new Scenario2ViewModel();
            mainViewModel.MenuItems.Add(new Scenario2NavMenuIItem
            {
                Label = "Page 1",
                DestinationPage = typeof (Scenario2Page1),
                Symbol = Symbol.Bookmarks,
                NotifyCount = 2
            });
            mainViewModel.MenuItems.Add(new Scenario2NavMenuIItem
            {
                Label = "Page 2",
                DestinationPage = typeof (Scenario2Page2),
                Symbol = Symbol.Emoji,
                NotifyCount = 7
            });
            mainViewModel.MenuItems.Add(new Scenario2NavMenuIItem
            {
                Label = "Page 3",
                DestinationPage = typeof (Scenario2Page3),
                Symbol = Symbol.RotateCamera,
                NotifyCount = 1
            });
            DataContext = mainViewModel;
        }
    }
}