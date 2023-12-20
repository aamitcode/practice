/****************************************************************************************************
 * Author - Amit Chauhan
 * Description - Request usage object
 ****************************************************************************************************/

namespace DataLayer
{
    public sealed class UserRequestUsage
    {
        public long OldestRequestTimeStamp { get; set; }
        public int RequestCount { get; set; }
    }
}
