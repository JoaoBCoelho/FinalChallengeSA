using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public GetOrderByIdQueryHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(
            GetOrderByIdQuery query,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(query.Id, cancellationToken)
                ?? throw new NotFoundException($"Order '{query.Id}' não encontrada.");
            return CreateResponse(order);
        }

        private static OrderResponse CreateResponse(Order order)
        {
            var productsResponse = order.Products.Select(p => new OrderProductResponse(p.Id, p.Name, p.Description, p.Price)).ToList();
            return new OrderResponse(order.Id, order.CustomerId, productsResponse, order.TotalAmount);
        }
    }
}
