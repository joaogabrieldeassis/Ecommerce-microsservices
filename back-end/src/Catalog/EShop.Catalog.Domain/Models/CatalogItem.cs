using EShop.Catalog.Domain.Models.Enuns;

namespace EShop.Catalog.Domain.Models;

public class CatalogItem
{
    public CatalogItem()
    {
        
    }
    public CatalogItem(int id, string name, string description, decimal price, string pictureFileName, string pictureUri, int catalogTypeId, int catalogBrandId, int availableStock, int restockThreshold, int maxStockThreshold)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        PictureFileName = pictureFileName;
        PictureUri = pictureUri;
        CatalogTypeId = catalogTypeId;
        CatalogBrandId = catalogBrandId;
        AvailableStock = availableStock;
        RestockThreshold = restockThreshold;
        MaxStockThreshold = maxStockThreshold;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string PictureFileName { get; private set; } = string.Empty;
    public string PictureUri { get; private set; } = string.Empty;
    public int CatalogTypeId { get; private set; }
    public CatalogType CatalogType { get; private set; }
    public int CatalogBrandId { get; private set; }
    public CatalogBrand CatalogBrand { get; private set; } = new();
    public int AvailableStock { get; private set; }
    public int RestockThreshold { get; private set; }
    public int MaxStockThreshold { get; private set; }
    public bool OnReorder { get; private set; }
}
