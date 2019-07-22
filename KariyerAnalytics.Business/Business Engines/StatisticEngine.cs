using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;
using System.ComponentModel.Composition;
using KariyerAnalytics.Common.DependencyInjection;
using System.Linq;

namespace KariyerAnalytics.Business
{
    [Export(typeof(IStatisticEngine))]
    public class StatisticEngine : IStatisticEngine
    {
        [Import(typeof(StatisticRepository))]
        private StatisticRepository _StatisticRepository;

        public StatisticEngine()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
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

        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            return _StatisticRepository.GetResponseTimes(responseTimeRequest.Endpoint, responseTimeRequest.After, responseTimeRequest.Before);
        }
        
    }
}
