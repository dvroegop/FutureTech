using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Tickets.Queries;

public record GetTicketByIdQuery : IRequest<TicketDto?>
{
    public int Id { get; init; }
}

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
{
    private readonly IApplicationDbContext _context;

    public GetTicketByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
        {
            return null;
        }

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status.ToString(),
            Priority = ticket.Priority.ToString(),
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            Comments = ticket.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                Author = c.Author,
                TicketId = c.TicketId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList()
        };
    }
}
