using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.CountOrders
{
    public sealed class CountOrdersQueryHandler : IRequestHandler<CountOrdersQuery, int>
    {
        private readonly IOrderRepository _repository;

        public CountOrdersQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CountOrdersQuery query,
            CancellationToken cancellationToken)
        {
            return await _repository.CountAsync(cancellationToken);
        }
    }
}
