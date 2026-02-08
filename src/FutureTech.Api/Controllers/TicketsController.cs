using FutureTech.Application.DTOs;
using FutureTech.Application.Tickets.Commands;
using FutureTech.Application.Tickets.Queries;
using FutureTech.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FutureTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all tickets
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TicketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
    {
        var tickets = await _mediator.Send(new GetAllTicketsQuery());
        return Ok(tickets);
    }

    /// <summary>
    /// Get a specific ticket by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetTicketById(int id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery { Id = id });
        
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    /// <summary>
    /// Create a new ticket
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] CreateTicketRequest request)
    {
        var command = new CreateTicketCommand
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            Priority = request.Priority
        };

        var ticket = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
    }

    /// <summary>
    /// Update an existing ticket
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
    {
        try
        {
            var command = new UpdateTicketCommand
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority
            };

            var ticket = await _mediator.Send(command);
            return Ok(ticket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete a ticket
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var result = await _mediator.Send(new DeleteTicketCommand { Id = id });
        
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public record CreateTicketRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public TicketStatus Status { get; init; } = TicketStatus.Open;
    public TicketPriority Priority { get; init; } = TicketPriority.Medium;
}

public record UpdateTicketRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public TicketStatus Status { get; init; }
    public TicketPriority Priority { get; init; }
}
