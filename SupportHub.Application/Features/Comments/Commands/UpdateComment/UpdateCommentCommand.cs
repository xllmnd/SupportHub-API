using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;

public record UpdateCommentCommand(int Id, string Body) : IRequest<Unit>;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
{
    private readonly IRepository<Comment> _repo;

    public UpdateCommentCommandHandler(IRepository<Comment> repo) => _repo = repo;

    public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken ct)
    {
        var comment = await _repo.GetByIdAsync(request.Id);
        if (comment == null) throw new KeyNotFoundException("The comment cannot be found.");

        comment.Body = request.Body;

        await _repo.UpdateAsync(comment);
        return Unit.Value;
    }
}