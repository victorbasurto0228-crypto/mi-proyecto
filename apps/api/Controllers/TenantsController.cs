using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : ControllerBase
{
    private readonly InMemoryStore _store;

    public TenantsController(InMemoryStore store) => _store = store;

    [HttpGet]
    public IActionResult GetAll() => Ok(_store.Tenants.Values);

    [HttpGet("{tenantId}")]
    public IActionResult GetById(string tenantId)
    {
        if (!_store.Tenants.TryGetValue(tenantId, out var tenant))
            return NotFound(new { message = $"Tenant '{tenantId}' not found." });
        return Ok(tenant);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Tenant tenant)
    {
        if (string.IsNullOrWhiteSpace(tenant.Id))
            return BadRequest(new { message = "Tenant Id is required." });
        if (_store.Tenants.ContainsKey(tenant.Id))
            return Conflict(new { message = $"Tenant '{tenant.Id}' already exists." });
        tenant.CreatedAt = DateTime.UtcNow;
        _store.Tenants[tenant.Id] = tenant;
        return CreatedAtAction(nameof(GetById), new { tenantId = tenant.Id }, tenant);
    }
}
