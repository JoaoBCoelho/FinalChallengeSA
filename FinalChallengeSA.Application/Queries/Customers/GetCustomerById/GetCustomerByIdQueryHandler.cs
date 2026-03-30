using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.GetCustomerById
{
    public sealed class GetCustomerByIdQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByIdQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponse> Handle(
            GetCustomerByIdQuery query,
            CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(query.Id, cancellationToken);

            return customer is null
                ? throw new NotFoundException($"Cliente com id '{query.Id}' não encontrado.")
                : new CustomerResponse(customer.Id, customer.Name, customer.Email);
        }
    }
}
