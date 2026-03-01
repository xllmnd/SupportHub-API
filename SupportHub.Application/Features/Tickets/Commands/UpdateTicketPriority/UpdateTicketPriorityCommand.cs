using MediatR;
using SupportHub.Application.Enums;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.UpdateTicketPriority
{
    public record UpdateTicketPriorityCommand(int Id, TicketPriorityDto NewPriority) : IRequest<Unit>;

    public class UpdateTicketPriorityCommandHandler : IRequestHandler<UpdateTicketPriorityCommand, Unit>
    {
        private readonly IRepository<Ticket> _repo;
        public UpdateTicketPriorityCommandHandler(IRepository<Ticket> repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateTicketPriorityCommand request, CancellationToken ct)
        {
            var ticket = await _repo.GetByIdAsync(request.Id);
            if (ticket == null) throw new KeyNotFoundException("The Ticket cannot be found.");

            
            if (ticket.Status == TicketStatus.Resolved || ticket.Status == TicketStatus.Closed)
                throw new InvalidOperationException("The priority of resolved and closed tickets cannot be changed.");

            ticket.Priority = (TicketPriority)request.NewPriority;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(ticket);
            return Unit.Value;
        }
    }
}
