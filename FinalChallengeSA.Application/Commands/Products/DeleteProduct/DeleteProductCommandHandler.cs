using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
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
            var _ = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Produto com id '{command.Id}' não encontrado.");
            var isInAnyOrder = await _repository.IsInAnyOrderAsync(command.Id, cancellationToken);
            if (isInAnyOrder)
            {
                throw new InvalidOperationException($"Produto com id '{command.Id}' não pode ser deletado pois está presente em um ou mais pedidos.");
            }

            await _repository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
