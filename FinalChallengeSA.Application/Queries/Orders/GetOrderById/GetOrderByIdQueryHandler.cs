using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IGenericRepository<Order> _repository;

        public GetOrderByIdQueryHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<OrderResponse> Handle(
            GetOrderByIdQuery query,
            CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(query.Id, cancellationToken);
            return order is null
                ? throw new NotFoundException($"Order '{query.Id}' not found.")
                : new OrderResponse(order.Id, order.Name, order.CustomerId, order.Total);
        }
    }
}
