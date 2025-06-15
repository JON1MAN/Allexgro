
public interface IUserRepository
{
    public ICollection<Product> findMyProducts(string sellerId);
}