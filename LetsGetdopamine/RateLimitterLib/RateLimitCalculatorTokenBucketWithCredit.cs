using DataLayer;
using Dopamine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateLimitterLib
{
    public class RateLimitCalculatorTokenBucketWithCredit : IRateLimitCalculator
    {
        private readonly IDataRepository<TokenBucketWithCredit> repository;
        private readonly PolicyWithCredit policy;
        public RateLimitCalculatorTokenBucketWithCredit(PolicyWithCredit policy, IDataRepository<TokenBucketWithCredit> repository)
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
            if (tokenBucket.availableTokens + tokenBucket.availableCreditTokens >= requestedToken)
            {
                tokenBucket.availableTokens -= requestedToken;

                // Burn Credit if available token underlfow.
                if(tokenBucket.availableTokens < 0)
                {
                    tokenBucket.availableCreditTokens += tokenBucket.availableTokens;
                    tokenBucket.availableTokens = 0;
                }

                response = new RateLimitRespone()
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
        private async Task<TokenBucketWithCredit> refillBucketAsync(string userIdentifier)
        {
            var tokenBucket = await this.repository.GetBucketAsync(userIdentifier);
            if (tokenBucket == null)
            {
                tokenBucket = new TokenBucketWithCredit()
                {
                    availableTokens = this.policy.MaxRequest,
                    MaxToken = this.policy.MaxRequest,
                    refillRate = this.policy.TimeUnitInSeconds,
                    lastRefreshedTime = DateTime.UtcNow,
                    availableCreditTokens =0,
                    maxCreditTokens = this.policy.MaxCredit
                };
            }
            else
            {
                DateTime now = DateTime.UtcNow;
                var duration = now.Subtract(tokenBucket.lastRefreshedTime);
                var tokensToAdd = Math.Floor(tokenBucket.refillRate * duration.TotalSeconds);
                tokensToAdd = tokenBucket.availableTokens + tokensToAdd;
                if (tokensToAdd > 0)
                {
                    tokenBucket.availableTokens = Math.Min((long)tokensToAdd, tokenBucket.MaxToken);
                    if(tokenBucket.MaxToken < tokensToAdd)
                    {
                        var creditTokentoAdd = tokensToAdd - tokenBucket.MaxToken;
                        tokenBucket.availableCreditTokens = Math.Min(tokenBucket.availableCreditTokens + (long)creditTokentoAdd, tokenBucket.maxCreditTokens);
                    }
                    tokenBucket.lastRefreshedTime = now;
                }
            }
            return tokenBucket;
        }
    }
}
