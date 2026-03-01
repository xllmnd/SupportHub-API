using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;

public record DeleteCustomerCommand(int Id) : IRequest<Unit>;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly IRepository<Customer> _repo;

    public DeleteCustomerCommandHandler(IRepository<Customer> repo) => _repo = repo;

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken ct)
    {
        var customer = await _repo.GetByIdAsync(request.Id);
        if (customer == null) throw new KeyNotFoundException("The customer cannot be found.");

        await _repo.DeleteAsync(customer);
        return Unit.Value;
    }
}