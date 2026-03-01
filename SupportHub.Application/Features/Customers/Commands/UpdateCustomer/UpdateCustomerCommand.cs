using MediatR;
using SupportHub.Application.Common.Events;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;

public record UpdateCustomerCommand(int Id, string Name, string Email) : IRequest<Unit>;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IRepository<Customer> _repo;
    private readonly IMediator _mediator;
    public UpdateCustomerCommandHandler(IRepository<Customer> repo, IMediator mediator) {
        _repo = repo; 
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken ct)
    {
        var customer = await _repo.GetByIdAsync(request.Id);
        if (customer == null) throw new KeyNotFoundException("The customer cannot be found.");

        customer.Name = request.Name;
        customer.Email = request.Email;

        await _repo.UpdateAsync(customer);

        await _mediator.Publish(new CacheInvalidationEvent($"Customer_{customer.Id}"), ct);


        return Unit.Value;
    }
}