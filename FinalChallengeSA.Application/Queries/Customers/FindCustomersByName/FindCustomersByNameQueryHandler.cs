using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.GetCustomersByName
{
    public sealed class GetCustomersByNameQueryHandler : IRequestHandler<GetCustomersByNameQuery, IReadOnlyCollection<CustomerResponse>>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomersByNameQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<CustomerResponse>> Handle(
            GetCustomersByNameQuery query,
            CancellationToken cancellationToken)
        {
            var customers = await _repository.GetByNameAsync(query.Name, cancellationToken);

            if (customers is null)
            {
                return [];
            }
            return [.. customers.Select(customer => new CustomerResponse(customer.Id, customer.Name, customer.Email))];
        }
    }
}
