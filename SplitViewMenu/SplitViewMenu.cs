using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SplitViewMenu
{
    public sealed class SplitViewMenu : Control
    {
        internal static readonly DependencyProperty MenuItemDataTemplateSelectorProperty =
            DependencyProperty.Register("MenuItemDataTemplateSelector", typeof(DataTemplateSelector),
                typeof(SplitViewMenu), new PropertyMetadata(null));

        internal static readonly DependencyProperty NavMenuItemTemplateProperty =
            DependencyProperty.Register("NavMenuItemTemplate", typeof(DataTemplate), typeof(SplitViewMenu),
                new PropertyMetadata(null));

        internal static readonly DependencyProperty NavMenuItemContainerStyleProperty =
            DependencyProperty.Register("NavMenuItemContainerStyle", typeof(Style), typeof(SplitViewMenu),
                new PropertyMetadata(null));

        internal static readonly DependencyProperty InitialPageProperty =
            DependencyProperty.Register("InitialPage", typeof(Type), typeof(SplitViewMenu),
                new PropertyMetadata(null));

        internal static readonly DependencyProperty NavigationItemsProperty =
            DependencyProperty.Register("NavigationItems", typeof(IEnumerable<INavigationMenuItem>),
                typeof(SplitViewMenu),
                new PropertyMetadata(Enumerable.Empty<INavigationMenuItem>(), OnNavigationItemsPropertyChanged));

        private Button backButton;
        private NavMenuListView navMenuListView;
        private Frame pageFrame;

        public SplitViewMenu()
        {
            DefaultStyleKey = typeof(SplitViewMenu);
            Loaded += OnSplitViewMenuLoaded;
        }

        public DataTemplateSelector MenuItemDataTemplateSelector
        {
            get => (DataTemplateSelector) GetValue(MenuItemDataTemplateSelectorProperty);
            set => SetValue(MenuItemDataTemplateSelectorProperty, value);
        }

        public DataTemplate NavMenuItemTemplate
        {
            get => (DataTemplate) GetValue(NavMenuItemTemplateProperty);
            set => SetValue(NavMenuItemTemplateProperty, value);
        }

        public Style NavMenuItemContainerStyle
        {
            get => (Style) GetValue(NavMenuItemContainerStyleProperty);
            set => SetValue(NavMenuItemContainerStyleProperty, value);
        }

        public Type InitialPage
        {
            get => (Type) GetValue(InitialPageProperty);
            set => SetValue(InitialPageProperty, value);
        }

        public IEnumerable<INavigationMenuItem> NavigationItems
        {
            get => (IEnumerable<INavigationMenuItem>) GetValue(NavigationItemsProperty);
            set => SetValue(NavigationItemsProperty, value);
        }

        private void OnSplitViewMenuLoaded(object sender, RoutedEventArgs e)
        {
            if (InitialPage == null || pageFrame == null)
                return;
            pageFrame.Navigate(InitialPage);
        }

        private static void OnNavigationItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menu = (SplitViewMenu) d;
            if (menu.navMenuListView != null)
                menu.navMenuListView.ItemsSource = e.NewValue;
        }

        protected override void OnApplyTemplate()
        {
            pageFrame = GetTemplateChild("PageFrame") as Frame;
            navMenuListView = GetTemplateChild("NavMenuList") as NavMenuListView;
            backButton = GetTemplateChild("BackButton") as Button;

            if (navMenuListView != null)
            {
                navMenuListView.ItemInvoked += OnNavMenuItemInvoked;
                navMenuListView.ContainerContentChanging += OnContainerContextChanging;
            }

            if (backButton != null) backButton.Click += OnBackButtonClick;

            if (pageFrame != null)
            {
                pageFrame.Navigating += OnNavigatingToPage;
                pageFrame.Navigated += OnNavigatedToPage;
            }
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            var ignored = false;
            BackRequested(ref ignored);
        }

        private void BackRequested(ref bool handled)
        {
            if (pageFrame == null)
                return;
            if (!pageFrame.CanGoBack || handled)
                return;
            handled = true;
            pageFrame.GoBack();
        }

        private static void OnContainerContextChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item is INavigationMenuItem)
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((INavigationMenuItem) args.Item).Label);
            else
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;
            if (page != null && e.Content != null)
            {
                var control = page;
                control.Loaded += PageLoaded;
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            ((Page) sender).Focus(FocusState.Programmatic);
            ((Page) sender).Loaded -= PageLoaded;
        }

        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.Back || !NavigationItems.Any())
                return;
            var item = NavigationItems.SingleOrDefault(p => p.DestinationPage == e.SourcePageType);
            if (item == null && pageFrame.BackStackDepth > 0)
                foreach (var entry in pageFrame.BackStack.Reverse())
                {
                    item = NavigationItems.SingleOrDefault(p => p.DestinationPage == entry.SourcePageType);
                    if (item != null)
                        break;
                }

            var container = (ListViewItem) navMenuListView.ContainerFromItem(item);
            if (container != null)
                container.IsTabStop = false;
            navMenuListView.SetSelectedItem(container);
            if (container != null)
                container.IsTabStop = true;
        }

        private void OnNavMenuItemInvoked(object sender, ListViewItem e)
        {
            var item = (INavigationMenuItem) ((NavMenuListView) sender).ItemFromContainer(e);

            if (item?.DestinationPage != null &&
                item.DestinationPage != pageFrame.CurrentSourcePageType)
                pageFrame.Navigate(item.DestinationPage, item.Arguments);
        }
    }
}