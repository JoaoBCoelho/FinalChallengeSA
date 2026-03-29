using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Products.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IGenericRepository<Product> _repository;

        public CreateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse> Handle(
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;

            var product = new Product(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.Price);

            await _repository.AddAsync(product, cancellationToken);

            return new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        }
    }
}
