using System.ComponentModel;

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

        public SearchRankingsViewModel()
        {
            Rankings = "TEST??";
        }
    }
}
