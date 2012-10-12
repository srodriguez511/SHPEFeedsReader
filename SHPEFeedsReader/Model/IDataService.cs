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
using SHPEFeedsReader.FeedsService;

namespace SHPEFeedsReader.Model
{
    public interface IFeedService
    {
        ObservableCollection<FeedsService.FeedItem> FeedResults { get; set; }

        void GetFeed(CompanyFeed Company, Action Callback);
        ObservableCollection<CompanyFeed> GetCompanies();
    }
}
