namespace Models.Parsers
{
    public interface ISearchParser
    {
        IEnumerable<SearchResult> ParseSearchHtml(string html);
    }
}
