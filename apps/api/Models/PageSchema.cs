using System.Text.Json.Serialization;

namespace Api.Models;

public class PageSchema
{
    public string Id { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string TemplateId { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public SeoMeta Seo { get; set; } = new();
    public ThemeConfig Theme { get; set; } = new();
    public List<Section> Sections { get; set; } = new();
}

public class SeoMeta
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? OgImage { get; set; }
    public string? Canonical { get; set; }
    public bool? NoIndex { get; set; }
}

public class ThemeConfig
{
    public string PrimaryColor { get; set; } = "#6366f1";
    public string SecondaryColor { get; set; } = "#8b5cf6";
    public string? AccentColor { get; set; }
    public string? BackgroundColor { get; set; }
    public string? TextColor { get; set; }
    public string? PrimaryFont { get; set; }
    public string? SecondaryFont { get; set; }
}

public class Section
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public string Label { get; set; } = string.Empty;
    public List<ComponentNode> Components { get; set; } = new();
}

public class ComponentNode
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public string? Label { get; set; }
    public string Path { get; set; } = string.Empty;
    public bool Editable { get; set; }
    public bool? Repeatable { get; set; }
    public ComponentNode? RepeatableTemplate { get; set; }
    public int? MinItems { get; set; }
    public int? MaxItems { get; set; }
    public object? Value { get; set; }
    public TypographyConfig? Typography { get; set; }
    public ColorConfig? Color { get; set; }
    public LinkConfig? Link { get; set; }
    public List<AttributeNode>? Attributes { get; set; }
    public List<ComponentNode>? Children { get; set; }
    public ValidationRules? Validation { get; set; }
}

public class TypographyConfig
{
    public string? FontSize { get; set; }
    public string? FontWeight { get; set; }
    public string? TextAlign { get; set; }
    public string? FontFamily { get; set; }
    public string? LetterSpacing { get; set; }
    public string? LineHeight { get; set; }
    public string? TextTransform { get; set; }
}

public class ColorConfig
{
    public string? Text { get; set; }
    public string? Background { get; set; }
    public string? Border { get; set; }
}

public class LinkConfig
{
    public string Href { get; set; } = string.Empty;
    public string? Target { get; set; }
    public string? Rel { get; set; }
    public string? AriaLabel { get; set; }
}

public class AttributeNode
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "string";
    public object Value { get; set; } = string.Empty;
    public bool Editable { get; set; }
    public string? Label { get; set; }
}

public class ValidationRules
{
    public bool? Required { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public string? Pattern { get; set; }
    public string? ErrorMessage { get; set; }
    public List<string>? AcceptedFileTypes { get; set; }
    public int? MaxFileSize { get; set; }
}

public class ImageValue
{
    public string Src { get; set; } = string.Empty;
    public string Alt { get; set; } = string.Empty;
    public int? Width { get; set; }
    public int? Height { get; set; }
}
