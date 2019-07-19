using System.Web.Http;
using KariyerAnalytics.Business;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class StatisticController : ApiController
    {
        [HttpGet]
        public MetricResponse GetBestResponseTime(Request request)
        {
            var engine = new StatisticEngine();
            return engine.GetBestResponseTime(request);
        }

        [HttpGet]
        public MetricResponse GetWorstResponseTime(Request request)
        {
            var engine = new StatisticEngine();
            return engine.GetWorstResponseTime(request);
        }
        
        [HttpGet]
        public string[] GetEndpoints(Request request)
        {
            var engine = new StatisticEngine();
            return engine.GetEndpoints(request);
        }

        [HttpGet]
        public int[] GetResponseTimes(ResponseTimeRequest responseTimeRequest)
        {
            var engine = new StatisticEngine();
            return engine.GetResponseTimes(responseTimeRequest);
        }

    }
}
