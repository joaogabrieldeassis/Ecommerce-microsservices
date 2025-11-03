using EShop.Shared.Entities;

namespace EShop.Cart.Domain.AggregatesModel.CartAggregate;

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
        TotalValue = Price;
    }

    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalValue { get; private set; }
    public Cart? Cart { get; private set; } = new();

    public void IncreaseQuantity()
    {
        Quantity++;
        UpdateTotalValue();
    }

    public void DecreaseQuantity()
    {
        Quantity--;
        UpdateTotalValue();
    }

    public void UpdateDetails(string name, int quantityInStock, decimal price)
    {
        Name = name;
        QuantityInStock = quantityInStock;
        Price = price;
        UpdateTotalValue();
    }

    private void UpdateTotalValue()
    {
        TotalValue = Quantity * Price;
    }
}