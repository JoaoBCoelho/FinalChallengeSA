using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.GetAllCustomers
{
    public sealed class GetAllCustomersQueryHandler
    : IRequestHandler<GetAllCustomersQuery, IReadOnlyCollection<CustomerResponse>>
    {
        private readonly IGenericRepository<Customer> _repository;

        public GetAllCustomersQueryHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<CustomerResponse>> Handle(
            GetAllCustomersQuery query,
            CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllAsync(cancellationToken);

            return customers
                .Select(c => new CustomerResponse(c.Id, c.Name, c.Email))
                .ToArray();
        }
    }
}
