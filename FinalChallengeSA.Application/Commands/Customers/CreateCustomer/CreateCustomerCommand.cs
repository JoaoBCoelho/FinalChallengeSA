using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.CreateCustomer
{
    public sealed record CreateCustomerCommand(CustomerRequest Request) : IRequest<CustomerResponse>;
}
