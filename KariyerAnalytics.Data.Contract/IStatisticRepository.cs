using System;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IStatisticRepository
    {
        MetricResponse GetBestResponseTime(DateTime after, DateTime before);

        MetricResponse GetWorstResponseTime(DateTime after, DateTime before);

        RealtimeUserMetric[] GetRealtimeUsers(int secondsBefore);

        string[] GetEndpoints(DateTime after, DateTime before);

        Histogram[] GetResponseTimes(string endpoint, TimeSpan interval, DateTime after, DateTime before);
    }
}
