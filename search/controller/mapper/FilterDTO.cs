
public class FilterDTO
{
    public Decimal? minPrice { get; set; }
    public Decimal? maxPrice { get; set; }
    public int? ProductCategoryId { get; set; }
    public int? ProductTypeId { get; set; }
    public ICollection<ProductAttributeDTO>? ProductAttributes { get; set; }
}