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

        public RealtimeUserMetricDTO[] GetRealtimeUsers(RealtimeUserCountRequest realtimeUserCountRequest)
        {
            var result = _StatisticRepository.GetRealtimeUsers(realtimeUserCountRequest.SecondsBefore);
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
