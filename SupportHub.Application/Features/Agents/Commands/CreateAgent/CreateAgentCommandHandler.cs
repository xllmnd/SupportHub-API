using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Agents.Commands.CreateAgent
{
    public record CreateAgentCommand(string DisplayName, string Email) : IRequest<int>;

    public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, int>
    {
        private readonly IRepository<Agent> _repository;

        public CreateAgentCommandHandler(IRepository<Agent> repository) => _repository = repository;

        public async Task<int> Handle(CreateAgentCommand request, CancellationToken ct)
        {
            var agent = new Agent { DisplayName = request.DisplayName, Email = request.Email };
            await _repository.AddAsync(agent);
            return agent.Id;
        }
    }
}
