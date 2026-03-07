using Api.Models;

namespace Api.Services;

public class InMemoryStore
{
    public Dictionary<string, Tenant> Tenants { get; } = new();
    public Dictionary<string, PageSchema> Pages { get; } = new();
    public List<Template> Templates { get; } = new();

    public InMemoryStore()
    {
        Seed();
    }

    private void Seed()
    {
        // Templates
        Templates.Add(new Template
        {
            Id = "generic-saas",
            Name = "Generic SaaS",
            Description = "A modern SaaS landing page template with hero, features, testimonials, pricing, CTA, and footer.",
            PreviewImage = "https://placehold.co/800x500?text=Generic+SaaS",
            Category = "saas"
        });

        Templates.Add(new Template
        {
            Id = "restaurante-uno",
            Name = "Restaurante",
            Description = "Template de landing page para restaurantes con hero, menú, nosotros, contacto y footer.",
            PreviewImage = "https://placehold.co/800x500?text=Restaurante",
            Category = "restaurant"
        });

        Templates.Add(new Template
        {
            Id = "organizador-eventos",
            Name = "Organizador de Eventos",
            Description = "Template para organizadores de eventos con hero, servicios, galería, testimonios y contacto.",
            PreviewImage = "https://placehold.co/800x500?text=Organizador+Eventos",
            Category = "events"
        });

        // Tenants
        Tenants["demo"] = new Tenant
        {
            Id = "demo",
            Name = "Demo Company",
            Subdomain = "demo",
            Plan = "pro",
            CreatedAt = DateTime.UtcNow
        };

        Tenants["bella-italia"] = new Tenant
        {
            Id = "bella-italia",
            Name = "Bella Italia Ristorante",
            Subdomain = "bella-italia",
            Plan = "pro",
            CreatedAt = DateTime.UtcNow
        };

        Tenants["eventos-mx"] = new Tenant
        {
            Id = "eventos-mx",
            Name = "Eventos MX",
            Subdomain = "eventos-mx",
            Plan = "pro",
            CreatedAt = DateTime.UtcNow
        };

        // Pages – keyed by tenantId (one page per tenant)
        Pages["demo"] = BuildDemoPage();
        Pages["bella-italia"] = BuildBellaItaliaPage();
        Pages["eventos-mx"] = BuildEventosMxPage();
    }

    private static PageSchema BuildDemoPage()
    {
        return new PageSchema
        {
            Id = "demo-index",
            Slug = "index",
            TemplateId = "generic-saas",
            TenantId = "demo",
            Seo = new SeoMeta
            {
                Title = "Demo Company – The best SaaS solution",
                Description = "Boost your productivity with Demo Company. The all-in-one platform for modern teams.",
                OgImage = "https://placehold.co/1200x630?text=Demo+Company"
            },
            Theme = new ThemeConfig
            {
                PrimaryColor = "#6366f1",
                SecondaryColor = "#8b5cf6",
                AccentColor = "#f59e0b",
                BackgroundColor = "#ffffff",
                TextColor = "#111827",
                PrimaryFont = "Inter",
                SecondaryFont = "Inter"
            },
            Sections = new List<Section>
            {
                // ─── HEADER ───────────────────────────────────────────
                new Section
                {
                    Id = "header",
                    Type = "header",
                    Order = 0,
                    Visible = true,
                    Label = "Header",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode
                        {
                            Id = "logo",
                            Type = "image",
                            Order = 0,
                            Visible = true,
                            Label = "Logo",
                            Path = "header.logo",
                            Editable = true,
                            Value = new ImageValue { Src = "https://placehold.co/120x40?text=Logo", Alt = "Demo Company Logo", Width = 120, Height = 40 }
                        },
                        new ComponentNode
                        {
                            Id = "nav",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Navigation",
                            Path = "header.nav",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 8,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "nav-item-tpl",
                                Type = "nav-item",
                                Order = 0,
                                Visible = true,
                                Label = "Nav Item",
                                Path = "header.nav[*]",
                                Editable = true,
                                Value = "Link",
                                Link = new LinkConfig { Href = "#", Target = "_self" }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "nav-1", Type = "nav-item", Order = 0, Visible = true, Label = "Features", Path = "header.nav[0]", Editable = true, Value = "Features", Link = new LinkConfig { Href = "#features", Target = "_self" } },
                                new ComponentNode { Id = "nav-2", Type = "nav-item", Order = 1, Visible = true, Label = "Testimonials", Path = "header.nav[1]", Editable = true, Value = "Testimonials", Link = new LinkConfig { Href = "#testimonials", Target = "_self" } },
                                new ComponentNode { Id = "nav-3", Type = "nav-item", Order = 2, Visible = true, Label = "Pricing", Path = "header.nav[2]", Editable = true, Value = "Pricing", Link = new LinkConfig { Href = "#pricing", Target = "_self" } },
                                new ComponentNode { Id = "nav-cta", Type = "button", Order = 3, Visible = true, Label = "CTA Button", Path = "header.navCta", Editable = true, Value = "Get Started", Link = new LinkConfig { Href = "#pricing", Target = "_self" } }
                            }
                        }
                    }
                },

