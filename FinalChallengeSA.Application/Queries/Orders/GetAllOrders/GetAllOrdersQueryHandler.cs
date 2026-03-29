using System;
using System.Collections.Generic;
using System.Text;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.GetAllOrders
{
    public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IReadOnlyCollection<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IReadOnlyCollection<OrderResponse>> Handle(
            GetAllOrdersQuery query,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
            return orders.Select(CreateResponse).ToArray();
        }

        private static OrderResponse CreateResponse(Order order)
        {
            var productsResponse = order.Products.Select(p => new OrderProductResponse(p.Id, p.Name, p.Description, p.Price)).ToList();
            return new OrderResponse(order.Id, order.CustomerId, productsResponse, order.TotalAmount);
        }
    }
}
