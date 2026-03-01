using SupportHub.Domain.Entities;
using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;

namespace SupportHub.Application.Features.Customers.Commands.CreateCustomer
{ 
    public record CreateCustomerCommand(string Name, string Email) : IRequest<int>;

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IRepository<Customer> _repository;

        public CreateCustomerCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            var result = await _repository.AddAsync(customer);

            return result.Id;
        }
    }
}
