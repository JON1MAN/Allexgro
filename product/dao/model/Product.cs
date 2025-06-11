public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public int ProductCategoryId { get; set; }
    public int ProductTypeId { get; set; }
    public ProductCategory ProductCategory { get; set; } = null!;
    public ProductType ProductType { get; set; } = null!;
    public ICollection<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    public int UserId;

    public Product() { }

    public Product(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
