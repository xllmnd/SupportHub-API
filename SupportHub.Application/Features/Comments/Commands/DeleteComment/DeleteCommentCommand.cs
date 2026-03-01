using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;

namespace SupportHub.Application.Features.Comments.Commands.DeleteComment;

// 1. Komut Nesnesi (Girdi: Silinecek Yorumun ID'si)
public record DeleteCommentCommand(int Id) : IRequest<Unit>;

// 2. İşleyici (Handler)
public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
{
    private readonly IRepository<Comment> _repository;

    public DeleteCommentCommandHandler(IRepository<Comment> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(request.Id);

        if (comment == null)
        {
            throw new KeyNotFoundException($"The representative with ID {request.Id} was not found; deletion failed");
        }

        await _repository.DeleteAsync(comment);

        return Unit.Value;
    }
}