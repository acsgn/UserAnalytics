using System.ComponentModel.Composition;
using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Common.DependencyInjection;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class StatisticController : ApiController
    {
        [Import(typeof(IStatisticEngine))]
        private IStatisticEngine _StatisticEngine;

        public StatisticController()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }

        [HttpGet]
        public MetricResponseDTO GetBestResponseTime(Request request)
        {
            return _StatisticEngine.GetBestResponseTime(request);
        }

        [HttpGet]
        public MetricResponseDTO GetWorstResponseTime(Request request)
        {
            return _StatisticEngine.GetWorstResponseTime(request);
        }

        [HttpGet]
        public RealtimeUserMetricDTO[] GetRealtimeUsers(RealtimeUserCountRequest realtimeUserCountRequest)
        {
            return _StatisticEngine.GetRealtimeUsers(realtimeUserCountRequest.SecondsBefore);
        }

        [HttpGet]
        public string[] GetEndpoints(Request request)
        {
            return _StatisticEngine.GetEndpoints(request);
        }

        [HttpGet]
        public HistogramDTO[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            return _StatisticEngine.GetResponseTimes(responseTimeRequest);
        }

    }
}
