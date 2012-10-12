using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Resources;
using System.Xml.Serialization;
using System.ServiceModel;
using System.Reactive.Linq;
using SHPEFeedsReader.FeedsService;

namespace SHPEFeedsReader.Model
{
    public class DataService : IFeedService
    {
        private readonly FeedsService.FeedsServiceClient _WebClient;
        private Action FeedResultAvailable = null;

        public ObservableCollection<FeedsService.FeedItem> FeedResults { get; set; }

        public DataService()
        {
            Uri servUri = new Uri("../FeedsService.svc", UriKind.Relative);
            EndpointAddress servAddr = new EndpointAddress(servUri);
            _WebClient = new FeedsService.FeedsServiceClient("BasicHttpBinding_IFeedsService", servAddr);
        }

        /// <summary>
        /// Retrieve the feed from the companies feed list through a webclient
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public void GetFeed(CompanyFeed SelectedCompany, Action Callback)
        {
            if(Callback == null)
            {
                return;
            }

            FeedResultAvailable = Callback;

            switch (SelectedCompany.Platform)
            {
                case "Jobs2Web":
                    GetJobs2WebFeed(SelectedCompany);
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// Gets the feed through the Jobs2Web WCF Service
        /// </summary>
        /// <param name="SelectedCompany"></param>
        /// <param name="Callback"></param>
        private void GetJobs2WebFeed(CompanyFeed SelectedCompany)
        {
            _WebClient.GetJobs2WebFeedCompleted += ServiceClient_Jobs2WebFeedCompleted;
            _WebClient.GetJobs2WebFeedAsync(SelectedCompany.FeedUrl);
        }

        /// <summary>
        /// Callback from model that feed is ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ServiceClient_Jobs2WebFeedCompleted(object sender, FeedsService.GetJobs2WebFeedCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                FeedResults = e.Result;
                if (FeedResultAvailable != null)
                {
                    FeedResultAvailable();
                }
            }
        }

        /// <summary>
        /// Retrieve the list of companies and their feeds from the xml file
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CompanyFeed> GetCompanies()
        {
            try
            {
                string path = "/SHPEFeedsReader;component/Model/CompanyFeeds.xml";
                Uri uri = new Uri(path, UriKind.Relative);
                StreamResourceInfo sri = Application.GetResourceStream(uri);
                if (sri == null)
                {
                    MessageBox.Show("Unable to load company feeds listing. Please Report!");
                    return null;
                }

                XmlSerializer mySerializer = new XmlSerializer(typeof(CompanyFeeds));
                CompanyFeeds result = (CompanyFeeds)mySerializer.Deserialize(sri.Stream);

                //Empty xml or error...
                if (result.CompanyFeed == null)
                {
                    return new ObservableCollection<CompanyFeed>();
                }

                return new ObservableCollection<CompanyFeed>(result.CompanyFeed);
            }
            catch (Exception)
            {
                //eat it for now.... add logging later
                return null;
            }
        }
    }
}
