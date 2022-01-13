using System.Collections.Generic;
using System.IO;

using FluentAssertions;
using NUnit.Framework;

using Models.Parsers;

namespace Models.UnitTests.Parsers
{
    internal class RegexSearchParserTests
    {
        [Test]
        public void ShouldReturnExpectedNumberOfSearchResults()
        {
            RegexSearchParser searchParser = new();

            string sampleHtml = File.ReadAllText("Resources/sample.html");

            IEnumerable<SearchResult> results = searchParser.ParseSearchHtml(sampleHtml);

            results.Should().HaveCount(5);
        }
    }
}
