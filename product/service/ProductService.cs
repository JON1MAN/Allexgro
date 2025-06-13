
using AutoMapper;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _productMapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _productMapper = mapper;
    }

    public Product CreateProduct(ProductCreateDTO request)
    {
        Product product = _productMapper.Map<Product>(request);
        _productRepository.SaveProduct(product);
        return product;
    }

    public Product FindById(int id)
    {
        return _productRepository.GetProductById(id);
    }

    public ICollection<Product> FindAll()
    {
        return _productRepository.GetProducts();
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