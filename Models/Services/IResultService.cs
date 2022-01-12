namespace Models.Services
{
    public interface IResultService
    {
        Task<IEnumerable<int>> GetSearchRankingsAsync(string searchText, string url, int maxNumberOfResults);
    }
}
