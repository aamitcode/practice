using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine.Entities
{
    public class TokenBucketWithCredit : TokenBucket
    {
        public long availableCreditTokens { get; set; }
        public long maxCreditTokens { get; set; }
    }
}
