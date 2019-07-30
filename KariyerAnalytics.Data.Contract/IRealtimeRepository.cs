using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IRealtimeRepository
    {
        long GetRealtimeUserCount(int secondsBefore);
        RealtimeUserCountResponse[] GetEndpointsRealtimeUserCount(int secondsBefore, int? size);
    }
}