using System;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.DeleteOrder
{
    public sealed class DeleteOrderCommand : IRequest
    {
        public Guid Id { get; }
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}
