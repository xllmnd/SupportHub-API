using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupportHub.Api.Controllers;
using SupportHub.Application.Features.Comments.Commands.CreateComment;
using SupportHub.Application.Features.Comments.Commands.DeleteComment;
using SupportHub.Application.Models;

[ApiController]
[Route("api/v1/[controller]")]
public class CommentsController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCommentCommand command)
        => Ok(await Mediator.Send(command));

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetById(int id)
        => Ok(await Mediator.Send(new GetCommentByIdQuery(id)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentCommand command)
    {
        if (id != command.Id) return BadRequest();
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCommentCommand(id));
        return NoContent();
    }
}