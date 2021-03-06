using System.Web;

namespace Models.Clients
{
    public class SearchClient : ISearchClient
    {
        private const string chromeUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";
        private const string baseSearchAddress = "https://www.google.com/search";

        private readonly HttpClient httpClient;

        public SearchClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            this.httpClient.DefaultRequestHeaders.Add("User-Agent", chromeUserAgent);
        }

        /// <summary>
        /// Returns the raw HTML of a search with the given <paramref name="searchText"/> and <paramref name="numberOfResults"/>.
        /// </summary>
        public async Task<string> SearchAsync(string searchText, int numberOfResults)
        {
            if (string.IsNullOrEmpty(searchText)) throw new ArgumentException($"{nameof(searchText)} cannot be null or empty.", nameof(searchText));
            if (numberOfResults <= 0) throw new ArgumentException($"{nameof(numberOfResults)} must be larger than zero.", nameof(numberOfResults));

            Uri uri = BuildSearchUri(searchText, numberOfResults);

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new ApplicationException($"Invalid response from search client: {response.ReasonPhrase}");
            }
        }

        private static Uri BuildSearchUri(string searchText, int numberOfResults)
        {
            UriBuilder builder = new(baseSearchAddress);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["num"] = numberOfResults.ToString();
            query["q"] = searchText;

            builder.Query = query.ToString();

            return builder.Uri;
        }
    }
}
