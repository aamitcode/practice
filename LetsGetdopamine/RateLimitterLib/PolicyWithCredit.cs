using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitterLib
{
    public class PolicyWithCredit : Policy
    {
        public int MaxCredit { get; set; }
    }
}
