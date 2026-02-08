using FutureTech.Application.Common.Interfaces;
using FutureTech.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Comments.Commands;

public record UpdateCommentCommand : IRequest<CommentDto>
{
    public int Id { get; init; }
    public required string Content { get; init; }
    public string? Author { get; init; }
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            throw new KeyNotFoundException($"Comment with ID {request.Id} not found.");
        }

        comment.Content = request.Content;
        comment.Author = request.Author;
        comment.UpdatedAt = DateTime.UtcNow;

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
