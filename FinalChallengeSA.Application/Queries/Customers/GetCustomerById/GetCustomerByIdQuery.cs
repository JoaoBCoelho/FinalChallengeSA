using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerResponse>;
}
