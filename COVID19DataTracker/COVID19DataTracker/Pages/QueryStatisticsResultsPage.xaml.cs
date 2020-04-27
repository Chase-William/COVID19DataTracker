using COVID19DataTracker.Models.COVID19_Statistics;
using COVID19DataTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID19DataTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryStatisticsResultsPage : ContentPage
    {
        public QueryStatisticsResultsPage(IList<Datum> _results)
        {
            InitializeComponent();
            ResultsCollectionView.ItemsSource = _results;
        }
    }
}