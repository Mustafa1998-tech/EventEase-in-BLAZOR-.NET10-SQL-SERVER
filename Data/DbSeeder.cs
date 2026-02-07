using EventEase.Models;
using EventEase.Services;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        EventDbContext context,
        IConfiguration configuration,
        UserService userService)
    {
        await context.Database.MigrateAsync();

        var existingCount = await context.Events.CountAsync();
        if (existingCount < 15)
        {
            var seedEvents = new List<Event>
            {
                new()
                {
                    Title = "Blazor Productivity Workshop",
                    Description = "Hands-on workshop covering modern Blazor patterns and tooling.",
                    Location = "Riyadh Tech Hub",
                    EventDate = DateTime.Today.AddDays(7)
                },
                new()
                {
                    Title = "EventEase Launch Meetup",
                    Description = "Product walkthrough, roadmap discussion, and Q&A.",
                    Location = "Jeddah Innovation Space",
                    EventDate = DateTime.Today.AddDays(14)
                },
                new()
                {
                    Title = "Community Networking Night",
                    Description = "Connect with organizers and share event best practices.",
                    Location = "Dammam Business Center",
                    EventDate = DateTime.Today.AddDays(21)
                },
                new()
                {
                    Title = "Startup Pitch Day",
                    Description = "Showcase your startup idea to mentors and investors.",
                    Location = "Khobar Innovation Hub",
                    EventDate = DateTime.Today.AddDays(28)
                },
                new()
                {
                    Title = "Cloud Fundamentals Bootcamp",
                    Description = "Intro to cloud architecture, security, and DevOps basics.",
                    Location = "Online",
                    EventDate = DateTime.Today.AddDays(35)
                },
                new()
                {
                    Title = "AI for Product Managers",
                    Description = "Practical AI use-cases for product strategy and delivery.",
                    Location = "Riyadh",
                    EventDate = DateTime.Today.AddDays(42)
                },
                new()
                {
                    Title = "Cybersecurity Awareness Seminar",
                    Description = "Modern threats, phishing defense, and best practices.",
                    Location = "Jeddah",
                    EventDate = DateTime.Today.AddDays(49)
                },
                new()
                {
                    Title = "UX Research Workshop",
                    Description = "Hands-on user interviews and usability testing.",
                    Location = "Dammam",
                    EventDate = DateTime.Today.AddDays(56)
                },
                new()
                {
                    Title = "Agile Delivery Deep Dive",
                    Description = "Scrum, Kanban, and delivery metrics for teams.",
                    Location = "Riyadh",
                    EventDate = DateTime.Today.AddDays(63)
                },
                new()
                {
                    Title = "Mobile Dev Meetup",
                    Description = "Latest trends in iOS/Android development.",
                    Location = "Khobar",
                    EventDate = DateTime.Today.AddDays(70)
                },
                new()
                {
                    Title = "Data Analytics 101",
                    Description = "Dashboards, KPIs, and storytelling with data.",
                    Location = "Online",
                    EventDate = DateTime.Today.AddDays(77)
                },
                new()
                {
                    Title = "Leadership for Engineers",
                    Description = "Communication, mentoring, and growth plans.",
                    Location = "Riyadh",
                    EventDate = DateTime.Today.AddDays(84)
                },
                new()
                {
                    Title = "Open Source Sprint",
                    Description = "Contribute to open source with guided maintainers.",
                    Location = "Jeddah",
                    EventDate = DateTime.Today.AddDays(91)
                },
                new()
                {
                    Title = "DevOps Tooling Expo",
                    Description = "CI/CD tools, monitoring stacks, and best practices.",
                    Location = "Online",
                    EventDate = DateTime.Today.AddDays(98)
                },
                new()
                {
                    Title = "Product Design Showcase",
                    Description = "Live demos from local product design teams.",
                    Location = "Riyadh",
                    EventDate = DateTime.Today.AddDays(105)
                }
            };

            context.Events.AddRange(seedEvents);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var name = configuration["SeedAdmin:Name"] ?? "Admin";
            var email = configuration["SeedAdmin:Email"] ?? "admin@eventease.local";
            var password = configuration["SeedAdmin:Password"] ?? "Admin@123";

            await userService.CreateAsync(name, email, password);
        }
    }
}
