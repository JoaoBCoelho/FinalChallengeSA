using System;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrderById
{
    public sealed class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public Guid Id { get; }
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
