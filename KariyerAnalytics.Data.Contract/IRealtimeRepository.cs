using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IRealtimeRepository
    {
        long GetRealtimeUserCount(int secondsBefore);
        RealtimeUserCountResponse[] GetRealtimeUserCountByEndpoints(int secondsBefore, int? size);
    }
}