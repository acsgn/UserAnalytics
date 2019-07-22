using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine : IEngine
    {
        MetricResponseDTO GetBestResponseTime(Request request);
        MetricResponseDTO GetWorstResponseTime(Request request);
        RealtimeUserMetricDTO[] GetRealtimeUsers(int secondsBefore);
        string[] GetEndpoints(Request request);
        int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest);
    }
}
