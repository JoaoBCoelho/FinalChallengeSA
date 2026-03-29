using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommand : IRequest<OrderResponse>
    {
        public OrderRequest Request { get; }
        public CreateOrderCommand(OrderRequest request)
        {
            Request = request;
        }
    }
}
