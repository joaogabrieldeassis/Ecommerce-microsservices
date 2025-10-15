namespace EShop.Cart.Api.Models;

public class ProductCart : Entity
{
    public ProductCart() { }
    public ProductCart(string name, int quantityInStock, int quantity, decimal price, Cart cart)
    {
        Name = name;
        QuantityInStock = quantityInStock;
        Quantity = quantity;
        Price = price;
        Cart = cart;
    }

    public string Name { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Cart Cart { get; private set; } = new();
}