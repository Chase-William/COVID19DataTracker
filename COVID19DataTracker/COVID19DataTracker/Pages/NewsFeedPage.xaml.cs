using COVID19DataTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace COVID19DataTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsFeedPage : ContentPage
    {
        public NewsFeedPageBM VM => (NewsFeedPageBM)BindingContext;
        public NewsFeedPage()
        {
            InitializeComponent();
            // When the listview is done refreshing; turn off the refresh icon.
            RSSListView.Refreshing += delegate { RSSListView.IsRefreshing = false; };
        }
    }
}