using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;
using NUnit.Framework;

using Models.Clients;
using Models.Parsers;
using Models.Services;

namespace Models.IntegrationTests.Services
{
    internal class ResultServiceTests
    {
        [Test]
        public async Task ShouldReturnExpectedRankings()
        {
            using HttpClient httpClient = new();

            ISearchClient searchClient = new SearchClient(httpClient);
            ISearchParser searchParser = new RegexSearchParser();

            ResultService sut = new(searchClient, searchParser);
            IEnumerable<int> rankings = await sut.GetSearchRankingsAsync("google search", "www.google.com", 10);

            rankings.Should().NotBeEmpty().And.HaveCountGreaterThan(0);
        }
    }
}
