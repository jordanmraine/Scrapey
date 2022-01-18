# Scrapey
A .NET 6 WPF application to manually scrape Google and rank URLs on a given search term.

## Project Requirements
- Scrape Google to determine where a given URL ranks with the given search terms
- Manual scraping is preferred - i.e. no Google API or HTML Agility Pack
- Must return an ordered list of rankings (e.g. 1, 10, 33), or 0 if the URL isn't present in the search
- WPF UI

## Further Considerations
The following changes could be made to improve the application:
- More reliable methods to retrieve data (e.g. Google API, HTML Agility Pack)
- Move the scraping & parsing code out of the Models project and into a Services project
- Dependency injection for the service/scraper/client
- More unit & integration tests
- UI testing project
- End-to-end testing project
- Exception handling
