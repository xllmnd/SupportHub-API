using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.AssignTicket
{
    public record AssignTicketCommand(int TicketId, int AgentId) : IRequest<Unit>;

    public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand, Unit>
    {
        private readonly IRepository<Ticket> _ticketRepo;
        private readonly IRepository<Agent> _agentRepo;

        public AssignTicketCommandHandler(IRepository<Ticket> ticketRepo, IRepository<Agent> agentRepo)
        {
            _ticketRepo = ticketRepo;
            _agentRepo = agentRepo;
        }

        public async Task<Unit> Handle(AssignTicketCommand request, CancellationToken ct)
        {
            var ticket = await _ticketRepo.GetByIdAsync(request.TicketId);
            var agent = await _agentRepo.GetByIdAsync(request.AgentId);

            if (ticket == null || agent == null) throw new Exception("The ticket or representative could not be found.");

            ticket.AssignedAgentId = request.AgentId;
            ticket.Status = TicketStatus.InProgress; 
            ticket.UpdatedAt = DateTime.UtcNow;

            await _ticketRepo.UpdateAsync(ticket);
            return Unit.Value;
        }
    }
}
