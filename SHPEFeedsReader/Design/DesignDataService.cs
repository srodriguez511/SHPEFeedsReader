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
using SHPEFeedsReader.Model;
using System.Collections.ObjectModel;
using SHPEFeedsReader.FeedsService;

namespace SHPEFeedsReader.Design
{
    public class DesignDataService : IFeedService
    {

        public System.Collections.ObjectModel.ObservableCollection<FeedsService.FeedItem> FeedResults { get; set; }

        public void GetFeed(CompanyFeed Company, Action Callback)
        {
            ObservableCollection<FeedsService.FeedItem> temp = new ObservableCollection<FeedsService.FeedItem>();
            FeedItem a = new FeedItem();
            a.Title = "My Title";
            a.Date = DateTime.Today.ToShortDateString();
            a.Description = "this is a really long description this is a really long description this is a really long description this is a really long description this is a really long description this is a really long descriptionthis is a really long descriptionvthis is a really long descriptionthis is a really long descriptionthis is a really long descriptionthis is a really long descriptionthis is a really long descriptionthis is a really long description";
            a.Url = "www.google.com";

            temp.Add(a);

            FeedResults = temp;
        }

        public System.Collections.ObjectModel.ObservableCollection<CompanyFeed> GetCompanies()
        {
            CompanyFeed c = new CompanyFeed();
            c.CompanyName = "Test";

            ObservableCollection<CompanyFeed> Temp = new ObservableCollection<CompanyFeed>();
            Temp.Add(c);
            return Temp;
        }
    }
}