                // ─── HERO ─────────────────────────────────────────────
                new Section
                {
                    Id = "hero",
                    Type = "hero",
                    Order = 1,
                    Visible = true,
                    Label = "Hero",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "hero-badge", Type = "badge", Order = 0, Visible = true, Label = "Badge", Path = "hero.badge", Editable = true, Value = "🚀 Now in public beta" },
                        new ComponentNode
                        {
                            Id = "hero-title",
                            Type = "heading",
                            Order = 1,
                            Visible = true,
                            Label = "Title",
                            Path = "hero.title",
                            Editable = true,
                            Value = "The all-in-one platform for modern teams",
                            Typography = new TypographyConfig { FontSize = "5xl", FontWeight = "bold", TextAlign = "center" }
                        },
                        new ComponentNode
                        {
                            Id = "hero-subtitle",
                            Type = "paragraph",
                            Order = 2,
                            Visible = true,
                            Label = "Subtitle",
                            Path = "hero.subtitle",
                            Editable = true,
                            Value = "Streamline your workflow, collaborate in real time, and ship faster than ever. Trusted by 10,000+ teams worldwide.",
                            Typography = new TypographyConfig { FontSize = "xl", TextAlign = "center" }
                        },
                        new ComponentNode { Id = "hero-cta-primary", Type = "button", Order = 3, Visible = true, Label = "Primary CTA", Path = "hero.ctaPrimary", Editable = true, Value = "Start for free", Link = new LinkConfig { Href = "#pricing", Target = "_self" } },
                        new ComponentNode { Id = "hero-cta-secondary", Type = "button", Order = 4, Visible = true, Label = "Secondary CTA", Path = "hero.ctaSecondary", Editable = true, Value = "Watch demo", Link = new LinkConfig { Href = "#demo", Target = "_self" } },
                        new ComponentNode
                        {
                            Id = "hero-image",
                            Type = "image",
                            Order = 5,
                            Visible = true,
                            Label = "Hero Image",
                            Path = "hero.image",
                            Editable = true,
                            Value = new ImageValue { Src = "https://placehold.co/1200x600?text=Dashboard+Preview", Alt = "Dashboard Preview", Width = 1200, Height = 600 }
                        }
                    }
                },

                // ─── FEATURES ─────────────────────────────────────────
                new Section
                {
                    Id = "features",
                    Type = "features",
                    Order = 2,
                    Visible = true,
                    Label = "Features",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "features-title", Type = "heading", Order = 0, Visible = true, Label = "Section Title", Path = "features.title", Editable = true, Value = "Everything you need to succeed", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "features-subtitle", Type = "paragraph", Order = 1, Visible = true, Label = "Section Subtitle", Path = "features.subtitle", Editable = true, Value = "Our platform gives your team the tools to move fast and stay aligned.", Typography = new TypographyConfig { TextAlign = "center" } },
                        new ComponentNode
                        {
                            Id = "features-grid",
                            Type = "grid",
                            Order = 2,
                            Visible = true,
                            Label = "Feature Cards",
                            Path = "features.cards",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 9,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "feature-card-tpl",
                                Type = "feature-card",
                                Order = 0,
                                Visible = true,
                                Label = "Feature Card",
                                Path = "features.cards[*]",
                                Editable = true,
                                Children = new List<ComponentNode>
                                {
                                    new ComponentNode { Id = "fc-icon-tpl", Type = "icon", Order = 0, Visible = true, Label = "Icon", Path = "features.cards[*].icon", Editable = true, Value = "⚡" },
                                    new ComponentNode { Id = "fc-title-tpl", Type = "heading", Order = 1, Visible = true, Label = "Title", Path = "features.cards[*].title", Editable = true, Value = "Feature Title", Typography = new TypographyConfig { FontWeight = "semibold" } },
                                    new ComponentNode { Id = "fc-desc-tpl", Type = "paragraph", Order = 2, Visible = true, Label = "Description", Path = "features.cards[*].description", Editable = true, Value = "Feature description goes here." }
                                }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "fc-1", Type = "feature-card", Order = 0, Visible = true, Label = "Feature 1", Path = "features.cards[0]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "fc-1-icon", Type = "icon", Order = 0, Visible = true, Label = "Icon", Path = "features.cards[0].icon", Editable = true, Value = "⚡" },
                                    new ComponentNode { Id = "fc-1-title", Type = "heading", Order = 1, Visible = true, Label = "Title", Path = "features.cards[0].title", Editable = true, Value = "Lightning Fast", Typography = new TypographyConfig { FontWeight = "semibold" } },
                                    new ComponentNode { Id = "fc-1-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Description", Path = "features.cards[0].description", Editable = true, Value = "Optimized for speed. Our platform handles millions of requests without breaking a sweat." }
                                }},
                                new ComponentNode { Id = "fc-2", Type = "feature-card", Order = 1, Visible = true, Label = "Feature 2", Path = "features.cards[1]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "fc-2-icon", Type = "icon", Order = 0, Visible = true, Label = "Icon", Path = "features.cards[1].icon", Editable = true, Value = "🔒" },
                                    new ComponentNode { Id = "fc-2-title", Type = "heading", Order = 1, Visible = true, Label = "Title", Path = "features.cards[1].title", Editable = true, Value = "Enterprise Security", Typography = new TypographyConfig { FontWeight = "semibold" } },
                                    new ComponentNode { Id = "fc-2-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Description", Path = "features.cards[1].description", Editable = true, Value = "SOC 2 compliant with end-to-end encryption. Your data is always safe with us." }
                                }},
                                new ComponentNode { Id = "fc-3", Type = "feature-card", Order = 2, Visible = true, Label = "Feature 3", Path = "features.cards[2]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "fc-3-icon", Type = "icon", Order = 0, Visible = true, Label = "Icon", Path = "features.cards[2].icon", Editable = true, Value = "🤝" },
                                    new ComponentNode { Id = "fc-3-title", Type = "heading", Order = 1, Visible = true, Label = "Title", Path = "features.cards[2].title", Editable = true, Value = "Real-time Collaboration", Typography = new TypographyConfig { FontWeight = "semibold" } },
                                    new ComponentNode { Id = "fc-3-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Description", Path = "features.cards[2].description", Editable = true, Value = "Work together seamlessly. See live updates, comments, and changes from your entire team." }
                                }}
                            }
                        }
                    }
                },

                // ─── TESTIMONIALS ─────────────────────────────────────
                new Section
                {
                    Id = "testimonials",
                    Type = "testimonials",
                    Order = 3,
                    Visible = true,
                    Label = "Testimonials",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "test-title", Type = "heading", Order = 0, Visible = true, Label = "Section Title", Path = "testimonials.title", Editable = true, Value = "Loved by teams everywhere", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode
                        {
                            Id = "test-list",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Testimonials",
                            Path = "testimonials.items",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 12,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "test-card-tpl",
                                Type = "testimonial-card",
                                Order = 0,
                                Visible = true,
                                Label = "Testimonial",
                                Path = "testimonials.items[*]",
                                Editable = true,
                                Children = new List<ComponentNode>
                                {
                                    new ComponentNode { Id = "tc-quote-tpl", Type = "paragraph", Order = 0, Visible = true, Label = "Quote", Path = "testimonials.items[*].quote", Editable = true, Value = "This product changed our workflow completely." },
                                    new ComponentNode { Id = "tc-avatar-tpl", Type = "image", Order = 1, Visible = true, Label = "Avatar", Path = "testimonials.items[*].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/64x64?text=AV", Alt = "Avatar", Width = 64, Height = 64 } },
                                    new ComponentNode { Id = "tc-name-tpl", Type = "text", Order = 2, Visible = true, Label = "Name", Path = "testimonials.items[*].name", Editable = true, Value = "Jane Doe" },
                                    new ComponentNode { Id = "tc-role-tpl", Type = "text", Order = 3, Visible = true, Label = "Role", Path = "testimonials.items[*].role", Editable = true, Value = "CEO at Company" }
                                }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "tc-1", Type = "testimonial-card", Order = 0, Visible = true, Label = "Testimonial 1", Path = "testimonials.items[0]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "tc-1-quote", Type = "paragraph", Order = 0, Visible = true, Label = "Quote", Path = "testimonials.items[0].quote", Editable = true, Value = "This platform transformed how our team works. We ship twice as fast as before!" },
                                    new ComponentNode { Id = "tc-1-avatar", Type = "image", Order = 1, Visible = true, Label = "Avatar", Path = "testimonials.items[0].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/64x64?text=SM", Alt = "Sarah M.", Width = 64, Height = 64 } },
                                    new ComponentNode { Id = "tc-1-name", Type = "text", Order = 2, Visible = true, Label = "Name", Path = "testimonials.items[0].name", Editable = true, Value = "Sarah Mitchell" },
                                    new ComponentNode { Id = "tc-1-role", Type = "text", Order = 3, Visible = true, Label = "Role", Path = "testimonials.items[0].role", Editable = true, Value = "CTO at Acme Corp" }
                                }},
                                new ComponentNode { Id = "tc-2", Type = "testimonial-card", Order = 1, Visible = true, Label = "Testimonial 2", Path = "testimonials.items[1]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "tc-2-quote", Type = "paragraph", Order = 0, Visible = true, Label = "Quote", Path = "testimonials.items[1].quote", Editable = true, Value = "Incredible product. Our customer satisfaction went up 40% in the first month." },
                                    new ComponentNode { Id = "tc-2-avatar", Type = "image", Order = 1, Visible = true, Label = "Avatar", Path = "testimonials.items[1].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/64x64?text=JR", Alt = "James R.", Width = 64, Height = 64 } },
                                    new ComponentNode { Id = "tc-2-name", Type = "text", Order = 2, Visible = true, Label = "Name", Path = "testimonials.items[1].name", Editable = true, Value = "James Rodriguez" },
                                    new ComponentNode { Id = "tc-2-role", Type = "text", Order = 3, Visible = true, Label = "Role", Path = "testimonials.items[1].role", Editable = true, Value = "Head of Product at Startup" }
                                }},
                                new ComponentNode { Id = "tc-3", Type = "testimonial-card", Order = 2, Visible = true, Label = "Testimonial 3", Path = "testimonials.items[2]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "tc-3-quote", Type = "paragraph", Order = 0, Visible = true, Label = "Quote", Path = "testimonials.items[2].quote", Editable = true, Value = "Simple, powerful, and beautifully designed. Exactly what we needed." },
                                    new ComponentNode { Id = "tc-3-avatar", Type = "image", Order = 1, Visible = true, Label = "Avatar", Path = "testimonials.items[2].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/64x64?text=EL", Alt = "Emma L.", Width = 64, Height = 64 } },
                                    new ComponentNode { Id = "tc-3-name", Type = "text", Order = 2, Visible = true, Label = "Name", Path = "testimonials.items[2].name", Editable = true, Value = "Emma Liu" },
                                    new ComponentNode { Id = "tc-3-role", Type = "text", Order = 3, Visible = true, Label = "Role", Path = "testimonials.items[2].role", Editable = true, Value = "Founder at TechFlow" }
                                }}
                            }
                        }
                    }
                },

                // ─── PRICING ──────────────────────────────────────────
                new Section
                {
                    Id = "pricing",
                    Type = "pricing",
                    Order = 4,
                    Visible = true,
                    Label = "Pricing",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "pricing-title", Type = "heading", Order = 0, Visible = true, Label = "Section Title", Path = "pricing.title", Editable = true, Value = "Simple, transparent pricing", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "pricing-subtitle", Type = "paragraph", Order = 1, Visible = true, Label = "Section Subtitle", Path = "pricing.subtitle", Editable = true, Value = "No hidden fees. Cancel anytime.", Typography = new TypographyConfig { TextAlign = "center" } },
                        new ComponentNode
                        {
                            Id = "pricing-cards",
                            Type = "grid",
                            Order = 2,
                            Visible = true,
                            Label = "Pricing Cards",
                            Path = "pricing.cards",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 5,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "pricing-card-tpl",
                                Type = "pricing-card",
                                Order = 0,
                                Visible = true,
                                Label = "Pricing Card",
                                Path = "pricing.cards[*]",
                                Editable = true,
                                Children = new List<ComponentNode>
                                {
                                    new ComponentNode { Id = "pc-name-tpl", Type = "text", Order = 0, Visible = true, Label = "Plan Name", Path = "pricing.cards[*].name", Editable = true, Value = "Plan" },
                                    new ComponentNode { Id = "pc-price-tpl", Type = "text", Order = 1, Visible = true, Label = "Price", Path = "pricing.cards[*].price", Editable = true, Value = "$0/mo" },
                                    new ComponentNode { Id = "pc-features-tpl", Type = "list", Order = 2, Visible = true, Label = "Features", Path = "pricing.cards[*].features", Editable = true, Value = "Feature 1\nFeature 2\nFeature 3" },
                                    new ComponentNode { Id = "pc-cta-tpl", Type = "button", Order = 3, Visible = true, Label = "CTA", Path = "pricing.cards[*].cta", Editable = true, Value = "Get started", Link = new LinkConfig { Href = "#", Target = "_self" } }
                                }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "pc-1", Type = "pricing-card", Order = 0, Visible = true, Label = "Starter Plan", Path = "pricing.cards[0]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "pc-1-name", Type = "text", Order = 0, Visible = true, Label = "Plan Name", Path = "pricing.cards[0].name", Editable = true, Value = "Starter" },
                                    new ComponentNode { Id = "pc-1-price", Type = "text", Order = 1, Visible = true, Label = "Price", Path = "pricing.cards[0].price", Editable = true, Value = "$0/mo" },
                                    new ComponentNode { Id = "pc-1-features", Type = "list", Order = 2, Visible = true, Label = "Features", Path = "pricing.cards[0].features", Editable = true, Value = "Up to 3 users\n5 projects\n1 GB storage\nEmail support" },
                                    new ComponentNode { Id = "pc-1-cta", Type = "button", Order = 3, Visible = true, Label = "CTA", Path = "pricing.cards[0].cta", Editable = true, Value = "Get started free", Link = new LinkConfig { Href = "#", Target = "_self" } }
                                }},
                                new ComponentNode { Id = "pc-2", Type = "pricing-card", Order = 1, Visible = true, Label = "Pro Plan", Path = "pricing.cards[1]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "pc-2-name", Type = "text", Order = 0, Visible = true, Label = "Plan Name", Path = "pricing.cards[1].name", Editable = true, Value = "Pro" },
                                    new ComponentNode { Id = "pc-2-price", Type = "text", Order = 1, Visible = true, Label = "Price", Path = "pricing.cards[1].price", Editable = true, Value = "$29/mo" },
                                    new ComponentNode { Id = "pc-2-features", Type = "list", Order = 2, Visible = true, Label = "Features", Path = "pricing.cards[1].features", Editable = true, Value = "Up to 20 users\nUnlimited projects\n50 GB storage\nPriority support\nAdvanced analytics" },
                                    new ComponentNode { Id = "pc-2-cta", Type = "button", Order = 3, Visible = true, Label = "CTA", Path = "pricing.cards[1].cta", Editable = true, Value = "Start free trial", Link = new LinkConfig { Href = "#", Target = "_self" } }
                                }},
                                new ComponentNode { Id = "pc-3", Type = "pricing-card", Order = 2, Visible = true, Label = "Enterprise Plan", Path = "pricing.cards[2]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "pc-3-name", Type = "text", Order = 0, Visible = true, Label = "Plan Name", Path = "pricing.cards[2].name", Editable = true, Value = "Enterprise" },
                                    new ComponentNode { Id = "pc-3-price", Type = "text", Order = 1, Visible = true, Label = "Price", Path = "pricing.cards[2].price", Editable = true, Value = "$99/mo" },
                                    new ComponentNode { Id = "pc-3-features", Type = "list", Order = 2, Visible = true, Label = "Features", Path = "pricing.cards[2].features", Editable = true, Value = "Unlimited users\nUnlimited projects\n500 GB storage\n24/7 dedicated support\nAdvanced analytics\nSSO & SAML\nCustom integrations" },
                                    new ComponentNode { Id = "pc-3-cta", Type = "button", Order = 3, Visible = true, Label = "CTA", Path = "pricing.cards[2].cta", Editable = true, Value = "Contact sales", Link = new LinkConfig { Href = "#contact", Target = "_self" } }
                                }}
                            }
                        }
                    }
                },

                // ─── CTA ──────────────────────────────────────────────
                new Section
                {
                    Id = "cta",
                    Type = "cta",
                    Order = 5,
                    Visible = true,
                    Label = "CTA",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "cta-title", Type = "heading", Order = 0, Visible = true, Label = "Title", Path = "cta.title", Editable = true, Value = "Ready to get started?", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "cta-desc", Type = "paragraph", Order = 1, Visible = true, Label = "Description", Path = "cta.description", Editable = true, Value = "Join thousands of teams already using our platform. No credit card required.", Typography = new TypographyConfig { TextAlign = "center" } },
                        new ComponentNode { Id = "cta-button", Type = "button", Order = 2, Visible = true, Label = "Button", Path = "cta.button", Editable = true, Value = "Start for free today", Link = new LinkConfig { Href = "#pricing", Target = "_self" } }
                    }
                },

                // ─── FOOTER ───────────────────────────────────────────
                new Section
                {
                    Id = "footer",
                    Type = "footer",
                    Order = 6,
                    Visible = true,
                    Label = "Footer",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "footer-copy", Type = "text", Order = 0, Visible = true, Label = "Copyright", Path = "footer.copyright", Editable = true, Value = "© 2024 Demo Company. All rights reserved." },
                        new ComponentNode
                        {
                            Id = "footer-socials",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Social Links",
                            Path = "footer.socials",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 0,
                            MaxItems = 10,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "social-twitter", Type = "social-link", Order = 0, Visible = true, Label = "Twitter/X", Path = "footer.socials[0]", Editable = true, Value = "Twitter", Link = new LinkConfig { Href = "https://twitter.com", Target = "_blank", Rel = "noopener noreferrer" } },
                                new ComponentNode { Id = "social-github", Type = "social-link", Order = 1, Visible = true, Label = "GitHub", Path = "footer.socials[1]", Editable = true, Value = "GitHub", Link = new LinkConfig { Href = "https://github.com", Target = "_blank", Rel = "noopener noreferrer" } },
                                new ComponentNode { Id = "social-linkedin", Type = "social-link", Order = 2, Visible = true, Label = "LinkedIn", Path = "footer.socials[2]", Editable = true, Value = "LinkedIn", Link = new LinkConfig { Href = "https://linkedin.com", Target = "_blank", Rel = "noopener noreferrer" } }
                            }
                        }
                    }
                }
            }
        };
    }

    public string PageKey(string tenantId, string slug) => $"{tenantId}:{slug}";

    private static PageSchema BuildBellaItaliaPage()
    {
        return new PageSchema
        {
            Id = "bella-italia-index",
            Slug = "index",
            TemplateId = "restaurante-uno",
            TenantId = "bella-italia",
            Seo = new SeoMeta
            {
                Title = "Bella Italia – Ristorante Autentico",
                Description = "Auténtica cocina italiana en el corazón de la ciudad. Pasta fresca, pizzas artesanales y los mejores vinos italianos.",
                OgImage = "https://placehold.co/1200x630?text=Bella+Italia"
            },
            Theme = new ThemeConfig
            {
                PrimaryColor = "#c0392b",
                SecondaryColor = "#e74c3c",
                AccentColor = "#f39c12",
                BackgroundColor = "#fffdf8",
                TextColor = "#2c1810",
                PrimaryFont = "Playfair Display",
                SecondaryFont = "Lato"
            },
            Sections = new List<Section>
            {
                // ─── HEADER ───────────────────────────────────────────
                new Section
                {
                    Id = "bi-header",
                    Type = "header",
                    Order = 0,
                    Visible = true,
                    Label = "Header",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "bi-logo", Type = "image", Order = 0, Visible = true, Label = "Logo", Path = "header.logo", Editable = true, Value = new ImageValue { Src = "https://placehold.co/140x45?text=Bella+Italia", Alt = "Bella Italia Logo", Width = 140, Height = 45 } },
                        new ComponentNode
                        {
                            Id = "bi-nav",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Navegación",
                            Path = "header.nav",
                            Editable = false,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "bi-nav-1", Type = "nav-item", Order = 0, Visible = true, Label = "Inicio", Path = "header.nav[0]", Editable = true, Value = "Inicio", Link = new LinkConfig { Href = "#hero", Target = "_self" } },
                                new ComponentNode { Id = "bi-nav-2", Type = "nav-item", Order = 1, Visible = true, Label = "Menú", Path = "header.nav[1]", Editable = true, Value = "Menú", Link = new LinkConfig { Href = "#menu", Target = "_self" } },
                                new ComponentNode { Id = "bi-nav-3", Type = "nav-item", Order = 2, Visible = true, Label = "Nosotros", Path = "header.nav[2]", Editable = true, Value = "Nosotros", Link = new LinkConfig { Href = "#about", Target = "_self" } },
                                new ComponentNode { Id = "bi-nav-4", Type = "nav-item", Order = 3, Visible = true, Label = "Contacto", Path = "header.nav[3]", Editable = true, Value = "Contacto", Link = new LinkConfig { Href = "#contact", Target = "_self" } },
                                new ComponentNode { Id = "bi-nav-cta", Type = "button", Order = 4, Visible = true, Label = "Reservar", Path = "header.navCta", Editable = true, Value = "Reservar Mesa", Link = new LinkConfig { Href = "#contact", Target = "_self" } }
                            }
                        }
                    }
                },

                // ─── HERO ─────────────────────────────────────────────
                new Section
                {
                    Id = "bi-hero",
                    Type = "hero",
                    Order = 1,
                    Visible = true,
                    Label = "Hero",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "bi-hero-badge", Type = "badge", Order = 0, Visible = true, Label = "Badge", Path = "hero.badge", Editable = true, Value = "🍝 Auténtica Cocina Italiana" },
                        new ComponentNode { Id = "bi-hero-title", Type = "heading", Order = 1, Visible = true, Label = "Título", Path = "hero.title", Editable = true, Value = "Una experiencia gastronómica inigualable", Typography = new TypographyConfig { FontSize = "5xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "bi-hero-subtitle", Type = "paragraph", Order = 2, Visible = true, Label = "Subtítulo", Path = "hero.subtitle", Editable = true, Value = "Pasta fresca elaborada a diario, pizzas artesanales al horno de leña y los mejores vinos italianos. Ven y descubre el sabor de Italia.", Typography = new TypographyConfig { FontSize = "xl", TextAlign = "center" } },
                        new ComponentNode { Id = "bi-hero-cta", Type = "button", Order = 3, Visible = true, Label = "Reservar", Path = "hero.cta", Editable = true, Value = "Reservar una mesa", Link = new LinkConfig { Href = "#contact", Target = "_self" } },
                        new ComponentNode { Id = "bi-hero-image", Type = "image", Order = 4, Visible = true, Label = "Imagen Principal", Path = "hero.image", Editable = true, Value = new ImageValue { Src = "https://placehold.co/1200x500?text=Bella+Italia+Restaurante", Alt = "Interior del restaurante Bella Italia", Width = 1200, Height = 500 } }
                    }
                },

                // ─── MENU ─────────────────────────────────────────────
                new Section
                {
                    Id = "bi-menu",
                    Type = "menu",
                    Order = 2,
                    Visible = true,
                    Label = "Menú",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "bi-menu-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "menu.title", Editable = true, Value = "Nuestro Menú", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "bi-menu-subtitle", Type = "paragraph", Order = 1, Visible = true, Label = "Subtítulo", Path = "menu.subtitle", Editable = true, Value = "Ingredientes frescos importados directamente de Italia", Typography = new TypographyConfig { TextAlign = "center" } },
                        new ComponentNode
                        {
                            Id = "bi-menu-items",
                            Type = "list",
                            Order = 2,
                            Visible = true,
                            Label = "Platos",
                            Path = "menu.items",
                            Editable = false,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "bi-dish-1", Type = "card", Order = 0, Visible = true, Label = "Tagliatelle al Ragù", Path = "menu.items[0]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "bi-d1-img", Type = "image", Order = 0, Visible = true, Label = "Imagen", Path = "menu.items[0].image", Editable = true, Value = new ImageValue { Src = "https://placehold.co/400x250?text=Tagliatelle", Alt = "Tagliatelle al Ragù", Width = 400, Height = 250 } },
                                    new ComponentNode { Id = "bi-d1-name", Type = "heading", Order = 1, Visible = true, Label = "Nombre", Path = "menu.items[0].name", Editable = true, Value = "Tagliatelle al Ragù" },
                                    new ComponentNode { Id = "bi-d1-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Descripción", Path = "menu.items[0].desc", Editable = true, Value = "Pasta fresca con ragù de res y cerdo, cocido lentamente durante 6 horas." },
                                    new ComponentNode { Id = "bi-d1-price", Type = "text", Order = 3, Visible = true, Label = "Precio", Path = "menu.items[0].price", Editable = true, Value = "$195 MXN" }
                                }},
                                new ComponentNode { Id = "bi-dish-2", Type = "card", Order = 1, Visible = true, Label = "Pizza Margherita", Path = "menu.items[1]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "bi-d2-img", Type = "image", Order = 0, Visible = true, Label = "Imagen", Path = "menu.items[1].image", Editable = true, Value = new ImageValue { Src = "https://placehold.co/400x250?text=Pizza+Margherita", Alt = "Pizza Margherita", Width = 400, Height = 250 } },
                                    new ComponentNode { Id = "bi-d2-name", Type = "heading", Order = 1, Visible = true, Label = "Nombre", Path = "menu.items[1].name", Editable = true, Value = "Pizza Margherita" },
                                    new ComponentNode { Id = "bi-d2-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Descripción", Path = "menu.items[1].desc", Editable = true, Value = "Base de tomate San Marzano, mozzarella di bufala y albahaca fresca." },
                                    new ComponentNode { Id = "bi-d2-price", Type = "text", Order = 3, Visible = true, Label = "Precio", Path = "menu.items[1].price", Editable = true, Value = "$165 MXN" }
                                }},
                                new ComponentNode { Id = "bi-dish-3", Type = "card", Order = 2, Visible = true, Label = "Tiramisù", Path = "menu.items[2]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "bi-d3-img", Type = "image", Order = 0, Visible = true, Label = "Imagen", Path = "menu.items[2].image", Editable = true, Value = new ImageValue { Src = "https://placehold.co/400x250?text=Tiramisu", Alt = "Tiramisù", Width = 400, Height = 250 } },
                                    new ComponentNode { Id = "bi-d3-name", Type = "heading", Order = 1, Visible = true, Label = "Nombre", Path = "menu.items[2].name", Editable = true, Value = "Tiramisù della Nonna" },
                                    new ComponentNode { Id = "bi-d3-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Descripción", Path = "menu.items[2].desc", Editable = true, Value = "Receta tradicional con mascarpone, espresso y Savoiardi artesanales." },
                                    new ComponentNode { Id = "bi-d3-price", Type = "text", Order = 3, Visible = true, Label = "Precio", Path = "menu.items[2].price", Editable = true, Value = "$95 MXN" }
                                }}
                            }
                        }
                    }
                },

                // ─── ABOUT ────────────────────────────────────────────
                new Section
                {
                    Id = "bi-about",
                    Type = "about",
                    Order = 3,
                    Visible = true,
                    Label = "Nosotros",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "bi-about-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "about.title", Editable = true, Value = "Nuestra Historia" },
                        new ComponentNode { Id = "bi-about-text", Type = "paragraph", Order = 1, Visible = true, Label = "Descripción", Path = "about.text", Editable = true, Value = "Fundado en 1998 por la familia Rossi, Bella Italia nació del sueño de traer la auténtica gastronomía italiana a México. Durante más de 25 años hemos servido recetas familiares transmitidas por generaciones, usando ingredientes frescos y técnicas tradicionales." },
                        new ComponentNode { Id = "bi-about-image", Type = "image", Order = 2, Visible = true, Label = "Imagen", Path = "about.image", Editable = true, Value = new ImageValue { Src = "https://placehold.co/600x400?text=Familia+Rossi", Alt = "La familia fundadora", Width = 600, Height = 400 } }
                    }
                },

                // ─── CONTACT ──────────────────────────────────────────
                new Section
                {
                    Id = "bi-contact",
                    Type = "contact",
                    Order = 4,
                    Visible = true,
                    Label = "Contacto",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "bi-contact-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "contact.title", Editable = true, Value = "Visítanos" },
                        new ComponentNode { Id = "bi-contact-address", Type = "text", Order = 1, Visible = true, Label = "Dirección", Path = "contact.address", Editable = true, Value = "Av. Presidente Masaryk 123, Polanco, CDMX" },
                        new ComponentNode { Id = "bi-contact-phone", Type = "text", Order = 2, Visible = true, Label = "Teléfono", Path = "contact.phone", Editable = true, Value = "+52 55 1234 5678" },
                        new ComponentNode { Id = "bi-contact-hours", Type = "text", Order = 3, Visible = true, Label = "Horario", Path = "contact.hours", Editable = true, Value = "Lunes a Domingo: 1:00 PM – 11:00 PM" },
                        new ComponentNode { Id = "bi-contact-cta", Type = "button", Order = 4, Visible = true, Label = "Reservar", Path = "contact.cta", Editable = true, Value = "Hacer una Reservación", Link = new LinkConfig { Href = "tel:+525512345678", Target = "_self" } }
                    }
                },

                // ─── FOOTER ───────────────────────────────────────────
                new Section
                {
                    Id = "bi-footer",
                    Type = "footer",
                    Order = 5,
                    Visible = true,
                    Label = "Footer",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "bi-footer-copy", Type = "text", Order = 0, Visible = true, Label = "Copyright", Path = "footer.copyright", Editable = true, Value = "© 2024 Bella Italia Ristorante. Todos los derechos reservados." },
                        new ComponentNode
                        {
                            Id = "bi-footer-socials",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Redes Sociales",
                            Path = "footer.socials",
                            Editable = false,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "bi-social-ig", Type = "social-link", Order = 0, Visible = true, Label = "Instagram", Path = "footer.socials[0]", Editable = true, Value = "Instagram", Link = new LinkConfig { Href = "https://instagram.com", Target = "_blank", Rel = "noopener noreferrer" } },
                                new ComponentNode { Id = "bi-social-fb", Type = "social-link", Order = 1, Visible = true, Label = "Facebook", Path = "footer.socials[1]", Editable = true, Value = "Facebook", Link = new LinkConfig { Href = "https://facebook.com", Target = "_blank", Rel = "noopener noreferrer" } }
                            }
                        }
                    }
                }
            }
        };
    }

    private static PageSchema BuildEventosMxPage()
    {
        return new PageSchema
        {
            Id = "eventos-mx-index",
            Slug = "index",
            TemplateId = "organizador-eventos",
            TenantId = "eventos-mx",
            Seo = new SeoMeta
            {
                Title = "Eventos MX – Organizamos tus momentos más especiales",
                Description = "Bodas, quinceañeras, eventos corporativos y más. Creamos experiencias únicas e inolvidables para cada ocasión.",
                OgImage = "https://placehold.co/1200x630?text=Eventos+MX"
            },
            Theme = new ThemeConfig
            {
                PrimaryColor = "#7c3aed",
                SecondaryColor = "#a78bfa",
                AccentColor = "#fbbf24",
                BackgroundColor = "#faf5ff",
                TextColor = "#1f1035",
                PrimaryFont = "Cormorant Garamond",
                SecondaryFont = "Inter"
            },
            Sections = new List<Section>
            {
                // ─── HEADER ───────────────────────────────────────────
                new Section
                {
                    Id = "em-header",
                    Type = "header",
                    Order = 0,
                    Visible = true,
                    Label = "Header",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-logo", Type = "image", Order = 0, Visible = true, Label = "Logo", Path = "header.logo", Editable = true, Value = new ImageValue { Src = "https://placehold.co/130x42?text=Eventos+MX", Alt = "Eventos MX Logo", Width = 130, Height = 42 } },
                        new ComponentNode
                        {
                            Id = "em-nav",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Navegación",
                            Path = "header.nav",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 8,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "em-nav-item-tpl",
                                Type = "nav-item",
                                Order = 0,
                                Visible = true,
                                Label = "Nav Item",
                                Path = "header.nav[*]",
                                Editable = true,
                                Value = "Link",
                                Link = new LinkConfig { Href = "#", Target = "_self" }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "em-nav-1", Type = "nav-item", Order = 0, Visible = true, Label = "Inicio", Path = "header.nav[0]", Editable = true, Value = "Inicio", Link = new LinkConfig { Href = "#hero", Target = "_self" } },
                                new ComponentNode { Id = "em-nav-2", Type = "nav-item", Order = 1, Visible = true, Label = "Servicios", Path = "header.nav[1]", Editable = true, Value = "Servicios", Link = new LinkConfig { Href = "#services", Target = "_self" } },
                                new ComponentNode { Id = "em-nav-3", Type = "nav-item", Order = 2, Visible = true, Label = "Galería", Path = "header.nav[2]", Editable = true, Value = "Galería", Link = new LinkConfig { Href = "#gallery", Target = "_self" } },
                                new ComponentNode { Id = "em-nav-4", Type = "nav-item", Order = 3, Visible = true, Label = "Testimonios", Path = "header.nav[3]", Editable = true, Value = "Testimonios", Link = new LinkConfig { Href = "#testimonials", Target = "_self" } },
                                new ComponentNode { Id = "em-nav-5", Type = "nav-item", Order = 4, Visible = true, Label = "Contacto", Path = "header.nav[4]", Editable = true, Value = "Contacto", Link = new LinkConfig { Href = "#contact", Target = "_self" } },
                                new ComponentNode { Id = "em-nav-cta", Type = "button", Order = 5, Visible = true, Label = "Cotizar", Path = "header.navCta", Editable = true, Value = "Solicitar Cotización", Link = new LinkConfig { Href = "#contact", Target = "_self" } }
                            }
                        }
                    }
                },

                // ─── HERO ─────────────────────────────────────────────
                new Section
                {
                    Id = "em-hero",
                    Type = "hero",
                    Order = 1,
                    Visible = true,
                    Label = "Hero",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-hero-badge", Type = "badge", Order = 0, Visible = true, Label = "Badge", Path = "hero.badge", Editable = true, Value = "✨ Más de 500 eventos realizados" },
                        new ComponentNode { Id = "em-hero-title", Type = "heading", Order = 1, Visible = true, Label = "Título", Path = "hero.title", Editable = true, Value = "Organizamos tus momentos más especiales", Typography = new TypographyConfig { FontSize = "5xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "em-hero-subtitle", Type = "paragraph", Order = 2, Visible = true, Label = "Subtítulo", Path = "hero.subtitle", Editable = true, Value = "Bodas, quinceañeras, eventos corporativos y celebraciones de vida. Convertimos tus sueños en experiencias únicas e inolvidables.", Typography = new TypographyConfig { FontSize = "xl", TextAlign = "center" } },
                        new ComponentNode { Id = "em-hero-cta", Type = "button", Order = 3, Visible = true, Label = "CTA Principal", Path = "hero.cta", Editable = true, Value = "Solicitar cotización", Link = new LinkConfig { Href = "#contact", Target = "_self" } },
                        new ComponentNode
                        {
                            Id = "em-hero-carousel",
                            Type = "list",
                            Order = 4,
                            Visible = true,
                            Label = "Imágenes del Hero",
                            Path = "hero.images",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 6,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "em-hero-img-tpl",
                                Type = "image",
                                Order = 0,
                                Visible = true,
                                Label = "Imagen",
                                Path = "hero.images[*]",
                                Editable = true,
                                Value = new ImageValue { Src = "https://placehold.co/1200x500?text=Nuevo+Evento", Alt = "Evento", Width = 1200, Height = 500 }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "em-hero-img-1", Type = "image", Order = 0, Visible = true, Label = "Boda", Path = "hero.images[0]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/1200x500?text=Bodas+Eventos+MX", Alt = "Boda elegante Eventos MX", Width = 1200, Height = 500 } },
                                new ComponentNode { Id = "em-hero-img-2", Type = "image", Order = 1, Visible = true, Label = "XV Años", Path = "hero.images[1]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/1200x500?text=XV+Anos+Eventos+MX", Alt = "Quinceañera Eventos MX", Width = 1200, Height = 500 } },
                                new ComponentNode { Id = "em-hero-img-3", Type = "image", Order = 2, Visible = true, Label = "Corporativo", Path = "hero.images[2]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/1200x500?text=Eventos+Corporativos", Alt = "Evento corporativo Eventos MX", Width = 1200, Height = 500 } }
                            }
                        }
                    }
                },

                // ─── SERVICES ─────────────────────────────────────────
                new Section
                {
                    Id = "em-services",
                    Type = "services",
                    Order = 2,
                    Visible = true,
                    Label = "Servicios",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-svc-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "services.title", Editable = true, Value = "Nuestros Servicios", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode { Id = "em-svc-subtitle", Type = "paragraph", Order = 1, Visible = true, Label = "Subtítulo", Path = "services.subtitle", Editable = true, Value = "Todo lo que necesitas para un evento perfecto, en un solo lugar." },
                        new ComponentNode
                        {
                            Id = "em-svc-list",
                            Type = "list",
                            Order = 2,
                            Visible = true,
                            Label = "Lista de Servicios",
                            Path = "services.items",
                            Editable = false,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "em-svc-1", Type = "card", Order = 0, Visible = true, Label = "Bodas", Path = "services.items[0]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "em-s1-icon", Type = "icon", Order = 0, Visible = true, Label = "Ícono", Path = "services.items[0].icon", Editable = true, Value = "💍" },
                                    new ComponentNode { Id = "em-s1-title", Type = "heading", Order = 1, Visible = true, Label = "Título", Path = "services.items[0].title", Editable = true, Value = "Bodas" },
                                    new ComponentNode { Id = "em-s1-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Descripción", Path = "services.items[0].desc", Editable = true, Value = "Organizamos tu boda soñada de principio a fin: desde la decoración hasta el banquete y la música." }
                                }},
                                new ComponentNode { Id = "em-svc-2", Type = "card", Order = 1, Visible = true, Label = "XV Años", Path = "services.items[1]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "em-s2-icon", Type = "icon", Order = 0, Visible = true, Label = "Ícono", Path = "services.items[1].icon", Editable = true, Value = "👑" },
                                    new ComponentNode { Id = "em-s2-title", Type = "heading", Order = 1, Visible = true, Label = "Título", Path = "services.items[1].title", Editable = true, Value = "XV Años" },
                                    new ComponentNode { Id = "em-s2-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Descripción", Path = "services.items[1].desc", Editable = true, Value = "Celebraciones de quinceañera únicas con decoración temática, fotografía y coordinación completa." }
                                }},
                                new ComponentNode { Id = "em-svc-3", Type = "card", Order = 2, Visible = true, Label = "Eventos Corporativos", Path = "services.items[2]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "em-s3-icon", Type = "icon", Order = 0, Visible = true, Label = "Ícono", Path = "services.items[2].icon", Editable = true, Value = "🏢" },
                                    new ComponentNode { Id = "em-s3-title", Type = "heading", Order = 1, Visible = true, Label = "Título", Path = "services.items[2].title", Editable = true, Value = "Eventos Corporativos" },
                                    new ComponentNode { Id = "em-s3-desc", Type = "paragraph", Order = 2, Visible = true, Label = "Descripción", Path = "services.items[2].desc", Editable = true, Value = "Conferencias, lanzamientos de productos, team buildings y convenciones con logística impecable." }
                                }}
                            }
                        }
                    }
                },

                // ─── GALLERY ──────────────────────────────────────────
                new Section
                {
                    Id = "em-gallery",
                    Type = "gallery",
                    Order = 3,
                    Visible = true,
                    Label = "Galería",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-gal-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "gallery.title", Editable = true, Value = "Nuestros Eventos", Typography = new TypographyConfig { FontSize = "3xl", FontWeight = "bold", TextAlign = "center" } },
                        new ComponentNode
                        {
                            Id = "em-gal-items",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Fotografías",
                            Path = "gallery.items",
                            Editable = false,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "em-gal-1", Type = "image", Order = 0, Visible = true, Label = "Foto 1", Path = "gallery.items[0]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/600x400?text=Boda+Elegante", Alt = "Boda elegante en jardín", Width = 600, Height = 400 } },
                                new ComponentNode { Id = "em-gal-2", Type = "image", Order = 1, Visible = true, Label = "Foto 2", Path = "gallery.items[1]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/600x400?text=XV+Anos", Alt = "Celebración de XV años", Width = 600, Height = 400 } },
                                new ComponentNode { Id = "em-gal-3", Type = "image", Order = 2, Visible = true, Label = "Foto 3", Path = "gallery.items[2]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/600x400?text=Evento+Corporativo", Alt = "Evento corporativo", Width = 600, Height = 400 } },
                                new ComponentNode { Id = "em-gal-4", Type = "image", Order = 3, Visible = true, Label = "Foto 4", Path = "gallery.items[3]", Editable = true, Value = new ImageValue { Src = "https://placehold.co/600x400?text=Cumpleanos", Alt = "Celebración de cumpleaños", Width = 600, Height = 400 } }
                            }
                        }
                    }
                },

                // ─── TESTIMONIALS ─────────────────────────────────────
                new Section
                {
                    Id = "em-testimonials",
                    Type = "testimonials",
                    Order = 4,
                    Visible = true,
                    Label = "Testimonios",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-test-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "testimonials.title", Editable = true, Value = "Lo que dicen nuestros clientes" },
                        new ComponentNode
                        {
                            Id = "em-test-list",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Testimonios",
                            Path = "testimonials.items",
                            Editable = false,
                            Repeatable = true,
                            MinItems = 1,
                            MaxItems = 12,
                            RepeatableTemplate = new ComponentNode
                            {
                                Id = "em-test-card-tpl",
                                Type = "testimonial-card",
                                Order = 0,
                                Visible = true,
                                Label = "Testimonio",
                                Path = "testimonials.items[*]",
                                Editable = true,
                                Children = new List<ComponentNode>
                                {
                                    new ComponentNode { Id = "em-tc-quote-tpl", Type = "paragraph", Order = 0, Visible = true, Label = "Cita", Path = "testimonials.items[*].quote", Editable = true, Value = "Un evento perfecto gracias a Eventos MX." },
                                    new ComponentNode { Id = "em-tc-avatar-tpl", Type = "image", Order = 1, Visible = true, Label = "Foto", Path = "testimonials.items[*].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/80x80?text=AV", Alt = "Avatar", Width = 80, Height = 80 } },
                                    new ComponentNode { Id = "em-tc-name-tpl", Type = "text", Order = 2, Visible = true, Label = "Nombre", Path = "testimonials.items[*].name", Editable = true, Value = "Nombre Cliente" },
                                    new ComponentNode { Id = "em-tc-role-tpl", Type = "text", Order = 3, Visible = true, Label = "Rol", Path = "testimonials.items[*].role", Editable = true, Value = "Tipo de Evento" }
                                }
                            },
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "em-t1", Type = "testimonial-card", Order = 0, Visible = true, Label = "Testimonio 1", Path = "testimonials.items[0]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "em-t1-quote", Type = "paragraph", Order = 0, Visible = true, Label = "Cita", Path = "testimonials.items[0].quote", Editable = true, Value = "Eventos MX hizo de nuestra boda un sueño hecho realidad. Cada detalle fue perfecto y superaron todas nuestras expectativas." },
                                    new ComponentNode { Id = "em-t1-avatar", Type = "image", Order = 1, Visible = true, Label = "Foto", Path = "testimonials.items[0].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/80x80?text=MC", Alt = "María y Carlos", Width = 80, Height = 80 } },
                                    new ComponentNode { Id = "em-t1-name", Type = "text", Order = 2, Visible = true, Label = "Nombre", Path = "testimonials.items[0].name", Editable = true, Value = "María y Carlos" },
                                    new ComponentNode { Id = "em-t1-role", Type = "text", Order = 3, Visible = true, Label = "Rol", Path = "testimonials.items[0].role", Editable = true, Value = "Boda 2023" }
                                }},
                                new ComponentNode { Id = "em-t2", Type = "testimonial-card", Order = 1, Visible = true, Label = "Testimonio 2", Path = "testimonials.items[1]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "em-t2-quote", Type = "paragraph", Order = 0, Visible = true, Label = "Cita", Path = "testimonials.items[1].quote", Editable = true, Value = "El XV años de mi hija fue simplemente mágico. La coordinación fue impecable y todos los invitados quedaron encantados." },
                                    new ComponentNode { Id = "em-t2-avatar", Type = "image", Order = 1, Visible = true, Label = "Foto", Path = "testimonials.items[1].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/80x80?text=LM", Alt = "Laura Martínez", Width = 80, Height = 80 } },
                                    new ComponentNode { Id = "em-t2-name", Type = "text", Order = 2, Visible = true, Label = "Nombre", Path = "testimonials.items[1].name", Editable = true, Value = "Laura Martínez" },
                                    new ComponentNode { Id = "em-t2-role", Type = "text", Order = 3, Visible = true, Label = "Rol", Path = "testimonials.items[1].role", Editable = true, Value = "XV Años 2023" }
                                }},
                                new ComponentNode { Id = "em-t3", Type = "testimonial-card", Order = 2, Visible = true, Label = "Testimonio 3", Path = "testimonials.items[2]", Editable = true, Children = new List<ComponentNode> {
                                    new ComponentNode { Id = "em-t3-quote", Type = "paragraph", Order = 0, Visible = true, Label = "Cita", Path = "testimonials.items[2].quote", Editable = true, Value = "Organizaron nuestra convención anual para 300 personas con profesionalismo y creatividad excepcionales." },
                                    new ComponentNode { Id = "em-t3-avatar", Type = "image", Order = 1, Visible = true, Label = "Foto", Path = "testimonials.items[2].avatar", Editable = true, Value = new ImageValue { Src = "https://placehold.co/80x80?text=JR", Alt = "Jorge Ramírez", Width = 80, Height = 80 } },
                                    new ComponentNode { Id = "em-t3-name", Type = "text", Order = 2, Visible = true, Label = "Nombre", Path = "testimonials.items[2].name", Editable = true, Value = "Jorge Ramírez" },
                                    new ComponentNode { Id = "em-t3-role", Type = "text", Order = 3, Visible = true, Label = "Rol", Path = "testimonials.items[2].role", Editable = true, Value = "Director, Empresa TechCorp" }
                                }}
                            }
                        }
                    }
                },

                // ─── CONTACT ──────────────────────────────────────────
                new Section
                {
                    Id = "em-contact",
                    Type = "contact",
                    Order = 5,
                    Visible = true,
                    Label = "Contacto",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-contact-title", Type = "heading", Order = 0, Visible = true, Label = "Título", Path = "contact.title", Editable = true, Value = "Contáctanos" },
                        new ComponentNode { Id = "em-contact-subtitle", Type = "paragraph", Order = 1, Visible = true, Label = "Subtítulo", Path = "contact.subtitle", Editable = true, Value = "Cuéntanos sobre tu evento y te enviaremos una cotización personalizada sin costo." },
                        new ComponentNode { Id = "em-contact-phone", Type = "text", Order = 2, Visible = true, Label = "Teléfono", Path = "contact.phone", Editable = true, Value = "+52 55 9876 5432" },
                        new ComponentNode { Id = "em-contact-email", Type = "text", Order = 3, Visible = true, Label = "Email", Path = "contact.email", Editable = true, Value = "hola@eventosmx.com" },
                        new ComponentNode { Id = "em-contact-cta", Type = "button", Order = 4, Visible = true, Label = "CTA Cotización", Path = "contact.cta", Editable = true, Value = "Solicitar Cotización", Link = new LinkConfig { Href = "mailto:hola@eventosmx.com", Target = "_self" } }
                    }
                },

                // ─── FOOTER ───────────────────────────────────────────
                new Section
                {
                    Id = "em-footer",
                    Type = "footer",
                    Order = 6,
                    Visible = true,
                    Label = "Footer",
                    Components = new List<ComponentNode>
                    {
                        new ComponentNode { Id = "em-footer-copy", Type = "text", Order = 0, Visible = true, Label = "Copyright", Path = "footer.copyright", Editable = true, Value = "© 2024 Eventos MX. Todos los derechos reservados." },
                        new ComponentNode
                        {
                            Id = "em-footer-socials",
                            Type = "list",
                            Order = 1,
                            Visible = true,
                            Label = "Redes Sociales",
                            Path = "footer.socials",
                            Editable = false,
                            Children = new List<ComponentNode>
                            {
                                new ComponentNode { Id = "em-social-ig", Type = "social-link", Order = 0, Visible = true, Label = "Instagram", Path = "footer.socials[0]", Editable = true, Value = "Instagram", Link = new LinkConfig { Href = "https://instagram.com", Target = "_blank", Rel = "noopener noreferrer" } },
                                new ComponentNode { Id = "em-social-fb", Type = "social-link", Order = 1, Visible = true, Label = "Facebook", Path = "footer.socials[1]", Editable = true, Value = "Facebook", Link = new LinkConfig { Href = "https://facebook.com", Target = "_blank", Rel = "noopener noreferrer" } }
                            }
                        }
                    }
                }
            }
        };
    }
}
