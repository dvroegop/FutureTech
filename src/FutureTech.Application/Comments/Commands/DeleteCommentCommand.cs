using FutureTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Comments.Commands;

public record DeleteCommentCommand : IRequest<bool>
{
    public int Id { get; init; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteCommentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (comment == null)
        {
            return false;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
