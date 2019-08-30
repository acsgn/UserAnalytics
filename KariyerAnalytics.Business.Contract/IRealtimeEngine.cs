using UserAnalytics.Service.Entities;

namespace UserAnalytics.Business.Contract
{
    public interface IRealtimeEngine : IEngine
    {
        long GetRealtimeUserCount(RealtimeRequest realtimeUserCountRequest);
        RealtimeUserCountResponseDTO[] GetEndpointsRealtimeUserCount(RealtimeRequest realtimeUserCountRequest);
    }
}
