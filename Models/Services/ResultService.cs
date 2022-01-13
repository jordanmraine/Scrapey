using Models.Clients;
using Models.Parsers;

namespace Models.Services
{
    public class ResultService : IResultService
    {
        private readonly ISearchClient searchClient;
        private readonly ISearchParser searchParser;

        public ResultService(ISearchClient searchClient, ISearchParser searchParser)
        {
            this.searchClient = searchClient ?? throw new ArgumentNullException(nameof(searchClient));
            this.searchParser = searchParser ?? throw new ArgumentNullException(nameof(searchParser));
        }

        public async Task<IEnumerable<int>> GetSearchRankingsAsync(string searchText, string url, int maxNumberOfResults)
        {
            string html = await searchClient.SearchAsync(searchText, maxNumberOfResults);
            IEnumerable<SearchResult> results = searchParser.ParseSearchHtml(html);

            Uri searchUri = new(FormatUrl(url));

            return results
                .Select(r => new { r.Rank, Uri = new Uri(r.Url) })
                .Where(r => string.Equals(r.Uri.Host, searchUri.Host, StringComparison.OrdinalIgnoreCase))
                .Select(r => r.Rank);
        }

        private static string FormatUrl(string url)
        {
            if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                url = $"https://{url}";
            }

            return url;
        }
    }
}
