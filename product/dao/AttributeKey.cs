

public class AttributeKey {
    public int Id {get; set;}
    public string name;

    //todo one to many
    public List<AttributeValue> AttributeValues {get; set;}
}