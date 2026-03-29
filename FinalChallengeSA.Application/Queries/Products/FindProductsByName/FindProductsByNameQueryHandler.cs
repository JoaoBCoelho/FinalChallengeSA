using System;
using System.Collections.Generic;
using System.Text;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.GetProductsByName
{
    public sealed class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IReadOnlyCollection<ProductResponse>>
    {
        private readonly IGenericRepository<Product> _repository;

        public GetProductsByNameQueryHandler(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ProductResponse>> Handle(
            GetProductsByNameQuery query,
            CancellationToken cancellationToken)
        {
            var products = await _repository.GetByNameAsync(query.Name, cancellationToken);

            if (products is null)
            {
                return [];
            }
            return [.. products.Select(product => new ProductResponse(product.Id, product.Name, product.Price))];
        }
    }
}
