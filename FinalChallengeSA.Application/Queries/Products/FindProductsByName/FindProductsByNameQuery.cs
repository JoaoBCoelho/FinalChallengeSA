using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Products.GetProductsByName
{
    public sealed class GetProductsByNameQuery : IRequest<ProductResponse>
    {
        public string Name { get; }
        public GetProductsByNameQuery(string name)
        {
            Name = name;
        }
    }
}
