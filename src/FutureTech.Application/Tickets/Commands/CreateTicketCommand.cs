using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using FutureTech.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Tickets.Commands;

public record CreateTicketCommand : IRequest<TicketDto>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public TicketStatus Status { get; init; }
    public TicketPriority Priority { get; init; }
}

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly IApplicationDbContext _context;

    public CreateTicketCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync(cancellationToken);

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status.ToString(),
            Priority = ticket.Priority.ToString(),
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt
        };
    }
}
