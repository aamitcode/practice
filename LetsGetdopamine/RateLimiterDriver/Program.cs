// See https://aka.ms/new-console-template for more information

using DataLayer;
using Dopamine.Entities;
using RateLimitterLib;
using System.Diagnostics;

var rateLimitPolicy = new PolicyWithCredit()
{
    MaxRequest = 4,
    MaxCredit = 2,
    TimeUnitInSeconds = 1
};

var RateLimitCalculator = new RateLimitCalculatorTokenBucketWithCredit(rateLimitPolicy, new DataRepository<TokenBucketWithCredit>());
List<string> useIdentifiers = new List<string> { "Test1", "Test2", "Test3" };
for (int i = 0; i < 20; i++)
{
    var useIdentifier = useIdentifiers[0];
    //foreach (var useIdentifier in useIdentifiers)
    {
        string requestTime = DateTime.UtcNow.ToString();
        Stopwatch sw = Stopwatch.StartNew();
        var requestAllow = RateLimitCalculator.IsRequestInLimitAsync(useIdentifier,1).Result;
        sw.Stop();
        if (requestAllow.IsRequestAllow)
        {
            Console.WriteLine($"{requestTime} Request Identifer:{useIdentifier} RequestCount :{i + 1} is allowed {requestAllow.IsRequestAllow} timetaken:{sw.Elapsed.TotalMilliseconds}");
        }
        else
        {
            Console.WriteLine($"{requestTime} Request Identifer:{useIdentifier} RequestCount :{i + 1} is allowed {requestAllow.IsRequestAllow}, Please retry after :{requestAllow.RetryAfterMiliSeconds} msecs timetaken:{sw.Elapsed.TotalMilliseconds}");
        }

        System.Threading.Thread.Sleep(300);
    }
    //System.Threading.Thread.Sleep(250);
}
