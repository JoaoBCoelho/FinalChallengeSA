using System;
using System.Collections.Generic;
using System.Text;

namespace FinalChallengeSA.Application.DTOs
{
    public sealed record OrderRequest(string Name, Guid CustomerId, decimal Total);
    public sealed record OrderResponse(Guid Id, string Name, Guid CustomerId, decimal Total);
}
