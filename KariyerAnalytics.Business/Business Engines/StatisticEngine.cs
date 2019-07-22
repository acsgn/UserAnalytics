using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;
using KariyerAnalytics.Data.Repositories;
using System.ComponentModel.Composition;
using KariyerAnalytics.Common.DependencyInjection;

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

        public MetricResponse GetBestResponseTime(Request request)
        {
            var response = _StatisticRepository.GetBestResponseTime(request.After, request.Before);
            return new MetricResponse
            {
                Endpoint = response.Endpoint,
                ResponseTime = response.ResponseTime
            };
        }

        public MetricResponse GetWorstResponseTime(Request request)
        {
            var response = _StatisticRepository.GetWorstResponseTime(request.After, request.Before);
            return new MetricResponse
            {
                Endpoint = response.Endpoint,
                ResponseTime = response.ResponseTime
            };
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
