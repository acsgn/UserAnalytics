using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IRealtimeEngine
    {
        long GetRealtimeUserCount(RealtimeUserCountRequest realtimeUserCountRequest);
        RealtimeUserCountResponseDTO[] GetRealtimeUserCountByEndpoints(RealtimeUserCountRequest realtimeUserCountRequest);
    }
}
