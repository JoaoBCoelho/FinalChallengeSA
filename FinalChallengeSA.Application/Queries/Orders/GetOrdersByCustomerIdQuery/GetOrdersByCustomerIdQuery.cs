using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrdersByCustomerIdQuery
{
    public sealed class GetOrdersByCustomerIdQuery : IRequest<IReadOnlyCollection<OrderResponse>>
    {
        public Guid CustomerId { get; }
        public GetOrdersByCustomerIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
