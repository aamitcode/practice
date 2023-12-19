using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitterLib
{
    public class RateLimitCalculator : IRateLimitCalculator
    {
        private readonly Policy rateLimitPolicy;
        private Dictionary<string,Queue<long>> requestWindow = new Dictionary<string, Queue<long>>();

        public RateLimitCalculator(Policy rateLimitPolicy)
        {
            this.rateLimitPolicy = rateLimitPolicy;
        }
        public bool IsRequestInLimit(string userIdentifier, out int retryAftermiliSeconds)
        {
            retryAftermiliSeconds = 0;
            long requestTime = DateTime.UtcNow.Ticks;
            bool isRequestAllowed = false;
            if(requestWindow.ContainsKey(userIdentifier))
            {
                Queue<long> requestUserRequestHistory = requestWindow[userIdentifier];
                if(requestUserRequestHistory.Count > rateLimitPolicy.MaxRequest - 1)
                {
                    var lookBackPeriod = DateTime.UtcNow.AddSeconds(-1 * rateLimitPolicy.TimeUnitInSeconds).Ticks;
                    if (requestWindow[userIdentifier].Peek() < lookBackPeriod) // oldest request in cache is outside of the window, drop that request from cache 
                    {
                        requestWindow[userIdentifier].Dequeue();
                        requestWindow[userIdentifier].Enqueue(requestTime);
                        isRequestAllowed = true;
                    }
                    else
                    {
                        long delayTicks = requestWindow[userIdentifier].Peek() - lookBackPeriod;
                        TimeSpan elapsedSpan = new TimeSpan(delayTicks);
                        retryAftermiliSeconds = (int)elapsedSpan.TotalMilliseconds;
                        //retryAftermiliSeconds = retryAftermiliSeconds == 0 ? 1 : retryAftermiliSeconds;//Minimum time 1 second
                    }
                }
                else // Total request in cache is below max allowed request
                {
                    requestWindow[userIdentifier].Enqueue(requestTime);
                    isRequestAllowed = true;
                }
            }
            else // First request from identifier
            {
                requestWindow.Add(userIdentifier, new Queue<long>());
                requestWindow[userIdentifier].Enqueue(requestTime);
                isRequestAllowed = true;
            }

            return isRequestAllowed;
        }
    }
}
