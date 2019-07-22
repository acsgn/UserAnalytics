using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine : IEngine
    {
        MetricResponse GetBestResponseTime(Request request);
        MetricResponse GetWorstResponseTime(Request request);
        long GetRealtimeUsers(int secondsBefore);
        string[] GetEndpoints(Request request);
        int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest);
    }
}
