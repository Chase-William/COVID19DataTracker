using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using COVID19DataTracker.Pages;
using Xamarin.Essentials;
using System.Net;
/// <summary>
/// 
///     Would really like to try some C# 8.0 features in this project so may need to dump UWP at some point.
///     https://docs.microsoft.com/en-us/dotnet/standard/net-standard#net-implementation-support
/// 
/// </summary>
namespace COVID19DataTracker
{
    public partial class App : Application
    {
        // Key for our rapidapi covid19 api
        public const string COVID19_API_KEY = "rapidapi_covid19_api_key";

        public App()
        {
            InitializeComponent();

            System.Console.WriteLine();

            // Initalize first launch data required if this is first launch of application.
            if (VersionTracking.IsFirstLaunchEver) InitializeFirstLaunchDataAsync();                        

            MainPage = new MainMasterPage();                       
        }

        /// <summary>
        ///     Inserts data into SecureStorage for safe keeping and later retrieval.
        ///     Erases contents from launch files.
        /// </summary>
        private async void InitializeFirstLaunchDataAsync()
        {
            await Task.Run(async () =>
            {
                string fileContents = string.Empty;

                using (var stream = await FileSystem.OpenAppPackageFileAsync("first_launch_config.txt"))
                using (var reader = new StreamReader(stream))

                    fileContents = await reader.ReadToEndAsync();

                // Putting our token in securestorage
                await SecureStorage.SetAsync(COVID19_API_KEY, fileContents);
            });            
        }

        protected override void OnStart()
        {            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        //var client = new RestClient("https://covid-19-data.p.rapidapi.com/country/all?format=undefined");
        //var request = new RestRequest(Method.GET);
        //request.AddHeader("x-rapidapi-host", "covid-19-data.p.rapidapi.com");
        //      request.AddHeader("x-rapidapi-key", "b2a53471bemsh888b9e6a270d2b3p1adad3jsn2c166912e1f9");
        //      IRestResponse response = client.Execute(request);

        //string content = response.Content;


        //var client = new RestClient("https://covid-19-statistics.p.rapidapi.com/reports?region_province=Beijing");
        //var request = new RestRequest(Method.GET);
        //request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
        //      request.AddHeader("x-rapidapi-key", "b2a53471bemsh888b9e6a270d2b3p1adad3jsn2c166912e1f9");
        //      IRestResponse response = client.Execute(request);
    }
}
