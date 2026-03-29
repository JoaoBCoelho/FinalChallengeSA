using System;
using System.Threading;
using System.Threading.Tasks;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IGenericRepository<Order> _repository;

        public CreateOrderCommandHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<OrderResponse> Handle(
            CreateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;
            var order = new Order(
                Guid.NewGuid(),
                request.CustomerId,
                request.Total);
            await _repository.AddAsync(order, cancellationToken);

            return new OrderResponse(order.Id, order.Name, order.CustomerId, order.TotalAmount);
        }
    }
}
