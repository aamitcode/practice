/****************************************************************************************************
 * Author - Amit Chauhan
 * Description - Data layer 
 ****************************************************************************************************/
namespace DataLayer
{
    public interface IDataRepository
    {
        public Task<UserRequestUsage> GetRequestUsagesAsyncs(string userIdentifier);
        public Task InsertLatestAsync(string userIdentifier, long requestTimeStamp, bool dropOldest = false);
    }
}
