
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService service)
    {
        productService = service;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    public ActionResult<Product> FindById([FromRoute] int id)
    {
        var product = productService.FindById(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet()]
    public ActionResult<List<Product>> FindAll()
    {
        var products = productService.FindAll();
        return Ok(products);
    }

    [HttpGet("product-categories")]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductCategory>> FindAllProductCategories()
    {
        var response = productService.FindAllProductCategories();
        if (response == null)
        {
            return NotFound("There is no product categories");
        }
        return Ok(response);
    }

    [HttpGet("product-categories/{categoryId}/product-types")]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductType>> FindProductTypesByCategory(int categoryId)
    {
        var response = productService.FindProductTypesForProductCategory(categoryId);
        if (response == null)
        {
            return NotFound("There is no product type for provided categoryId");
        }
        return Ok(response);
    }

    [HttpGet("product-categories/product-types/{typeId}/attributes")]
    [ProducesResponseType(200)]
    public ActionResult<List<ProductAttributeKey>> FindProductAttributeKeysForProductType(int typeId)
    {
        var response = productService.FindProductAttributeKeysForProductType(typeId);
        if (response == null)
        {
            return NotFound("There is no attributes for provided product type id");
        }
        return Ok(response);
    }
}
