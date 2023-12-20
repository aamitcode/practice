// See https://aka.ms/new-console-template for more information

using DataLayer;
using RateLimitterLib;
using System.Diagnostics;

var rateLimitPolicy = new Policy()
{
    MaxRequest = 4,
    TimeUnitInSeconds = 3
};

var RateLimitCalculator = new RateLimitCalculator(rateLimitPolicy, new DataRepository());
List<string> useIdentifiers = new List<string> { "Test1", "Test2", "Test3" };
for (int i = 0; i < 20; i++)
{
    var useIdentifier = useIdentifiers[0];
    //foreach (var useIdentifier in useIdentifiers)
    {
        string requestTime = DateTime.UtcNow.ToString();
        Stopwatch sw = Stopwatch.StartNew();
        var requestAllow = RateLimitCalculator.IsRequestInLimitAsync(useIdentifier).Result;
        sw.Stop();
        if (requestAllow.IsRequestAllow)
        {
            Console.WriteLine($"{requestTime} Request Identifer:{useIdentifier} RequestCount :{i + 1} is allowed {requestAllow.IsRequestAllow} timetaken:{sw.Elapsed.TotalMilliseconds}");
        }
        else
        {
            Console.WriteLine($"{requestTime} Request Identifer:{useIdentifier} RequestCount :{i + 1} is allowed {requestAllow.IsRequestAllow}, Please retry after :{requestAllow.RetryAfterMiliSeconds} msecs timetaken:{sw.Elapsed.TotalMilliseconds}");
        }

        System.Threading.Thread.Sleep(100);
    }
    //System.Threading.Thread.Sleep(250);
}
