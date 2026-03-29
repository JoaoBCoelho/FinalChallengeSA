using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(Guid Id, CustomerRequest Request) : IRequest<CustomerResponse>;
}
