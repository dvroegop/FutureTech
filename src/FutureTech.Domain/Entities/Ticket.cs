using FutureTech.Domain.Common;

namespace FutureTech.Domain.Entities;

public class Ticket : BaseEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

public enum TicketStatus
{
    Open,
    InProgress,
    Resolved,
    Closed
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Critical
}
