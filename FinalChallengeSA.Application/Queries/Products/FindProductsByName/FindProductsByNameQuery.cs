using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Queries.Products.GetProductsByName
{
    public sealed class GetProductsByNameQuery : IRequest<IReadOnlyCollection<ProductResponse>>
    {
        public string Name { get; }
        public GetProductsByNameQuery(string name)
        {
            Name = name;
        }
    }
}
