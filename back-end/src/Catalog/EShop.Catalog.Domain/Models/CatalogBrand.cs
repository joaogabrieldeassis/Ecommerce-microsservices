namespace EShop.Catalog.Domain.Models;

public class CatalogBrand
{
    public CatalogBrand(Guid id, string brand)
    {
        Id = id;
        Brand = brand;
    }
    public CatalogBrand() { }

    public Guid Id { get; private set; }
    public string Brand { get; private set; } = string.Empty;
}