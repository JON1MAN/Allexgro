
public class Product {
    public int Id {get; set;}
    public int SellerId {get; set;}
    public string Name {get; set;}
    public decimal Price {get; set;}
    public int Amount {get; set;}
    public ProductCategory parentCategory {get; set;}
    public ProductCategory subCategory {get; set;}
    public ProductType productType {get; set;}
    public List<ProductAttribute> productAttributes {get; set;}

    public Product(){}

    public Product(int id, string name) {
        Id = id;
        Name = name;
    }
}