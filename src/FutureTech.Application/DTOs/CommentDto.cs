namespace FutureTech.Application.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public string? Author { get; set; }
    public int TicketId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
