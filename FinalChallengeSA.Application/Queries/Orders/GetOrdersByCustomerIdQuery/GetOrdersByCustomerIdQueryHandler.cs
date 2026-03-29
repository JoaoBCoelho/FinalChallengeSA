using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrdersByCustomerIdQuery
{
    public sealed class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IReadOnlyCollection<OrderResponse>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersByCustomerIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<OrderResponse>> Handle(
            GetOrdersByCustomerIdQuery query,
            CancellationToken cancellationToken)
        {
            var orders = await _repository.GetByCustomerIdAsync(query.CustomerId, cancellationToken);
            if (orders is null)
            {
                return [];
            }
            return [.. orders.Select(CreateResponse)];
        }

        private static OrderResponse CreateResponse(Order order)
        {
            var productsResponse = order.Products.Select(p => new OrderProductResponse(p.Id, p.Name, p.Description, p.Price)).ToList();
            return new OrderResponse(order.Id, order.CustomerId, productsResponse, order.TotalAmount);
        }
    }
}
