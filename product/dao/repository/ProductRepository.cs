

using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

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

    public Product? GetProductById(int productId)
    {
        return _context.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductType)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductAttributes)
                .ThenInclude(pa => pa.AttributeKey)
            .FirstOrDefault(p => p.Id == productId);
    }

    public ICollection<ProductCategory> GetProductCategories()
    {
        return _context.ProductCategories.OrderBy(category => category.Name).ToList();
    }

    public ICollection<ProductType> GetProductTypesForProductCategory(int productCategoryId)
    {
        return _context.ProductTypes
            .Where(pt => pt.ProductCategoryId == productCategoryId)
            .ToList();
    }

    public ICollection<ProductAttributeKey> GetProductAttributeKeysForProductType(int productTypeId)
    {
        return _context.ProductAttributeKeys
        .Where(at => at.ProductTypeId == productTypeId)
        .ToList();
    }

    public void SaveProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
}