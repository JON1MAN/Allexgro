
public class ProductService : IProductService
{
    public Product CreateProduct(Product request)
    {
        Product product = request;
        return product;
    }

    public Product FindById(int id)
    {
        Product product = new Product();
        product.Id = 1;
        return product;
    }

    public List<Product> FindAll()
    {
        List<Product> products = new List<Product>{
            new Product(1, "keyborad"),
            new Product(2, "laptop"),
            new Product(3, "nike air force"),
            new Product(4, "white table"),
            new Product(5, "baseball cap")
        };
        return products;
    }

    public Product UpdateProduct(int productId, Product request)
    {
        Product product = request;
        return product;
    }
}