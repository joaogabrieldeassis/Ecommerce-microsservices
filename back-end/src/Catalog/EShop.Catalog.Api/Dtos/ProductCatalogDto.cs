using EShop.Catalog.Domain.Models.Enuns;

namespace EShop.Catalog.Api.Dtos;

public class ProductCatalogDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PictureFileName { get; set; } = string.Empty;
    public string PictureUri { get; set; } = string.Empty;
    public int CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; }
    public int CatalogBrandId { get; set; }
    public ProductCatalogBrandDto ProductCatalogBrandDto { get; set; } = new();
    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
    public bool OnReorder { get; set; }
}