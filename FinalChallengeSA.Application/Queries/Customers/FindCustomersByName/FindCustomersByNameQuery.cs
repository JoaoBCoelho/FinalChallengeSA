using System;
using System.Collections.Generic;
using System.Text;
using FinalChallengeSA.Application.DTOs;
using MediatR;

namespace FinalChallengeSA.Application.Queries.Customers.GetCustomersByName
{
    public sealed record GetCustomersByNameQuery(string Name) : IRequest<IReadOnlyCollection<CustomerResponse>>;
}
