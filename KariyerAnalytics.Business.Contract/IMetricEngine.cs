using UserAnalytics.Service.Entities;

namespace UserAnalytics.Business.Contract
{
    public interface IMetricEngine : IEngine
    {
        MetricsResponseDTO[] GetEndpointMetrics(MetricRequest request);
        MetricsResponseDTO[] GetCompanyMetrics(MetricRequest request);
        MetricsResponseDTO GetSingleMetric(MetricRequest request);
    }
}
