using EShop.Shared.Entities;

namespace EShop.Catalog.Domain.Models;

public class ProductCatalog : Entity
{
    public ProductCatalog() { }
    public ProductCatalog(string name, string description, decimal price, string pictureUri, int quantity)
    {
        Name = name;
        Description = description;
        Price = price;
        PictureUri = pictureUri;
        QuantityInStock = quantity;
    }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string PictureUri { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }

    public void Update(string name, string description, decimal price, string pictureUri, int quantity)
    {
        Name = name;
        Description = description;
        Price = price;
        PictureUri = pictureUri;
        QuantityInStock = quantity;
    }

    public void AddOneToStock()
    {
        QuantityInStock++;
    }

    public void RemoveOneFromStock()
    {
        if (QuantityInStock < 1)
            throw new InvalidOperationException("Não tem como remover um estoque pois o produto não tem estoque.");

        QuantityInStock--;
    }
}