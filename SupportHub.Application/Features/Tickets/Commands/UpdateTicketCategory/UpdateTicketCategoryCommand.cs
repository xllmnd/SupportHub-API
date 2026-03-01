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

namespace SupportHub.Application.Features.Tickets.Commands.UpdateTicketCategory
{
    public record UpdateTicketCategoryCommand(int Id, TicketCategoryDto NewCategory) : IRequest<Unit>;

    public class UpdateTicketCategoryCommandHandler : IRequestHandler<UpdateTicketCategoryCommand, Unit>
    {
        private readonly IRepository<Ticket> _repo;
        public UpdateTicketCategoryCommandHandler(IRepository<Ticket> repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateTicketCategoryCommand request, CancellationToken ct)
        {
            var ticket = await _repo.GetByIdAsync(request.Id);
            if (ticket == null) throw new KeyNotFoundException("The Ticket cannot be found.");

            
            if (ticket.Status == TicketStatus.Resolved || ticket.Status == TicketStatus.Closed)
                throw new InvalidOperationException("The category of resolved and closed tickets cannot be changed.");

            ticket.Category = (TicketCategory)request.NewCategory;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(ticket);
            return Unit.Value;
        }
    }
}
