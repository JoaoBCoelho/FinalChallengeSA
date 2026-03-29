using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IGenericRepository<Order> _repository;

        public UpdateOrderCommandHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<OrderResponse> Handle(
            UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Order '{command.Id}' not found.");

            var updated = existing with
            {
                Name = command.Request.Name,
                CustomerId = command.Request.CustomerId,
                Total = command.Request.Total
            };
            await _repository.UpdateAsync(updated, cancellationToken);
            return new OrderResponse(updated.Id, updated.Name, updated.CustomerId, updated.Total);
        }
    }
}
