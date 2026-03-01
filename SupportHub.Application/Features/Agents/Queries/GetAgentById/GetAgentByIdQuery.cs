using AutoMapper;
using MediatR;
using SupportHub.Application.Common.Interfaces;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Application.Models;
using SupportHub.Domain.Entities;

namespace SupportHub.Application.Features.Agents.Queries.GetAgentById;

public record GetAgentByIdQuery(int Id) : IRequest<AgentDto>, ICacheable
{
    public string CacheKey => $"Agent_{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
}

public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, AgentDto>
{
    private readonly IRepository<Agent> _repository;
    private readonly IMapper _mapper;

    public GetAgentByIdQueryHandler(IRepository<Agent> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AgentDto> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetByIdAsync(request.Id);

        if (agent == null)
        {
            throw new KeyNotFoundException($"ID'si {request.Id} olan temsilci bulunamadı.");
        }

        return _mapper.Map<AgentDto>(agent);
    }
}