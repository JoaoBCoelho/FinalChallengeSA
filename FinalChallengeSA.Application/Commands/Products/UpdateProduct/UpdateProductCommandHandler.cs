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
        private readonly IGenericRepository<Product> _repository;

        public UpdateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponse> Handle(
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Product '{command.Id}' not found.");

            var updated = existing with
            {
                Name = command.Request.Name,
                Price = command.Request.Price
            };
            await _repository.UpdateAsync(updated, cancellationToken);
            return new ProductResponse(updated.Id, updated.Name, updated.Price);
        }
    }
}
