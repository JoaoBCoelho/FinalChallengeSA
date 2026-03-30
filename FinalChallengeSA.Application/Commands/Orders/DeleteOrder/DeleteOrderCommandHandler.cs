using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _repository;

        public DeleteOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            DeleteOrderCommand command,
            CancellationToken cancellationToken)
        {
            var _ = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Pedido com id '{command.Id}' não encontrado.");
            await _repository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
