using System;
using System.Collections.Generic;
using System.Text;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Products.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            DeleteProductCommand command,
            CancellationToken cancellationToken)
        {
            var _ = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Product '{command.Id}' not found.");
            await _repository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
