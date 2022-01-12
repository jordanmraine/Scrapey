using System.Threading.Tasks;

using FluentAssertions;
using NUnit.Framework;

using Models.Clients;
using System.Net.Http;

namespace Models.IntegrationTests.Clients
{
    internal class SearchClientTests
    {
        [Test]
        public async Task ShouldReturnHtmlTextAsync()
        {
            using HttpClient client = new();
            SearchClient searchClient = new(client);

            string result = await searchClient.SearchAsync("Some text", 100);

            result.Should().StartWith("<!doctype html>");
        }
    }
}
