/****************************************************************************************************
 * Author - Amit Chauhan
 * Description - Request Limit calculator
 ****************************************************************************************************/

namespace RateLimitterLib
{
    using DataLayer;
    using Dopamine.Entities;

    public class RateLimitCalculator : IRateLimitCalculator
    {
        private readonly Policy rateLimitPolicy;
        private readonly IDataRepository<TokenBucketWithCredit> dataRepository;

        /// <summary>
        /// Inject rate limit policy and data repo for testing
        /// </summary>
        /// <param name="rateLimitPolicy">rate limit policy</param>
        /// <param name="dataRepository">data layer</param>
        public RateLimitCalculator(Policy rateLimitPolicy, IDataRepository<TokenBucketWithCredit> dataRepository)
        {
            this.rateLimitPolicy = rateLimitPolicy;
            this.dataRepository = dataRepository;
        }

        /// <summary>
        /// Request limit validator
        /// </summary>
        /// <param name="userIdentifier">identifier</param>
        /// <returns>Response : is allowed and retry after</returns>
        public async Task<RateLimitRespone> IsRequestInLimitAsync(string userIdentifier, long requestedTokens = 1)
        {
            RateLimitRespone response = new RateLimitRespone() { IsRequestAllow = false, RetryAfterMiliSeconds = -1 };
            long requestTime = DateTime.UtcNow.Ticks;
            var requestUsage = await dataRepository.GetRequestUsagesAsyncs(userIdentifier);

            if (requestUsage.RequestCount <= this.rateLimitPolicy.MaxRequest) // Happy Path - total request count in under limit - Actions - Increase the request count and allow
            {
                await this.dataRepository.InsertLatestAsync(userIdentifier, requestTime, false); 
                response.IsRequestAllow = true;
            }
            else
            {
                var lookBackPeriod = DateTime.UtcNow.AddSeconds(-1 * rateLimitPolicy.TimeUnitInSeconds).Ticks;

                if (requestUsage.OldestRequestTimeStamp < lookBackPeriod) // Happy Path - oldest request is outside of the window, drop that request from cache, Actions - Allow and drop the oldest request + Insert the latest
                {
                    await this.dataRepository.InsertLatestAsync(userIdentifier, requestTime, true);
                    response.IsRequestAllow = true;
                }
                else // Unhappy path - Count out of threshold and oldest request with in the window, Action - Drop the request & don't modify request usages.
                {
                    long delayTicks = requestUsage.OldestRequestTimeStamp - lookBackPeriod;
                    TimeSpan elapsedSpan = new TimeSpan(delayTicks);
                    response.RetryAfterMiliSeconds = (int)elapsedSpan.TotalMilliseconds;
                }
            }

            return response;
        }
    }
}
