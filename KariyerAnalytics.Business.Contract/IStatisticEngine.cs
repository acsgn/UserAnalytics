using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine : IEngine
    {
        EndpointAbsoluteMetricsResponseDTO GetBestResponseTime(Request request);
        EndpointAbsoluteMetricsResponseDTO GetWorstResponseTime(Request request);
        EndpointMetricsResponseDTO[] GetEndpointMetrics(StatisticRequest request);
        EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompany(StatisticRequest request);
        EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompanyAndUser(StatisticRequest request);
    }
}
