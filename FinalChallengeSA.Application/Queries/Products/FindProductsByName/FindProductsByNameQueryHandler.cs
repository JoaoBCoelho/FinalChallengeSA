using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.GetProductsByName
{
    public sealed class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, ProductResponse?>
    {
        private readonly IProductRepository _repository;

        public GetProductsByNameQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse?> Handle(
            GetProductsByNameQuery query,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByNameAsync(query.Name, cancellationToken);

            if (product is null)
            {
                return null;
            }
            return new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        }
    }
}
