using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(Guid Id) : IRequest;
}
