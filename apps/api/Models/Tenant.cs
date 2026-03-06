namespace Api.Models;

public class Tenant
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Subdomain { get; set; } = string.Empty;
    public string Plan { get; set; } = "free";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
