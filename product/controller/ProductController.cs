
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _productMapper;
    private readonly ISecurityUtils _securityUtils;

    public ProductController(IProductService service, IMapper productMapper, ISecurityUtils securityUtils)
    {
        _productService = service;
        _productMapper = productMapper;
        _securityUtils = securityUtils;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    public ActionResult<ProductDTO> FindById([FromRoute] int id)
    {
        var product = _productService.FindById(id);
        if (product == null)
            return NotFound();

        return Ok(_productMapper.Map<ProductDTO>(product));
    }

    [HttpGet()]
    public ActionResult<List<Product>> FindAll()
    {
        var products = _productService.FindAll();

        var response = mapToListDTO(products);

        return Ok(response);
    }

    [HttpGet("my")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<List<Product>> FindProductsForLoggedUser()
    {
        string? userId = _securityUtils.getCurrentLoggedUserId();
        var products = (userId != null) ? _productService.FindProductsForLoggedUser(userId) : null;

        if (products == null)
        {
            return NotFound("There is no products for user with id: " + userId);
        }

        var response = mapToListDTO(products);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<ProductDTO> CreateProduct([FromBody] ProductCreateDTO request)
    {
        var sellerId = _securityUtils.getCurrentLoggedUserId();
        var response = _productService.CreateProduct(request, sellerId);
        return Ok(_productMapper.Map<ProductDTO>(response));
    }

    [HttpGet("product-categories")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductCategory>> FindAllProductCategories()
    {
        var response = _productService.FindAllProductCategories();
        if (response == null)
        {
            return NotFound("There is no product categories");
        }
        return Ok(response);
    }

    [HttpGet("product-categories/{categoryId}/product-types")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductType>> FindProductTypesByCategory(int categoryId)
    {
        var response = _productService.FindProductTypesForProductCategory(categoryId);
        if (response == null)
        {
            return NotFound("There is no product type for provided categoryId");
        }
        return Ok(response);
    }

    [HttpGet("product-categories/product-types/{typeId}/attributes")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductAttributeKey>> FindProductAttributeKeysForProductType(int typeId)
    {
        var response = _productService.FindProductAttributeKeysForProductType(typeId);
        if (response == null)
        {
            return NotFound("There is no attributes for provided product type id");
        }
        return Ok(response);
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
