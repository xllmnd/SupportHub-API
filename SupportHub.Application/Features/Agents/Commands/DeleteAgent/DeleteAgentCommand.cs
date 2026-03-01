using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;

namespace SupportHub.Application.Features.Agents.Commands.DeleteAgent;


public record DeleteAgentCommand(int Id) : IRequest<Unit>;

public class DeleteAgentCommandHandler : IRequestHandler<DeleteAgentCommand, Unit>
{
    private readonly IRepository<Agent> _repository;

    public DeleteAgentCommandHandler(IRepository<Agent> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteAgentCommand request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetByIdAsync(request.Id);

        if (agent == null)
        {
            throw new KeyNotFoundException($"ID'si {request.Id} olan temsilci silinemedi çünkü bulunamadı.");
        }

        await _repository.DeleteAsync(agent);

        return Unit.Value;
    }
}