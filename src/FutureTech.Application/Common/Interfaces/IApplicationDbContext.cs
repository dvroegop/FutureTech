using FutureTech.Domain.Entities;

namespace FutureTech.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Microsoft.EntityFrameworkCore.DbSet<Ticket> Tickets { get; }
    Microsoft.EntityFrameworkCore.DbSet<Comment> Comments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
