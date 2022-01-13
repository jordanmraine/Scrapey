using System.Text.RegularExpressions;

namespace Models.Parsers
{
    public class RegexSearchParser : ISearchParser
    {
        private const string pattern = @"(?<=<div class=""yuRUbf""><a href="")(.*?)(?="" data-ved)";

        public IEnumerable<SearchResult> ParseSearchHtml(string html)
        {
            MatchCollection matches = Regex.Matches(html, pattern);

            List<SearchResult> results = new();

            for (int i = 0; i < matches.Count; i++)
            {
                results.Add(new SearchResult
                {
                    Rank = i + 1,
                    Url = matches[i].Groups[0].Value
                });
            }

            return results;
        }
    }
}
