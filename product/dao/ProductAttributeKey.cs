
public class ProductAttributeKey
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string DataType { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

}
