using System;
using System.Collections.Generic;
using System.Text;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Orders.GetOrdersByName
{
    public sealed class GetOrdersByNameQueryHandler : IRequestHandler<GetOrdersByNameQuery, IReadOnlyCollection<OrderResponse>>
    {
        private readonly IGenericRepository<Order> _repository;

        public GetOrdersByNameQueryHandler(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<OrderResponse>> Handle(
            GetOrdersByNameQuery query,
            CancellationToken cancellationToken)
        {
            var orders = await _repository.GetByNameAsync(query.Name, cancellationToken);

            if (orders is null)
            {
                return [];
            }
            return [.. orders.Select(order => new OrderResponse(order.Id, order.Name, order.CustomerId, order.Total))];
        }
    }
}
