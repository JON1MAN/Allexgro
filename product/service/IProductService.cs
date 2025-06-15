
public interface IProductService
{
    public Product FindById(int id);
    public ICollection<Product> FindAll();
    public Product CreateProduct(ProductCreateDTO product, string sellerId);
    public Product UpdateProduct(int id, Product product, string userId);
    public ICollection<ProductCategory> FindAllProductCategories();
    public ICollection<ProductType> FindProductTypesForProductCategory(int productCategoryId);
    public ICollection<ProductAttributeKey> FindProductAttributeKeysForProductType(int productTypeId);
    public ICollection<Product> FindProductsForLoggedUser(string sellerId);
}