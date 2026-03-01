using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Application.Models;
using SupportHub.Application.Enums;
using SupportHub.Application.Features.Tickets.Commands.AssignTicket;
using SupportHub.Application.Features.Tickets.Commands.CreateTicket;
using SupportHub.Application.Features.Tickets.Commands.UpdateTicketStatus;
using SupportHub.Application.Features.Tickets.Queries.GetTicketById;
using SupportHub.Application.Features.Tickets.Commands.UpdateTicketCategory;
using SupportHub.Application.Features.Tickets.Commands.UpdateTicketPriority;
using SupportHub.Application.Features.Tickets.Commands.DeleteTicket;
using SupportHub.Application.Features.Tickets.Commands.UpdateTicket;

namespace SupportHub.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketsController : BaseApiController
{
    
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateTicketCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var result = await Mediator.Send(new GetTicketByIdQuery(id));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTicketCommand(id));
        return NoContent();
    }

    [HttpPost("{ticketId}/assign/{agentId}")]
    public async Task<IActionResult> Assign(int ticketId, int agentId)
    {
        await Mediator.Send(new AssignTicketCommand(ticketId, agentId));
        return Ok();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] TicketStatusDto newStatus)
    {
        await Mediator.Send(new UpdateTicketStatusCommand(id, newStatus));
        return NoContent();
    }

    [HttpPatch("{id}/category")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] TicketCategoryDto newCategory)
    {
        await Mediator.Send(new UpdateTicketCategoryCommand(id, newCategory));
        return NoContent();
    }

    [HttpPatch("{id}/priority")]
    public async Task<IActionResult> UpdatePriority(int id, [FromBody] TicketPriorityDto newPriority)
    {
        await Mediator.Send(new UpdateTicketPriorityCommand(id, newPriority));
        return NoContent();
    }
}