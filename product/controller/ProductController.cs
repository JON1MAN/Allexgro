
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

    [HttpGet("product-category")]
    [ProducesResponseType(200)]
    public ActionResult<ProductType> FindAllProductTypes()
    {
        var response = productService.FindAllProductCategories();
        if (response == null)
        {
            return NotFound("There is no product categories");
        }
        return Ok(response);
    }
}
