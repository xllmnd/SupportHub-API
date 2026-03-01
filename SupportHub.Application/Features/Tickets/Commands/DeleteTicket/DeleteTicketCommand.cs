using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.DeleteTicket
{
    public record DeleteTicketCommand(int Id) : IRequest<Unit>;

    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Unit>
    {
        private readonly IRepository<Ticket> _repo;
        public DeleteTicketCommandHandler(IRepository<Ticket> repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken ct)
        {
            var ticket = await _repo.GetByIdAsync(request.Id);
            if (ticket == null) throw new KeyNotFoundException("The ticket cannot be found.");

            await _repo.DeleteAsync(ticket);
            return Unit.Value;
        }
    }
}
