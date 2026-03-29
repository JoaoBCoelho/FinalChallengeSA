using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Queries.Products.GetAllProducts
{
    public sealed class GetAllProductsQuery : IRequest<IReadOnlyCollection<ProductResponse>>
    {
    }
}
