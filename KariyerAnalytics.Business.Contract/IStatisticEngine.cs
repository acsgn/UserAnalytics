using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine : IEngine
    {
        MetricResponseDTO GetBestResponseTime(Request request);
        MetricResponseDTO GetWorstResponseTime(Request request);
        RealtimeUserMetricDTO[] GetRealtimeUsers(RealtimeUserCountRequest realtimeUserCountRequest);
        string[] GetEndpoints(Request request);
        HistogramDTO[] GetResponseTimes(ResponseTimeRequest responseTimeRequest);
        HistogramDTO[] GetResponseTimesByEndpoint(ResponseTimeRequest responseTimeRequest);
    }
}
