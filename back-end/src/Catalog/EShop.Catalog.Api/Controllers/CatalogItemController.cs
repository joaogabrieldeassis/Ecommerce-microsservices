using EShop.Catalog.Infrestructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly CatalogContext _catalogContext;

    public CatalogController(CatalogContext context)
    {
        _catalogContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    [Route("items")]
    public async Task<IActionResult> GetItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        return Ok(await _catalogContext.CatalogItems
                                       .AsNoTracking()
                                       .OrderBy(c => c.Name)
                                       .Skip(pageSize * pageIndex)
                                       .Take(pageSize)
                                       .ToListAsync());
    }
}