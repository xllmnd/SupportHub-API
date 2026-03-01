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

namespace SupportHub.Application.Features.Tickets.Queries.GetTicketsByCustomerId
{
    public record GetTicketsByCustomerIdQuery(int CustomerId) : IRequest<List<TicketDto>>;

    public class GetTicketsByCustomerIdQueryHandler : IRequestHandler<GetTicketsByCustomerIdQuery, List<TicketDto>>
    {
        private readonly IRepository<Ticket> _repo;
        private readonly IMapper _mapper;

        public GetTicketsByCustomerIdQueryHandler(IRepository<Ticket> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TicketDto>> Handle(GetTicketsByCustomerIdQuery request, CancellationToken ct)
        {           
            var tickets = await _repo.GetEntitiesWithSpec(
                t => t.CustomerId == request.CustomerId,
                t => t.Customer, t => t.AssignedAgent);

            return _mapper.Map<List<TicketDto>>(tickets);
        }
    }
}
