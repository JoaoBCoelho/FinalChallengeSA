using System;
using System.Collections.Generic;
using System.Text;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Products.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse> Handle(
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Product '{command.Id}' not found.");

            product.Update(command.Request.Name, command.Request.Description, command.Request.Price);

            await _repository.UpdateAsync(product, cancellationToken);
            return new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        }
    }
}
