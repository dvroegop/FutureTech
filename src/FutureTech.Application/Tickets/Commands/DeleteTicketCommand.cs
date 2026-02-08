using FutureTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FutureTech.Application.Tickets.Commands;

public record DeleteTicketCommand : IRequest<bool>
{
    public int Id { get; init; }
}

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteTicketCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (ticket == null)
        {
            return false;
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
