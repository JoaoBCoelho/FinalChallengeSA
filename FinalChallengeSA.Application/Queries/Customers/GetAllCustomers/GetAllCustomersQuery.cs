using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.GetAllCustomers
{
    public sealed record GetAllCustomersQuery() : IRequest<IReadOnlyCollection<CustomerResponse>>;
}
