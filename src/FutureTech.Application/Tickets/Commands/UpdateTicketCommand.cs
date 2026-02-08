using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using FutureTech.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Tickets.Commands;

public record UpdateTicketCommand : IRequest<TicketDto>
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public TicketStatus Status { get; init; }
    public TicketPriority Priority { get; init; }
}

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateTicketCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TicketDto> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.Id} not found.");
        }

        ticket.Title = request.Title;
        ticket.Description = request.Description;
        ticket.Status = request.Status;
        ticket.Priority = request.Priority;
        ticket.UpdatedAt = DateTime.UtcNow;

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
