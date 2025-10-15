namespace EShop.Catalog.Api.Dtos.Requests;

public class ProductCatalogRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureUri { get; set; } = string.Empty;
    public int Quantity { get; set; }
}