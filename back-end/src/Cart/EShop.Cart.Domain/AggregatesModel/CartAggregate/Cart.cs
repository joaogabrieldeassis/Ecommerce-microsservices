using EShop.Shared.Entities;
using EShop.Shared.Interfaces;

namespace EShop.Cart.Domain.AggregatesModel.CartAggregate;

public class Cart : Entity, IAggregateRoot
{
    public Cart() { }
    public Cart(Guid userId)
    {
        UserId = userId;
        IsDeleted = false;
    }

    public Guid UserId { get; private set; }
    public bool IsDeleted { get; private set; }
    public List<ProductCart> Products { get; private set; } = [];
    public decimal TotalValue { get; private set; }

    public void AddProduct(ProductCart product)
    {
        Products.Add(product);
        UpdateTotalValue();
    }

    public void RemoveProduct(ProductCart product)
    {
        Products.Remove(product);
        UpdateTotalValue();
    }

    public void IncreaseQuantityProduct(Guid productId)
    {
        var product = Products.FirstOrDefault(p => p.ProductId == productId);
        product!.IncreaseQuantity();

        UpdateTotalValue();
    }

    public void DecreaseQuantityProduct(Guid productId)
    {
        var product = Products.FirstOrDefault(p => p.ProductId == productId);
        product!.DecreaseQuantity();

        UpdateTotalValue();
    }

    private void UpdateTotalValue()
    {
        TotalValue = Products.Sum(p => p.TotalValue);
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}