using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.CountCustomers
{
    public sealed class CountCustomersQueryHandler : IRequestHandler<CountCustomersQuery, int>
    {
        private readonly ICustomerRepository _repository;

        public CountCustomersQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CountCustomersQuery query,
            CancellationToken cancellationToken)
        {
            return await _repository.CountAsync(cancellationToken);
        }
    }
}
