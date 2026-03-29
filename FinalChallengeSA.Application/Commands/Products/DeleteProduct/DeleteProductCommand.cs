using System;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Products.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest
    {
        public Guid Id { get; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
