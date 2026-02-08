using FutureTech.Domain.Common;

namespace FutureTech.Domain.Entities;

public class Comment : BaseEntity
{
    public required string Content { get; set; }
    public string? Author { get; set; }
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
}
