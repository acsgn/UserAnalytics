using UserAnalytics.Business.Entities;

namespace UserAnalytics.Data.Contract
{
    public interface IRealtimeRepository
    {
        long GetRealtimeUserCount(int secondsBefore);
        RealtimeUserCountResponse[] GetEndpointsRealtimeUserCount(int secondsBefore, int? size);
    }
}