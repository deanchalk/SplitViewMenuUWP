using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SplitViewMenuUWP.Scenario1;
using SplitViewMenuUWP.Scenario2;
using SplitViewMenuUWP.Scenario3;

namespace SplitViewMenuUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnScenario1Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Scenario1Main));
        }

        private void OnScenario2Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Scenario2Main));
        }

        private void OnScenario3Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Scenario3Main));
        }
    }
}