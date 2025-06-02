

using System.Threading.Tasks;

public class ProductRepository : IProductRepository
{

    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Product> GetProducts()
    {
        return _context.Products.OrderBy(product => product.Id).ToList();
    }

    public Product GetProductById(int productId)
    {
        return _context.Products.Find(productId);
    }

    public ICollection<ProductCategory> GetProductCategories()
    {
        return _context.ProductCategories.OrderBy(category => category.Name).ToList();
    }
}