using System.Threading.Tasks;
using System.Net;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.ComponentModel;
using COVID19DataTracker.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;

namespace COVID19DataTracker.ViewModels
{
    public class NewsFeedPageBM : NotifyClass
    {
        /// <summary>
        ///     Webclient used for getting RSS data.
        /// </summary>
        public WebClient Client { get; set; } = new WebClient();
        
        private ObservableCollection<RSSContainer> rssFeed = new ObservableCollection<RSSContainer>();
        /// <summary>
        ///     Collection that holds our RSS data.
        /// </summary>
        public ObservableCollection<RSSContainer> RSSFeed
        {
            get => rssFeed;
            set
            {
                // If the value is the same, don't bother assigning and notifying the view
                if (rssFeed != value)
                {
                    rssFeed = value;
                    NotifyPropertyChanged();
                }
            }
        }   


        /// <summary>
        ///     Opens the source website's article from it's RSS feed within the application.
        /// </summary>
        public ICommand GetSourceCMD => new Command(async (link) =>
        {
            await Browser.OpenAsync((string)link, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show
            });
        });

        /// <summary>
        ///     
        /// </summary>
        public ICommand RSSRefreshCMD => new Command(async () =>
        {            
            await ParseRssFile();            
        });

        public NewsFeedPageBM()
        {
            InitalizeData();

            RSSFeed.CollectionChanged += delegate { NotifyPropertyChanged(nameof(RSSFeed)); };
        }



        private async void InitalizeData() => await ParseRssFile();

        /// <summary>
        ///     Parses the XMl RSS file.
        ///     This method comes from this site: https://www.codeproject.com/articles/820669/how-to-parse-rss-feeds-in-net
        /// </summary>
        private async Task ParseRssFile()
        {
            await Task.Run(() =>
            {
                XmlDocument rssXmlDoc = new XmlDocument();

                // Load the RSS file from the RSS URL
                rssXmlDoc.Load("https://www.cnbc.com/id/100003114/device/rss/rss.html");

                // Parse the Items in the RSS file
                XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                // Iterate through the items in the RSS file
                foreach (XmlNode rssNode in rssNodes)
                {
                    XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                    string title = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNode.SelectSingleNode("link");
                    string link = rssSubNode != null ? rssSubNode.InnerText : "";

                    rssSubNode = rssNode.SelectSingleNode("description");
                    string description = rssSubNode != null ? rssSubNode.InnerText : "";

                    // If this RSSContainer already exist (same link) don't add it and continue iterating.
                    if (!RSSFeed.Any(x => x.Link == link))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            RSSFeed.Add(new RSSContainer
                            {
                                Title = title,
                                Link = link,
                                Description = description
                            });
                        });                        
                    }
                }
            });            
        }        
    }
}
