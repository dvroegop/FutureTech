using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Tickets.Queries;

public record GetAllTicketsQuery : IRequest<List<TicketDto>>;

public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<TicketDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTicketsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _context.Tickets
            .Include(t => t.Comments)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status.ToString(),
            Priority = t.Priority.ToString(),
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt,
            Comments = t.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                Author = c.Author,
                TicketId = c.TicketId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList()
        }).ToList();
    }
}
