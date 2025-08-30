namespace EShop.Catalog.Domain.Models;

public class ProductCatalog
{
    public ProductCatalog()
    {

    }
    public ProductCatalog(string name,
                          string description,
                          decimal price,
                          string pictureFileName,
                          string pictureUri,
                          int availableStock,
                          int restockThreshold,
                          int maxStockThreshold,
                          Guid catalogBrandId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        PictureFileName = pictureFileName;
        PictureUri = pictureUri;
        AvailableStock = availableStock;
        RestockThreshold = restockThreshold;
        MaxStockThreshold = maxStockThreshold;
        CatalogBrandId = catalogBrandId;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string PictureFileName { get; private set; } = string.Empty;
    public string PictureUri { get; private set; } = string.Empty;
    public Guid CatalogBrandId { get; private set; }
    public ProductCatalogBrand? ProductCatalogBrand { get; private set; }
    public int AvailableStock { get; private set; }
    public int RestockThreshold { get; private set; }
    public int MaxStockThreshold { get; private set; }

    public void Update(string name, string description, decimal price, string pictureFileName, string pictureUri,
                       Guid catalogBrandId,  int availableStock, int restockThreshold, int maxStockThreshold)
    {
        Name = name;
        Description = description;
        Price = price;
        PictureFileName = pictureFileName;
        PictureUri = pictureUri;
        CatalogBrandId = catalogBrandId;
        AvailableStock = availableStock;
        RestockThreshold = restockThreshold;
        MaxStockThreshold = maxStockThreshold;
    }
}