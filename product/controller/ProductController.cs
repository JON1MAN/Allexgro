
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase{
    private readonly IProductService productService;

    public ProductController(IProductService service) {
        productService = service;
    }

    [HttpGet("{id}")]
    public ActionResult<Product> FindById([FromRoute] int id) {
        var product = productService.FindById(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet()]
    public ActionResult<List<Product>> FindAll() {
        var products = productService.FindAll();
        return Ok(products);
    }

    [HttpPost("{id}")]
    public ActionResult<Product> UpdateProduct(
        [FromRoute] int id,
        Product request
    ) {
        var updatedProduct = productService.UpdateProduct(id, request);
        return Ok(updatedProduct);
    }

}