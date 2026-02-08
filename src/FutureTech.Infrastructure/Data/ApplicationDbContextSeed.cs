using FutureTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FutureTech.Infrastructure.Data;

public static class ApplicationDbContextSeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();

        // Seed data only if no tickets exist
        if (!await context.Tickets.AnyAsync())
        {
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Title = "Setup development environment",
                    Description = "Install .NET SDK, Visual Studio, and configure the project",
                    Status = TicketStatus.Closed,
                    Priority = TicketPriority.High,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Ticket
                {
                    Title = "Implement user authentication",
                    Description = "Add JWT authentication for API endpoints",
                    Status = TicketStatus.InProgress,
                    Priority = TicketPriority.Critical,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                },
                new Ticket
                {
                    Title = "Create API documentation",
                    Description = "Document all REST endpoints with Swagger",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Medium,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Ticket
                {
                    Title = "Fix bug in comment deletion",
                    Description = "Comments are not being deleted properly when tickets are removed",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.High,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new Ticket
                {
                    Title = "Add unit tests",
                    Description = "Create comprehensive unit tests for all API endpoints",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Low,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };

            context.Tickets.AddRange(tickets);
            await context.SaveChangesAsync();

            // Add comments to some tickets
            var comments = new List<Comment>
            {
                new Comment
                {
                    Content = "Environment is now fully configured and working perfectly!",
                    Author = "John Developer",
                    TicketId = tickets[0].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-9)
                },
                new Comment
                {
                    Content = "Started implementing JWT token generation",
                    Author = "Jane Smith",
                    TicketId = tickets[1].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                },
                new Comment
                {
                    Content = "Need to add refresh token functionality as well",
                    Author = "Bob Johnson",
                    TicketId = tickets[1].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Comment
                {
                    Content = "Swagger is already configured in the API project",
                    Author = "Alice Williams",
                    TicketId = tickets[2].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-4)
                }
            };

            context.Comments.AddRange(comments);
            await context.SaveChangesAsync();
        }
    }
}
