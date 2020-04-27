using COVID19DataTracker.ViewModels;
using RestSharp;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID19DataTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryStatisticsPage : ContentPage
    {        
        public QueryStatisticsPage()
        {
            InitializeComponent();                   
        }

        private async void OnGetQueryResults_BtnClicked(object sender, System.EventArgs e)
        {
            var VM = (QueryStatisticsPageVM)BindingContext;

            // Gets the desired information from the api endpoint and assigns it into this bindingContext's QueryResultsCollection.
            await VM.GetQueryDataFromServerAsync(VM.CreateQueryString());
            await this.Navigation.PushAsync(new QueryStatisticsResultsPage(VM.QueryResults));
        }
    }
}