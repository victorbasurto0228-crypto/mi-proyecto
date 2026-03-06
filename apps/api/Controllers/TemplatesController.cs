using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/templates")]
public class TemplatesController : ControllerBase
{
    private readonly InMemoryStore _store;

    public TemplatesController(InMemoryStore store) => _store = store;

    [HttpGet]
    public IActionResult GetAll() => Ok(_store.Templates);

    [HttpGet("{templateId}")]
    public IActionResult GetById(string templateId)
    {
        var template = _store.Templates.FirstOrDefault(t => t.Id == templateId);
        if (template is null)
            return NotFound(new { message = $"Template '{templateId}' not found." });
        return Ok(template);
    }
}
