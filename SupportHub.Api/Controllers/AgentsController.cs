using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Api.Controllers;
using SupportHub.Application.Features.Agents.Commands.CreateAgent;
using SupportHub.Application.Features.Agents.Commands.DeleteAgent;
using SupportHub.Application.Features.Agents.Queries.GetAgentById;
using SupportHub.Application.Models;

[ApiController]
[Route("api/v1/[controller]")]
public class AgentsController : BaseApiController
{    

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateAgentCommand command)
        => Ok(await Mediator.Send(command));

    [HttpGet("{id}")]
    public async Task<ActionResult<AgentDto>> GetById(int id)
        => Ok(await Mediator.Send(new GetAgentByIdQuery(id)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteAgentCommand(id));
        return NoContent();
    }
}