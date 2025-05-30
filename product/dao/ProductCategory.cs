
public class ProductCategory {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public ProductCategory ParentCategory { get; set; }
    public List<ProductCategory> SubCategories { get; set; }
}
