using AutoMapper;
using MediatR;
using SupportHub.Application.Common.Interfaces;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Application.Models;
using SupportHub.Domain.Entities;

namespace SupportHub.Application.Features.Customers.Queries.GetCustomerById;

// 1. Sorgu Nesnesi (Girdi: Müşteri ID, Çıktı: CustomerDto)
public record GetCustomerByIdQuery(int Id) : IRequest<CustomerDto>, ICacheable
{
    public string CacheKey => $"Customer_{Id}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
}

// 2. İşleyici (Handler)
public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly IRepository<Customer> _repository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IRepository<Customer> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {        
        var customer = await _repository.GetByIdAsync(request.Id);
        
        if (customer == null)
        {
            throw new KeyNotFoundException($"ID'si {request.Id} olan müşteri bulunamadı.");
        }

        return _mapper.Map<CustomerDto>(customer);
    }
}