using EShop.Catalog.Domain.Models;

namespace EShop.Catalog.Domain.Interfaces;

public interface ICatalogRepository : IRepository<ProductCatalog>
{
    Task<IEnumerable<ProductCatalogBrand>> GetAllProductsCatalogBranchAsync();
}