using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/tenants/{tenantId}/page")]
public class PageController : ControllerBase
{
    private readonly InMemoryStore _store;

    public PageController(InMemoryStore store) => _store = store;

    [HttpGet]
    public IActionResult Get(string tenantId)
    {
        if (!_store.Tenants.ContainsKey(tenantId))
            return NotFound(new { message = $"Tenant '{tenantId}' not found." });

        if (!_store.Pages.TryGetValue(tenantId, out var page))
            return NotFound(new { message = $"No page found for tenant '{tenantId}'." });

        return Ok(page);
    }

    [HttpPut]
    public IActionResult Put(string tenantId, [FromBody] PageSchema page)
    {
        if (!_store.Tenants.ContainsKey(tenantId))
            return NotFound(new { message = $"Tenant '{tenantId}' not found." });

        page.TenantId = tenantId;
        if (string.IsNullOrWhiteSpace(page.Id))
            page.Id = Guid.NewGuid().ToString();

        _store.Pages[tenantId] = page;
        return Ok(page);
    }
}
