
public interface IProductService
{
    public Product FindById(int id);
    public ICollection<Product> FindAll();
    public Product CreateProduct(ProductCreateDTO product);
    public Product UpdateProduct(int id, Product product);
    public ICollection<ProductCategory> FindAllProductCategories();
    public ICollection<ProductType> FindProductTypesForProductCategory(int productCategoryId);
    public ICollection<ProductAttributeKey> FindProductAttributeKeysForProductType(int productTypeId);
}