using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.UpdateTicket
{
    public record UpdateTicketCommand(int Id, string Title, string Description) : IRequest<Unit>;

    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Unit>
    {
        private readonly IRepository<Ticket> _repo;
        public UpdateTicketCommandHandler(IRepository<Ticket> repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateTicketCommand request, CancellationToken ct)
        {
            var ticket = await _repo.GetByIdAsync(request.Id);
            if (ticket == null) throw new KeyNotFoundException("The Ticket cannot be found.");

            ticket.Title = request.Title;
            ticket.Description = request.Description;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(ticket);
            return Unit.Value;
        }
    }
}
