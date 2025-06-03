
public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

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

    public ICollection<ProductCategory> FindAllProductCategories()
    {
        var productCategories = _productRepository.GetProductCategories();

        if (productCategories == null)
        {
            return null;
        }

        return productCategories;
    }

    public ICollection<ProductType> FindProductTypesForProductCategory(int productCategoryId)
    {
        var productTypes = _productRepository.GetProductTypesForProductCategory(productCategoryId);

        if (productTypes == null)
        {
            Console.WriteLine("null");
            return null;
        }

        return productTypes;
    }

    public ICollection<ProductAttributeKey> FindProductAttributeKeysForProductType(int productTypeId)
    {
        var ProductAttributeKeys = _productRepository.GetProductAttributeKeysForProductType(productTypeId);

        if (ProductAttributeKeys == null)
        {
            Console.WriteLine("null");
            return null;
        }

        return ProductAttributeKeys;
    }
}