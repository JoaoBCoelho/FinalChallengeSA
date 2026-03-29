using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyCollection<ProductResponse>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetAllProductsQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ProductResponse>> Handle(
            GetAllProductsQuery query,
            CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);
            return products.Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price)).ToArray();
        }
    }
}
