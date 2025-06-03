
public class ProductAttributeKey
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string DataType { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public bool IsPredefined { get; set; }
    public ICollection<string>? AllowedValues { get; set; }

}
