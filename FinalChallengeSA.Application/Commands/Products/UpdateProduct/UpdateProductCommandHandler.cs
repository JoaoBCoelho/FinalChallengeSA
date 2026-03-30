using FinalChallengeSA.Application.Commands.Products.CreateProduct;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalChallengeSA.Application.Commands.Products.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IProductRepository repository, IValidator<UpdateProductCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ProductResponse> Handle(
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Produto com id '{command.Id}' não encontrado.");

            product.Update(command.Request.Name, command.Request.Description, command.Request.Price);

            await _repository.UpdateAsync(product, cancellationToken);
            return new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        }
    }
}
