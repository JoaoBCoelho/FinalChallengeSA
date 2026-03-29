using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IGenericRepository<Order> _repository;

        public DeleteOrderCommandHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            DeleteOrderCommand command,
            CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Order '{command.Id}' not found.");

            await _repository.DeleteAsync(existing, cancellationToken);
        }
    }
}
