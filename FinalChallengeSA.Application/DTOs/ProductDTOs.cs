using System;
using System.Collections.Generic;
using System.Text;

namespace FinalChallengeSA.Application.DTOs
{
    public sealed record ProductRequest(string Name, decimal Price);
    public sealed record ProductResponse(Guid Id, string Name, decimal Price);
}
