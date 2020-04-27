using RestSharp;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using COVID19DataTracker.Pages;
using COVID19DataTracker.Models.COVID19_Statistics;

namespace COVID19DataTracker.ViewModels
{
    public class QueryStatisticsPageVM : NotifyClass
    {
        private ObservableCollection<Datum> queryResult;
        public ObservableCollection<Datum> QueryResults
        {
            get => queryResult;
            set
            {
                if (queryResult == value) return;

                queryResult = value;
                NotifyPropertyChanged();
            }
        }

        public QueryParameter ProvinceOrState { get; set; } = new QueryParameter("region_province");
        public QueryParameter CountryISOCode { get; set; }  = new QueryParameter("iso");
        public QueryParameter RegionName { get; set; }      = new QueryParameter("region_name");
        public QueryParameter CityName { get; set; }        = new QueryParameter("city_name");

        // Good for chart: https://rapidapi.com/Yatko/api/coronavirus-map?endpoint=apiendpoint_5c9df527-2111-4553-a09e-e6098736e1d8
        // Currently Used: https://rapidapi.com/axisbits-axisbits-default/api/covid-19-statistics?endpoint=apiendpoint_ef9e1955-666c-43ba-9b5c-4b463ae316dc
        //public ICommand GetQueryDataCMD => new Command(async () => {
        //    await GetQueryDataFromServerAsync(CreateQueryString());            
        //});


        /// <summary>
        ///     Creates a query string from our query properties.
        /// </summary>
        public string CreateQueryString()
        {
            // Const that define the index value of the first parameter.
            const sbyte IS_FIRST_PARAMETER = 0;
            // Collection of parameters.
            var parameters = new List<QueryParameter>();           
            string queryStr = string.Empty;

            // Check if the property is null or whitespace, if it is not either then add to parameter collection.
            if (!string.IsNullOrWhiteSpace(ProvinceOrState.ParameterValue)) parameters.Add(ProvinceOrState);
            if (!string.IsNullOrWhiteSpace(CountryISOCode.ParameterValue)) parameters.Add(CountryISOCode);
            if (!string.IsNullOrWhiteSpace(RegionName.ParameterValue)) parameters.Add(RegionName);
            if (!string.IsNullOrWhiteSpace(CityName.ParameterValue)) parameters.Add(CityName);

            // Iterate parameter collection and append parameters.
            for (int i = 0; i < parameters.Count; i++)
            {
                // First parameter begins with '?'
                // Other paramters begins with '&'
                queryStr += i == IS_FIRST_PARAMETER? '?' + parameters[i].UrlParameterIdentifier + '=' + parameters[i] : '&' + parameters[i].UrlParameterIdentifier + '=' + parameters[i];
            }

            // Return full parameter string.
            return queryStr;
        }

        /// <summary>
        ///     Queries the rapidapi endpoint for getting our desired data.
        /// </summary>
        public async Task GetQueryDataFromServerAsync(string _queryString)
        {
            await Task.Run(async () =>
            {
                //region_province=Beijing
                //iso=CHN&region_name=China
                //date=2020-03-14
                //q=China%20Beijing
                var client = new RestClient("https://covid-19-statistics.p.rapidapi.com/reports?region_province=Alabama&iso=USA&region_name=US&city_name=Autauga&date=2020-04-16");
                //var client = new RestClient($"https://covid-19-statistics.p.rapidapi.com/reports{_queryString}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", await SecureStorage.GetAsync(App.COVID19_API_KEY));
                IRestResponse response = client.Execute(request);

                // If something went bad return.
                if (response.StatusCode != System.Net.HttpStatusCode.OK) return;

                // Parsing the json data into C# objects before assigning it to our QueryResults on the mainthread.
                var tempResults = new ObservableCollection<Datum>(JsonConvert.DeserializeObject<RootObject>(response.Content).Data);
                // Updating QueryResults safely on the mainthread.
                MainThread.BeginInvokeOnMainThread(() => QueryResults = tempResults);                
            });            
        }
    }

    public class QueryParameter
    {
        /// <summary>
        ///     The idenifier in the url used by the server for your query.
        /// </summary>
        public readonly string UrlParameterIdentifier;
        /// <summary>
        ///     The value in the url used by the server for your query.
        /// </summary>
        public string ParameterValue;
        
        // Locking the QueryParameter default constructor because you will always have a idenifier and parameter value together as a pair.
        private QueryParameter() { }
        public QueryParameter(string _urlParameterIdentifier)
        {
            UrlParameterIdentifier = _urlParameterIdentifier;
        }
    }
}