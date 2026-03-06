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

        // Tenants
        Tenants["demo"] = new Tenant
        {
            Id = "demo",
            Name = "Demo Company",
            Subdomain = "demo",
            Plan = "pro",
            CreatedAt = DateTime.UtcNow
        };

        // Pages – keyed by tenantId (one page per tenant)
        Pages["demo"] = BuildDemoPage();
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
}
