using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrdersByName
{
    public sealed class GetOrdersByNameQuery : IRequest<IReadOnlyCollection<OrderResponse>>
    {
        public string Name { get; }
        public GetOrdersByNameQuery(string name)
        {
            Name = name;
        }
    }
}
