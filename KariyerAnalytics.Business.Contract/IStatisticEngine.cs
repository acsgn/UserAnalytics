using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine : IEngine
    {
        EndpointAbsoluteMetricsResponseDTO GetBestResponseTime(Request request);
        EndpointAbsoluteMetricsResponseDTO GetWorstResponseTime(Request request);
        EndpointMetricsResponseDTO[] GetEndpointMetrics(Request request);
        EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompany(UserRequest userRequest);
        EndpointMetricsResponseDTO[] GetEndpointMetricsbyUserandCompany(EndpointRequest endpointRequest);
    }
}
