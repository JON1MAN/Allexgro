
public interface IProductRepository
{
    ICollection<Product> GetProducts();
    Product GetProductById(int productId);
    ICollection<ProductCategory> GetProductCategories();
    ICollection<ProductType> GetProductTypesForProductCategory(int productCategoryId);
    ICollection<ProductAttributeKey> GetProductAttributeKeysForProductType(int productTypeId);
    void SaveProduct(Product product);
    public ICollection<Product> GetProductsForLoggedUser(string sellerId);
    public Product Update(Product product);

}