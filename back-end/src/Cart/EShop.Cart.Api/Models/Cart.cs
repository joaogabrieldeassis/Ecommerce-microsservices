namespace EShop.Cart.Api.Models;

public class Cart
{
    public Guid UserId { get; private set; }
    public List<ProductCart> Products { get; private set; } = [];
}