/****************************************************************************************************
 * Author - Amit Chauhan
 * Description - Policy Class
 ****************************************************************************************************/

namespace RateLimitterLib
{
    public class Policy
    {
        public int TimeUnitInSeconds { get; set; }
        public int MaxRequest { get; set; }

    }
}
