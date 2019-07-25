using System;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IStatisticRepository
    {
        EndpointAbsoluteMetricsResponse GetBestResponseTime(DateTime after, DateTime before);
        EndpointAbsoluteMetricsResponse GetWorstResponseTime(DateTime after, DateTime before);
        EndpointMetricsResponse[] GetEndpointMetrics(DateTime after, DateTime before);
        EndpointMetricsResponse[] GetEndpointMetricsbyCompany(string companyName, DateTime after, DateTime before);
        EndpointMetricsResponse[] GetEndpointMetricsbyUserandCompany(string companyName, string username, DateTime after, DateTime before);
    }
}
