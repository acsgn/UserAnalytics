using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IRealtimeEngine : IEngine
    {
        long GetRealtimeUserCount(RealtimeRequest realtimeUserCountRequest);
        RealtimeUserCountResponseDTO[] GetEndpointsRealtimeUserCount(RealtimeRequest realtimeUserCountRequest);
    }
}
