using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Products.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IProductRepository repository,
            IValidator<CreateProductCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ProductResponse> Handle(
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var request = command.Request;
            await ValidateProductWithSameName(request);

            var product = new Product(
                request.Name,
                request.Description,
                request.Price);

            await _repository.AddAsync(product, cancellationToken);

            return new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        }

        private async Task ValidateProductWithSameName(ProductRequest request)
        {
            var existingProductWithSameName = await _repository.GetByNameAsync(request.Name);
            if (existingProductWithSameName is not null)
            {
                throw new ConflictException("Já existe um produto criado com o mesmo nome.");
            }
        }
    }
}
