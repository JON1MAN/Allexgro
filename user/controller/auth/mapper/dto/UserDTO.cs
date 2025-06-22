
public class UserDTO
{
    public string Id { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UpdateStripeAccountLinkUrl { get; set;}
    public string? Description { get; set; }

    //TODO: add discription / phonenumber / createdAt / full name
}