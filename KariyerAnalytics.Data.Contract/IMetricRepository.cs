using System;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IMetricRepository
    {
        MetricsResponse[] GetEndpointMetrics(string companyName, string username, int? size, bool? ascending, DateTime? after, DateTime? before);
        MetricsResponse[] GetCompanyMetrics(string endpoint, int? size, bool? ascending, DateTime? after, DateTime? before);
        MetricsResponse GetSingleMetric(string companyName, string username, string endpoint, DateTime? after, DateTime? before);
    }
}
