using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Api.Controllers;
using SupportHub.Application.Features.Customers.Commands.CreateCustomer;
using SupportHub.Application.Features.Customers.Queries.GetCustomerById;
using SupportHub.Application.Features.Tickets.Queries.GetTicketsByCustomerId;
using SupportHub.Application.Models;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomersController : BaseApiController
{
    
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCustomerCommand command)
        => Ok(await Mediator.Send(command));

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
        => Ok(await Mediator.Send(new GetCustomerByIdQuery(id)));
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
    {
        if (id != command.Id) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();
    }

    [HttpGet("{id}/tickets")]
    public async Task<ActionResult<List<TicketDto>>> GetCustomerTickets(int id)
        => Ok(await Mediator.Send(new GetTicketsByCustomerIdQuery(id)));
}