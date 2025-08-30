namespace EShop.Catalog.Api.Dtos.Requests;

public class ProductCatalogRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = string.Empty;
    public string PictureUri { get; set; } = string.Empty;
    public Guid CatalogBrandId { get; set; }
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
}