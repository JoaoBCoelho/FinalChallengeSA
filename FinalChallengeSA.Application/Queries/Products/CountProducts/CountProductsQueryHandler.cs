using FinalChallengeSA.Application.Interfaces;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.CountProducts
{
    public sealed class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, int>
    {
        private readonly IProductRepository _repository;

        public CountProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CountProductsQuery query,
            CancellationToken cancellationToken)
        {
            return await _repository.CountAsync(cancellationToken);
        }
    }
}
