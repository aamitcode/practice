using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitterLib
{
    public sealed class RateLimitRespone
    {
        public bool IsRequestAllow { get; set; }
        public int RetryAfterMiliSeconds { get; set; }
    }
}
