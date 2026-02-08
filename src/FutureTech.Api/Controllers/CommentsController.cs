using FutureTech.Application.Comments.Commands;
using FutureTech.Application.Comments.Queries;
using FutureTech.Application.DTOs;
using FutureTech.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FutureTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all comments for a specific ticket
    /// </summary>
    [HttpGet("ticket/{ticketId}")]
    [ProducesResponseType(typeof(List<CommentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CommentDto>>> GetCommentsByTicketId(int ticketId)
    {
        var comments = await _mediator.Send(new GetCommentsByTicketIdQuery { TicketId = ticketId });
        return Ok(comments);
    }

    /// <summary>
    /// Create a new comment on a ticket
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentRequest request)
    {
        try
        {
            var command = new CreateCommentCommand
            {
                TicketId = request.TicketId,
                Content = request.Content,
                Author = request.Author
            };

            var comment = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCommentsByTicketId), new { ticketId = comment.TicketId }, comment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Update an existing comment
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> UpdateComment(int id, [FromBody] UpdateCommentRequest request)
    {
        try
        {
            var command = new UpdateCommentCommand
            {
                Id = id,
                Content = request.Content,
                Author = request.Author
            };

            var comment = await _mediator.Send(command);
            return Ok(comment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete a comment
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var result = await _mediator.Send(new DeleteCommentCommand { Id = id });
        
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public record CreateCommentRequest
{
    public int TicketId { get; init; }
    public required string Content { get; init; }
    public string? Author { get; init; }
}

public record UpdateCommentRequest
{
    public required string Content { get; init; }
    public string? Author { get; init; }
}
