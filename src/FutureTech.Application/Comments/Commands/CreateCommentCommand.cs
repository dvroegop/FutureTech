using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using FutureTech.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Comments.Commands;

public record CreateCommentCommand : IRequest<CommentDto>
{
    public int TicketId { get; init; }
    public required string Content { get; init; }
    public string? Author { get; init; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly IApplicationDbContext _context;

    public CreateCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == request.TicketId, cancellationToken);

        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket with ID {request.TicketId} not found.");
        }

        var comment = new Comment
        {
            Content = request.Content,
            Author = request.Author,
            TicketId = request.TicketId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            Author = comment.Author,
            TicketId = comment.TicketId,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt
        };
    }
}
