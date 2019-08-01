using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IMetricEngine : IEngine
    {
        MetricsResponseDTO[] GetEndpointMetrics(MetricRequest request);
        MetricsResponseDTO[] GetCompanyMetrics(MetricRequest request);
        MetricsResponseDTO GetSingleMetric(MetricRequest request);
    }
}
