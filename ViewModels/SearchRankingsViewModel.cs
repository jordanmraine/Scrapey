using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

using Models.Clients;
using Models.Parsers;
using Models.Services;
using ViewModels.Commands;

namespace ViewModels
{
    public sealed class SearchRankingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));

                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        private string url;
        public string Url
        {
            get => url;
            set
            {
                url = value;
                OnPropertyChanged(nameof(Url));

                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        private string rankings;
        public string Rankings
        {
            get => rankings;
            set
            {
                rankings = value;
                OnPropertyChanged(nameof(Rankings));
            }
        }

        private string searchButtonText;
        public string SearchButtonText
        {
            get => searchButtonText;
            set
            {
                searchButtonText = value;
                OnPropertyChanged(nameof(SearchButtonText));
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;

                if (isBusy)
                {
                    SearchButtonText = "Loading...";
                }
                else
                {
                    SearchButtonText = "Get Rankings";
                }

                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public BaseCommand SearchCommand { get; set; }

        private readonly ResultService resultService;
        private const int maxResults = 100;

        public SearchRankingsViewModel()
        {
            // TODO: dependency injection.
            resultService = new(new SearchClient(new HttpClient()), new RegexSearchParser());
            SearchCommand = new BaseCommand(OnSearch, CanSearch);

            IsBusy = false;
        }

        private async void OnSearch()
        {
            IsBusy = true;

            IEnumerable<int> searchRankings = await resultService.GetSearchRankingsAsync(SearchText, Url, maxResults);

            if (searchRankings == null || !searchRankings.Any())
            {
                Rankings = "0";
            }
            else
            {
                Rankings = string.Join(", ", searchRankings.OrderBy(sr => sr));
            }

            IsBusy = false;
        }

        private bool CanSearch()
        {
            return !string.IsNullOrWhiteSpace(SearchText) && !string.IsNullOrWhiteSpace(Url);
        }
    }
}
