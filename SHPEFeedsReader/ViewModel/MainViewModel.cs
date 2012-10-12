using GalaSoft.MvvmLight;
using SHPEFeedsReader.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System;
using SHPEFeedsReader.FeedsService;

namespace SHPEFeedsReader.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The data service providing us the information
        /// </summary>
        private readonly IFeedService _dataService;

        /// <summary>
        /// List of all the companies and their feeds
        /// </summary>
        public ObservableCollection<CompanyFeed> Companies
        {
            get
            {
                return _dataService.GetCompanies();
            }
        }

        /// <summary>
        /// The requested url to service based on the selected feed
        /// </summary>
        private CompanyFeed _selectedFeed;
        public CompanyFeed SelectedFeed
        {
            get
            {
                return _selectedFeed;
            }
            set
            {
                if (_selectedFeed == value) return;
                _selectedFeed = value;
                RaisePropertyChanged("SelectedFeed");
            }
        }

        /// <summary>
        /// The results from a feed retrievel from the dataservice
        /// </summary>
        public ObservableCollection<FeedsService.FeedItem> FeedResultsCollection { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IFeedService dataService)
        {
            _dataService = dataService;
            FeedResultsCollection = new ObservableCollection<FeedsService.FeedItem>();
        }

        // <summary>
        /// Get the feed based on the selected company
        /// </summary>
        private RelayCommand _getFeedCommand;
        public RelayCommand GetFeedCommand
        {
            get
            {
                if (_getFeedCommand == null)
                {
                    _getFeedCommand = new RelayCommand(
                        () => this.GetFeed()
                            );
                }
                return _getFeedCommand;
            }
        }

        /// <summary>
        /// Get the companies feed and display in the list box
        /// </summary>
        private void GetFeed()
        {
            if (SelectedFeed == null)
            {
                return;
            }
            if (SelectedFeed.FeedUrl == null)
            {
                return;
            }
            _dataService.GetFeed(SelectedFeed, ServiceClient_GetFeedCompleted);
        }

        /// <summary>
        /// Callback from model that feed is ready
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ServiceClient_GetFeedCompleted()
        {
            FeedResultsCollection = _dataService.FeedResults;
            RaisePropertyChanged("FeedResultsCollection");
        }

    }
}