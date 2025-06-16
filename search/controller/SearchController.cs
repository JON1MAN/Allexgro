
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("api/v1/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly IMapper _productMapper;

    public SearchController(ISearchService searchService, IMapper productMapper)
    {
        _searchService = searchService;
        _productMapper = productMapper;
    }

    [HttpPost()]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductDTO>> filterProducts([FromBody] FilterDTO request)
    {
        var products = _searchService.filterProducts(request);
        return Ok(mapToListDTO(products));
    }
    
    private ICollection<ProductDTO> mapToListDTO(ICollection<Product> products)
    {
        var response = new List<ProductDTO>();

        foreach (var product in products)
        {
            response.Add(_productMapper.Map<ProductDTO>(product));
        }

        return response;
    }
}