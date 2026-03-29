using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Commands.Products.CreateProduct
{
    public sealed class CreateProductCommand : IRequest<ProductResponse>
    {
        public ProductRequest Request { get; }
        public CreateProductCommand(ProductRequest request)
        {
            Request = request;
        }
    }
}
