// See https://aka.ms/new-console-template for more information

using RateLimitterLib;
using System.Diagnostics;

var rateLimitPolicy = new Policy()
{
    MaxRequest = 8,
    TimeUnitInSeconds = 10
};

var RateLimitCalculator = new RateLimitCalculator(rateLimitPolicy);
List<string> useIdentifiers = new List<string> { "Test1", "Test2", "Test3" };
for (int i = 0; i < 20; i++)
{
    //var useIdentifier = useIdentifiers[0];
    foreach (var useIdentifier in useIdentifiers)
    {
        int retryAfter = 0;
        string requestTime = DateTime.UtcNow.ToString();
        Stopwatch sw = Stopwatch.StartNew();
        var requestAllow = RateLimitCalculator.IsRequestInLimit(useIdentifier, out retryAfter);
        sw.Stop();
        if (requestAllow)
        {
            Console.WriteLine($"{requestTime} Request Identifer:{useIdentifier} RequestCount :{i + 1} is allowed {requestAllow} timetaken:{sw.Elapsed.TotalMilliseconds}");
        }
        else
        {
            Console.WriteLine($"{requestTime} Request Identifer:{useIdentifier} RequestCount :{i + 1} is allowed {requestAllow}, Please retry after :{retryAfter} msecs timetaken:{sw.Elapsed.TotalMilliseconds}");
        }

        System.Threading.Thread.Sleep(100);
    }
    System.Threading.Thread.Sleep(250);
}
