using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.CountProducts
{
    public sealed class CountProductsQueryHandler : IRequestHandler<CountProductsQuery, int>
    {
        private readonly IGenericRepository<Product> _repository;

        public CountProductsQueryHandler(IGenericRepository<Product> repository)
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
