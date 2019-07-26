using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Contract;
using System.Linq;

namespace KariyerAnalytics.Business
{
    public class StatisticEngine : IStatisticEngine
    {
        private readonly IStatisticRepository _StatisticRepository;

        public StatisticEngine(IStatisticRepository statisticRepository)
        {
            _StatisticRepository = statisticRepository;
        }

        public EndpointAbsoluteMetricsResponseDTO GetBestResponseTime(Request request)
        {
            var result = _StatisticRepository.GetBestResponseTime(request.After, request.Before);
            return new EndpointAbsoluteMetricsResponseDTO
            {
                Endpoint = result.Endpoint,
                AverageResponseTime = result.AverageResponseTime
            };
        }

        public EndpointAbsoluteMetricsResponseDTO GetWorstResponseTime(Request request)
        {
            var result = _StatisticRepository.GetWorstResponseTime(request.After, request.Before);
            return new EndpointAbsoluteMetricsResponseDTO
            {
                Endpoint = result.Endpoint,
                AverageResponseTime = result.AverageResponseTime
            };
        }

        public EndpointMetricsResponseDTO[] GetEndpointMetrics(StatisticRequest request)
        {
            var result = _StatisticRepository.GetEndpointMetrics(request.After, request.Before);
            return (from r in result
                    select new EndpointMetricsResponseDTO
                    {
                        Endpoint = r.Endpoint,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }

        public EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompany(StatisticRequest userRequest)
        {
            var result = _StatisticRepository.GetEndpointMetrics(userRequest.After, userRequest.Before, userRequest.CompanyName);
            return (from r in result
                    select new EndpointMetricsResponseDTO
                    {
                        Endpoint = r.Endpoint,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }

        public EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompanyAndUser(StatisticRequest endpointRequest)
        {
            var result = _StatisticRepository.GetEndpointMetrics(endpointRequest.After, endpointRequest.Before, endpointRequest.CompanyName, endpointRequest.Username);
            return (from r in result
                    select new EndpointMetricsResponseDTO
                    {
                        Endpoint = r.Endpoint,
                        NumberOfRequests = r.NumberOfRequests,
                        MinResponseTime = r.MinResponseTime,
                        AverageResponseTime = r.AverageResponseTime,
                        MaxResponseTime = r.MaxResponseTime
                    }).ToArray();
        }
    }
}
