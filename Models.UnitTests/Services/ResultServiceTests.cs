using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;
using Moq;
using NUnit.Framework;

using Models.Clients;
using Models.Parsers;
using Models.Services;

namespace Models.UnitTests.Services
{
    internal class ResultServiceTests
    {
        [Test]
        public async Task ShouldReturnExpectedRankings()
        {
            var mockClient = new Mock<ISearchClient>();

            var mockParser = new Mock<ISearchParser>();
            mockParser
                .Setup(m => m.ParseSearchHtml(It.IsAny<string>()))
                .Returns(new List<SearchResult>
                {
                    new SearchResult { Rank = 1, Url = "https://www.example.com/" },
                    new SearchResult { Rank = 2, Url = "https://www.other.com/" },
                    new SearchResult { Rank = 3, Url = "https://www.example.com" },
                    new SearchResult { Rank = 4, Url = "https://www.other.com/" },
                    new SearchResult { Rank = 5, Url = "http://www.example.com/" }
                });

            ResultService sut = new(mockClient.Object, mockParser.Object);
            IEnumerable<int> rankings = await sut.GetSearchRankingsAsync("a search", "www.example.com", 5);

            rankings.Should().NotBeEmpty().And.HaveCount(3).And.BeEquivalentTo(new[] { 1, 3, 5 });
        }
    }
}
