using AutoMapper;
using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Application.Models;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Queries.GetTicketById
{
    public record GetTicketByIdQuery(int Id) : IRequest<TicketDto>;

    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IRepository<Ticket> _repo;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(IRepository<Ticket> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken ct)
        {
            var ticket = await _repo.GetEntityWithSpec(t => t.Id == request.Id, t => t.Customer, t => t.AssignedAgent);
            return _mapper.Map<TicketDto>(ticket);
        }
    }
}
