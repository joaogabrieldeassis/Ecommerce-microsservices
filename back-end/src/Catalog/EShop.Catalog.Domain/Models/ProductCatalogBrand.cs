namespace EShop.Catalog.Domain.Models;

public class ProductCatalogBrand
{
    public ProductCatalogBrand(string brand)
    {
        Id = Guid.NewGuid();
        Brand = brand;
    }
    public ProductCatalogBrand() { }

    public Guid Id { get; private set; }
    public string Brand { get; private set; } = string.Empty;
}