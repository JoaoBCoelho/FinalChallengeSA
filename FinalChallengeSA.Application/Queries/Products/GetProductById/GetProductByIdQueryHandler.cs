using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProductByIdQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse> Handle(
            GetProductByIdQuery query,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(query.Id, cancellationToken);
            return product is null
                ? throw new NotFoundException($"Produto com id '{query.Id}' não encontrado.")
                : new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        }
    }
}
