namespace EShop.Cart.Api.Models;

public class Product : Entity
{
    public Product() { }
    public Product(string name, int quantityInStock, decimal price)
    {
        Name = name;
        QuantityInStock = quantityInStock;
        Price = price;
    }

    public string Name { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public decimal Price { get; private set; }
}