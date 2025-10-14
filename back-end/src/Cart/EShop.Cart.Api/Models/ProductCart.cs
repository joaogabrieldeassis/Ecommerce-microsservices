using EShop.Shared.Entities;

namespace EShop.Cart.Api.Models;

public class ProductCart : Entity
{

    public string Name { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public decimal Price { get; private set; }
    public Cart Cart { get; private set; } = new();
}