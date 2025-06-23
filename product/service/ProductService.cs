
using System.Threading.Tasks;
using AutoMapper;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _productMapper;
    private readonly ILogger<ProductService> _logger;
    private readonly IStripeService _stripeService;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<ProductService> logger,
        IStripeService stripeService
    )
    {
        _productRepository = productRepository;
        _productMapper = mapper;
        _logger = logger;
        _stripeService = stripeService;
    }

    public async Task<Product> CreateProduct(ProductCreateDTO request, string sellerId)
    {
        _logger.LogInformation("Creating a product by user with id: {sellerId}", sellerId);
        Product product = _productMapper.Map<Product>(request);
        product.UserId = sellerId;
        StripeProductDetails stripeProductDetails = await _stripeService.createStripeProduct(product);

        product.StripeProductId = stripeProductDetails.StripeProductId;
        product.StripeProductPriceId = stripeProductDetails.StripeProductPriceId;

        _productRepository.SaveProduct(product);
        return product;
    }

    public Product FindById(int id)
    {
        _logger.LogInformation("Fetching a product with id: {productId}", id);
        return _productRepository.GetProductById(id);
    }

    public ICollection<Product> FindAll()
    {
        _logger.LogInformation("Fetching all products");
        return _productRepository.GetProducts();
    }

    //TODO validation if product belongs to current user, if so - he can update it
    public Product UpdateProduct(int productId, Product request, string userId)
    {
        _logger.LogInformation("Updating a product with id: {productId}, by user: {userId}", productId, userId);
        Product product = request;
        return product;
    }

    public ICollection<ProductCategory> FindAllProductCategories()
    {
        _logger.LogInformation("Fetching all product categories");
        var productCategories = _productRepository.GetProductCategories();

        if (productCategories == null)
        {
            return null;
        }

        return productCategories;
    }

    public ICollection<ProductType> FindProductTypesForProductCategory(int productCategoryId)
    {
        _logger.LogInformation("Fetching all product types for product category: {productCategoryId}", productCategoryId);
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
        _logger.LogInformation("Fetching all product attribute keys for product type: {productTypeId}", productTypeId);
        var ProductAttributeKeys = _productRepository.GetProductAttributeKeysForProductType(productTypeId);

        if (ProductAttributeKeys == null)
        {
            return null;
        }

        return ProductAttributeKeys;
    }

    public ICollection<Product> FindProductsForLoggedUser(string sellerId)
    {
        _logger.LogInformation("Fetching products for logged user: {sellerId}", sellerId);
        return _productRepository.GetProductsForLoggedUser(sellerId);
    }

    public void UpdateProductAmounts(ShoppingCart shoppingCart)
    {
        _logger.LogInformation("Updating amounts for each product from shoppiong cart");
        var shoppingCartProducts = shoppingCart.Products;

        foreach (var shoppingCartProduct in shoppingCartProducts)
        {
            Product product = FindById(shoppingCartProduct.Id);
            product.Amount = product.Amount - shoppingCartProduct.Amount;

            _productRepository.Update(product);
        }
    }
}
