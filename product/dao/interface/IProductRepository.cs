
public interface IProductRepository
{
    ICollection<Product> GetProducts();
    Product GetProductById(int productId);
    ICollection<ProductCategory> GetProductCategories();

}