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

namespace SupportHub.Application.Features.Tickets.Commands.UpdateTicketStatus
{
    public record UpdateTicketStatusCommand(int Id, TicketStatusDto NewStatus) : IRequest<Unit>;

    public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, Unit>
    {
        private readonly IRepository<Ticket> _repository;

        public UpdateTicketStatusCommandHandler(IRepository<Ticket> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateTicketStatusCommand request, CancellationToken ct)
        {
            var ticket = await _repository.GetByIdAsync(request.Id);
            if (ticket == null) throw new Exception("The Ticket cannot be found.");

            if (ticket.Status == TicketStatus.Resolved || ticket.Status == TicketStatus.Closed)
            {
                throw new Exception("The status of resolved and closed tickets cannot be changed.");
            }

            ticket.Status = (TicketStatus)request.NewStatus;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(ticket);
            return Unit.Value;
        }
    }
}
