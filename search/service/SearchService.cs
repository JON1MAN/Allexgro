

public class SearchService : ISearchService
{
    private readonly ISearchRepository _searchRepository;
    private readonly ILogger<SearchService> _logger;

    public SearchService(ISearchRepository searchRepository, ILogger<SearchService> logger)
    {
        _searchRepository = searchRepository;
        _logger = logger;
    }
    public ICollection<Product> filterProducts(FilterDTO filterDTO)
    {
        _logger.LogInformation("Filtering products with filter criteria: {filterDTO}", filterDTO.ToString());
        var result = _searchRepository.filterProducts(filterDTO);
        return result;
    }
}