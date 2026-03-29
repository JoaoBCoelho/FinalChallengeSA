using System;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Queries.Products.GetProductById
{
    public sealed class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public Guid Id { get; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
