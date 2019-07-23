using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class StatisticController : ApiController
    {
        private readonly IStatisticEngine _StatisticEngine;

        public StatisticController(IStatisticEngine statisticEngine)
        {
            _StatisticEngine = statisticEngine;
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
