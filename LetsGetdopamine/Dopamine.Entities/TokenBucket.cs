namespace Dopamine.Entities
{
    public class TokenBucket
    {
        public long availableTokens { get; set; }
        public long MaxToken { get; set; }
        public long refillRate { get; set; }
        public DateTime lastRefreshedTime { get; set; }
    }
}
