using System;
using System.Collections.Generic;
using System.Text;

namespace FinalChallengeSA.Application.DTOs
{
    public sealed record CreateCustomerRequest(string Name, string Email);

    public sealed record UpdateCustomerRequest(string Name, string Email);

    public sealed record CustomerResponse(Guid Id, string Name, string Email);
}
