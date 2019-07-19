using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine
    {
        MetricResponse GetBestResponseTime(Request request);
        MetricResponse GetWorstResponseTime(Request request);
        string[] GetEndpoints(Request request);
        int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest);
    }
}
