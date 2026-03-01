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

namespace SupportHub.Application.Features.Customers.Queries.GetCustomerTickets
{
    public record GetCustomerTicketsQuery(int CustomerId) : IRequest<List<TicketDto>>;

    public class GetCustomerTicketsQueryHandler : IRequestHandler<GetCustomerTicketsQuery, List<TicketDto>>
    {
        private readonly IRepository<Ticket> _repository; 
        private readonly IMapper _mapper;

        public GetCustomerTicketsQueryHandler(IRepository<Ticket> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TicketDto>> Handle(GetCustomerTicketsQuery request, CancellationToken ct)
        {
            
            var tickets = await _repository.GetEntitiesWithSpec(
                t => t.CustomerId == request.CustomerId,
                t => t.Customer, t => t.AssignedAgent);

            return _mapper.Map<List<TicketDto>>(tickets);
        }
    }
}
