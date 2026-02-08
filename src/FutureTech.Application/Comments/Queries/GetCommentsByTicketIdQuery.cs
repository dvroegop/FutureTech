using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Comments.Queries;

public record GetCommentsByTicketIdQuery : IRequest<List<CommentDto>>
{
    public int TicketId { get; init; }
}

public class GetCommentsByTicketIdQueryHandler : IRequestHandler<GetCommentsByTicketIdQuery, List<CommentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCommentsByTicketIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommentDto>> Handle(GetCommentsByTicketIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await _context.Comments
            .Where(c => c.TicketId == request.TicketId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            Content = c.Content,
            Author = c.Author,
            TicketId = c.TicketId,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();
    }
}
