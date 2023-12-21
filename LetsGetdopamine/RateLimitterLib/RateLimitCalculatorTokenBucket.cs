using DataLayer;
using Dopamine.Entities;

namespace RateLimitterLib
{
    public class RateLimitCalculatorTokenBucket : IRateLimitCalculator
    {
        private readonly IDataRepository<TokenBucket> repository;
        private readonly Policy policy;
        public RateLimitCalculatorTokenBucket(Policy policy, IDataRepository<TokenBucket> repository)
        {
            this.repository = repository;
            this.policy = policy;
        }

        public async Task<RateLimitRespone> IsRequestInLimitAsync(string userIdentifier, long requestedToken)
        {
            await Task.Delay(1);
            var response = new RateLimitRespone()
            {
                IsRequestAllow = true,
                RetryAfterMiliSeconds = 0
            };

            var tokenBucket = await this.refillBucketAsync(userIdentifier);
            if (tokenBucket.availableTokens >= requestedToken)
            {
                tokenBucket.availableTokens -= requestedToken;
                response= new RateLimitRespone()
                {
                    IsRequestAllow = true,
                    RetryAfterMiliSeconds = 0
                };
            }
            else
            {
                DateTime now = DateTime.UtcNow;
                var duration = now.Subtract(tokenBucket.lastRefreshedTime);
                var refillTimeLeft = (tokenBucket.refillRate - duration.TotalSeconds) * 1000;
                response = new RateLimitRespone()
                {
                    IsRequestAllow = false,
                    RetryAfterMiliSeconds = (int)refillTimeLeft
                };
            };
            await this.repository.UpdateBucketAsync(userIdentifier, tokenBucket);
            return response;
        }
        private async Task<TokenBucket> refillBucketAsync(string userIdentifier)
        {
            var tokenBucket = await this.repository.GetBucketAsync(userIdentifier);
            if (tokenBucket == null)
            {
                tokenBucket = new TokenBucket()
                {
                    availableTokens = this.policy.MaxRequest,
                    MaxToken = this.policy.MaxRequest,
                    refillRate = this.policy.TimeUnitInSeconds,
                    lastRefreshedTime = DateTime.UtcNow
                };
            }
            else
            {
                DateTime now = DateTime.UtcNow;
                var duration = now.Subtract(tokenBucket.lastRefreshedTime);
                var tokensToAdd = Math.Floor(tokenBucket.refillRate * duration.TotalSeconds);
                if (tokensToAdd > 0)
                {
                    tokenBucket.availableTokens = Math.Min(tokenBucket.availableTokens + (long)tokensToAdd, tokenBucket.MaxToken);
                    tokenBucket.lastRefreshedTime = now;
                }
            }
            return tokenBucket;
        }
    }
}
