using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;
using System.Linq;

namespace KariyerAnalytics.Business
{
    public class StatisticEngine : IStatisticEngine
    {
        private StatisticRepository _StatisticRepository;

        public StatisticEngine(StatisticRepository statisticRepository)
        {
            _StatisticRepository = statisticRepository;
        }

        public MetricResponseDTO GetBestResponseTime(Request request)
        {
            var result = _StatisticRepository.GetBestResponseTime(request.After, request.Before);
            return new MetricResponseDTO
            {
                Endpoint = result.Endpoint,
                ResponseTime = result.ResponseTime
            };
        }

        public MetricResponseDTO GetWorstResponseTime(Request request)
        {
            var result = _StatisticRepository.GetWorstResponseTime(request.After, request.Before);
            return new MetricResponseDTO
            {
                Endpoint = result.Endpoint,
                ResponseTime = result.ResponseTime
            };
        }

        public RealtimeUserMetricDTO[] GetRealtimeUsers(int secondsBefore)
        {
            var result = _StatisticRepository.GetRealtimeUsers(secondsBefore);
            return (from r in result
                    select new RealtimeUserMetricDTO
                    {
                        Endpoint = r.Endpoint,
                        UserCount = r.UserCount
                    }).ToArray();
        }

        public string[] GetEndpoints(Request request)
        {
            return _StatisticRepository.GetEndpoints(request.After, request.Before);
        }

        public HistogramDTO[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            var result = _StatisticRepository.GetResponseTimes(responseTimeRequest.Endpoint, responseTimeRequest.Interval, responseTimeRequest.After, responseTimeRequest.Before);
            return (from r in result
                    select new HistogramDTO
                    {
                        Average = r.Average,
                        Timestamp = r.Timestamp
                    }).ToArray();
        }
        
    }
}
