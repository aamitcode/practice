using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitterLib
{
    public interface IRateLimitCalculator
    {
        public Task<RateLimitRespone> IsRequestInLimitAsync(string userIdentifier, long requestedTokens = 1);
    }
}
