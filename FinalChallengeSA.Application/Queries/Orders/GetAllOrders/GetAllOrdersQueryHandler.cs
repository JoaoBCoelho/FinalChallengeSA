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
        private readonly IGenericRepository<Order> _repository;

        public GetAllOrdersQueryHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<OrderResponse>> Handle(
            GetAllOrdersQuery query,
            CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAllAsync(cancellationToken);
            return orders.Select(o => new OrderResponse(o.Id, o.Name, o.CustomerId, o.Total)).ToArray();
        }
    }
}
