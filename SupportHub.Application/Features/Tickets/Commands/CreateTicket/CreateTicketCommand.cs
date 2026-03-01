using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Enums;

namespace SupportHub.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(
        int CustomerId,
        string Title,
        string Description,
        TicketCategory Category,
        TicketPriority Priority) : IRequest<int>;

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Customer> _customerRepository;

        public CreateTicketCommandHandler(
            IRepository<Ticket> ticketRepository,
            IRepository<Customer> customerRepository)
        {
            _ticketRepository = ticketRepository;
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            // İŞ KURALI: Müşteri gerçekten var mı?
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
            {
                // Burada özel bir Exception fırlatıyoruz. 
                // API tarafındaki Middleware bunu yakalayıp 404/400 dönecek.
                throw new Exception($"The customer with ID {request.CustomerId} cannot be found. Ticket cannot be created.");
            }

            // 3. Entity Oluşturma
            var ticket = new Ticket
            {
                CustomerId = request.CustomerId,
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Priority = request.Priority,
                Status = TicketStatus.Open, 
                CreateAt = DateTime.UtcNow
            };

            // 4. Kaydetme
            var result = await _ticketRepository.AddAsync(ticket);

            return result.Id;
        }
    }
}
