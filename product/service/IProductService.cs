
public interface IProductService
{
    public Product FindById(int id);
    public List<Product> FindAll();
    public Product CreateProduct(Product product);
    public Product UpdateProduct(int id, Product product);
    public ICollection<ProductCategory> FindAllProductCategories();
}