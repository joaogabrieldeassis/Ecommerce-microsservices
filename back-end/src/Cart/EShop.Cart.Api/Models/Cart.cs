namespace EShop.Cart.Api.Models;

public class Cart : Entity
{
    public Cart() { }
    public Cart(Guid userId)
    {
        UserId = userId;
        IsDeleted = true;
    }

    public Guid UserId { get; private set; }
    public bool IsDeleted { get; private set; }
    public List<ProductCart> Products { get; private set; } = [];

    public void AddProduct(ProductCart product)
    {
        Products.Add(product);
    }

    public void RemoveProduct(ProductCart product)
    {
        Products.Remove(product);
    }

    public bool HasProduct()
    {
        return Products != null && Products.Count > 0;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}