using Microsoft.AspNetCore.Mvc;

namespace EShop.Catalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetItemsAsync()
    {
        return Ok("Teste");
    }
}