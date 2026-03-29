using System;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommand : IRequest<OrderResponse>
    {
        public Guid Id { get; }
        public OrderRequest Request { get; }
        public UpdateOrderCommand(Guid id, OrderRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
