namespace Models.Clients
{
    public interface ISearchClient
    {
        Task<string> SearchAsync(string searchText, int numberOfResults);
    }
}
