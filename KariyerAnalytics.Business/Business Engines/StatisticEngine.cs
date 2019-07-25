using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using System.Linq;
using KariyerAnalytics.Data.Contract;

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

        public EndpointMetricsResponseDTO[] GetEndpointMetrics(Request request)
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

        public EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompany(UserRequest userRequest)
        {
            var result = _StatisticRepository.GetEndpointMetricsbyCompany(userRequest.CompanyName, userRequest.After, userRequest.Before);
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

        public EndpointMetricsResponseDTO[] GetEndpointMetricsbyUserandCompany(EndpointRequest endpointRequest)
        {
            var result = _StatisticRepository.GetEndpointMetricsbyUserandCompany(endpointRequest.CompanyName, endpointRequest.Username, endpointRequest.After, endpointRequest.Before);
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
