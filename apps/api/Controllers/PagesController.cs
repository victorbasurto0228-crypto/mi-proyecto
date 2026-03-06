using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/tenants/{tenantId}/pages")]
public class PagesController : ControllerBase
{
    private readonly InMemoryStore _store;

    public PagesController(InMemoryStore store) => _store = store;

    [HttpGet]
    public IActionResult GetAll(string tenantId)
    {
        var pages = _store.Pages.Values.Where(p => p.TenantId == tenantId).ToList();
        return Ok(pages);
    }

    [HttpGet("{slug}")]
    public IActionResult GetBySlug(string tenantId, string slug)
    {
        var key = _store.PageKey(tenantId, slug);
        if (!_store.Pages.TryGetValue(key, out var page))
            return NotFound(new { message = $"Page '{slug}' not found for tenant '{tenantId}'." });
        return Ok(page);
    }

    [HttpPost]
    public IActionResult Create(string tenantId, [FromBody] PageSchema page)
    {
        if (string.IsNullOrWhiteSpace(page.Slug))
            return BadRequest(new { message = "Page Slug is required." });

        page.TenantId = tenantId;
        var key = _store.PageKey(tenantId, page.Slug);

        if (_store.Pages.ContainsKey(key))
            return Conflict(new { message = $"Page '{page.Slug}' already exists for tenant '{tenantId}'." });

        if (string.IsNullOrWhiteSpace(page.Id))
            page.Id = Guid.NewGuid().ToString();

        _store.Pages[key] = page;
        return CreatedAtAction(nameof(GetBySlug), new { tenantId, slug = page.Slug }, page);
    }

    [HttpPut("{slug}")]
    public IActionResult Update(string tenantId, string slug, [FromBody] PageSchema page)
    {
        var key = _store.PageKey(tenantId, slug);
        page.TenantId = tenantId;
        page.Slug = slug;
        _store.Pages[key] = page;
        return Ok(page);
    }

    [HttpDelete("{slug}")]
    public IActionResult Delete(string tenantId, string slug)
    {
        var key = _store.PageKey(tenantId, slug);
        if (!_store.Pages.Remove(key))
            return NotFound(new { message = $"Page '{slug}' not found for tenant '{tenantId}'." });
        return NoContent();
    }
}
