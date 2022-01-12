namespace Models.Clients
{
    public class SearchClient : ISearchClient
    {
        public Task<string> SearchAsync(string searchText, int numberOfResults)
        {
            throw new NotImplementedException();
        }
    }
}
