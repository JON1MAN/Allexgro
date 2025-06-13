
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

    public ProductController(IProductService service, IMapper productMapper)
    {
        _productService = service;
        _productMapper = productMapper;
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

        var response = new List<ProductDTO>();

        foreach (var product in products)
        {
            response.Add(_productMapper.Map<ProductDTO>(product));
        }

        return Ok(products);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<ProductDTO> CreateProduct([FromBody] ProductCreateDTO request)
    {
        var response = _productService.CreateProduct(request);
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
}
