/****************************************************************************************************
 * Author - Amit Chauhan
 * Description - Data layer in memory implementation
 ****************************************************************************************************/

namespace DataLayer
{
    using Dopamine.Entities;
    using System.Collections.Concurrent;

    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        //Using Hash Table with concrrency support
        private ConcurrentDictionary<string, Queue<long>> requestWindow = new ConcurrentDictionary<string, Queue<long>>();
        private ConcurrentDictionary<string, T> bucketMap = new ConcurrentDictionary<string, T>();

        /// <summary>
        /// Insert and drop the entries from data base
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="requestTimeStamp">Request time stam which needs to be stored</param>
        /// <param name="dropOldest">need to drop the oldest entry</param>
        /// <returns>Task</returns>
        public async Task InsertLatestAsync(string userIdentifier, long requestTimeStamp, bool dropOldest = false)
        {
            if (requestWindow.ContainsKey(userIdentifier))
            {
                requestWindow[userIdentifier].Enqueue(requestTimeStamp);
                if (dropOldest)
                {
                    requestWindow[userIdentifier].Dequeue();
                }
            }
            else
            {
                var requestQueue = requestWindow.GetOrAdd(userIdentifier, new Queue<long>());
                requestQueue.Enqueue(requestTimeStamp);
            }
            await Task.Delay(1);
        }

        /// <summary>
        /// Fetch oldest entry and total request count
        /// </summary>
        /// <param name="userIdentifier">user identifier</param>
        /// <returns>Response :Oldest timestamp, Request Count </returns>
        public async Task<UserRequestUsage> GetRequestUsagesAsyncs(string userIdentifier)
        {
            var response = new UserRequestUsage() { OldestRequestTimeStamp = 0, RequestCount = 0 };
            if (requestWindow.ContainsKey(userIdentifier))
            {
                response.OldestRequestTimeStamp = requestWindow[userIdentifier].Peek();
                response.RequestCount = requestWindow[userIdentifier].Count;
            }

            await Task.Delay(1);
            return response;
        }

        public async Task<T> GetBucketAsync(string userIdentifier)
        {
            if (this.bucketMap.ContainsKey(userIdentifier))
            {
                return this.bucketMap[userIdentifier];
            }

            return default(T);
        }

        public async Task UpdateBucketAsync(string userIdentifier, T bucket)
        {
            if (this.bucketMap.ContainsKey(userIdentifier))
            {
                this.bucketMap[userIdentifier] = bucket;
            }
            else
            {
                this.bucketMap.GetOrAdd(userIdentifier, bucket);
            }
        }
    }
}
