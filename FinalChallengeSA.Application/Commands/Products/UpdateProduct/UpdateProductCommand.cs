using System;
using MediatR;
using FinalChallengeSA.Application.DTOs;

namespace FinalChallengeSA.Application.Commands.Products.UpdateProduct
{
    public sealed class UpdateProductCommand : IRequest<ProductResponse>
    {
        public Guid Id { get; }
        public ProductRequest Request { get; }
        public UpdateProductCommand(Guid id, ProductRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
