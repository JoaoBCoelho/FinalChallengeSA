using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Queries.Orders.GetAllOrders
{
    public sealed class GetAllOrdersQuery : IRequest<IReadOnlyCollection<OrderResponse>>
    {
    }
}
