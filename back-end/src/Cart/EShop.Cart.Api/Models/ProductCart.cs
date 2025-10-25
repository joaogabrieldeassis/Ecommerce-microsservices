namespace EShop.Cart.Api.Models;

public class ProductCart : Entity
{
    public ProductCart() { }
    public ProductCart(Guid productId, string name, int quantityInStock, decimal price)
    {
        ProductId = productId;
        Name = name;
        QuantityInStock = quantityInStock;
        IncreaseQuantity();
        Price = price;
    }

    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Cart Cart { get; private set; } = new();

    public void IncreaseQuantity()
    {
        Quantity++;
    }

    public void DecreaseQuantity()
    {
        Quantity--;
    }
}