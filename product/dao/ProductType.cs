
public class ProductType {
    public int Id {get; set;}
    public String Name {get; set;}
    public int ParentCategoryId {get; set;}
    public int SubCategoryId {get; set;}

    //TODO one to many
    public List<AttributeKey> attributeKeys;
}