using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.CountCustomers
{
    public sealed record CountCustomersQuery() : IRequest<int>;
}
