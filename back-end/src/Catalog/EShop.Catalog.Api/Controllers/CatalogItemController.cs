using EShop.Catalog.Infrestructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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