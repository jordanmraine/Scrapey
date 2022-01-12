using System;
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;
using Moq;
using NUnit.Framework;

using Models.Clients;

namespace Models.UnitTests.Clients
{
    internal class SearchClientTests
    {
        [Test]
        public async Task ShouldThrowArgumentExceptionIfSearchTextIsEmpty()
        {
            var mockHttpClient = new Mock<HttpClient>();
            SearchClient searchClient = new(mockHttpClient.Object);

            Func<Task> act = () => searchClient.SearchAsync("", 100);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [TestCase(0)]
        [TestCase(-10)]
        public async Task ShouldThrowArgumentExceptionForInvalidNumberOfResults(int numberOfResults)
        {
            var mockHttpClient = new Mock<HttpClient>();
            SearchClient searchClient = new(mockHttpClient.Object);

            Func<Task> act = () => searchClient.SearchAsync("Some text", numberOfResults);

            await act.Should().ThrowAsync<ArgumentException>();
        }
    }
}
