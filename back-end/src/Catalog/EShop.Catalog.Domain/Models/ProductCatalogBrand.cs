namespace EShop.Catalog.Domain.Models;

public class ProductCatalogBrand
{
    public ProductCatalogBrand(Guid id, string brand)
    {
        Id = id;
        Brand = brand;
    }
    public ProductCatalogBrand() { }

    public Guid Id { get; private set; }
    public string Brand { get; private set; } = string.Empty;
}