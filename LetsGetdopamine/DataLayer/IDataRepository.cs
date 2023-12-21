/****************************************************************************************************
 * Author - Amit Chauhan
 * Description - Data layer 
 ****************************************************************************************************/

namespace DataLayer
{
    using Dopamine.Entities;

    public interface IDataRepository<T> where T : class
    {
        public Task<UserRequestUsage> GetRequestUsagesAsyncs(string userIdentifier);
        public Task InsertLatestAsync(string userIdentifier, long requestTimeStamp, bool dropOldest = false);

        public Task<T> GetBucketAsync(string userIdentifier);
        public Task UpdateBucketAsync(string userIdentifier, T bucket);
    }
}
