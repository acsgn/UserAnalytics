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
        public EndpointAbsoluteMetricsResponseDTO GetBestResponseTime(Request request)
        {
            return _StatisticEngine.GetBestResponseTime(request);
        }

        [HttpGet]
        public EndpointAbsoluteMetricsResponseDTO GetWorstResponseTime(Request request)
        {
            return _StatisticEngine.GetWorstResponseTime(request);
        }

        [HttpGet]
        public EndpointMetricsResponseDTO[] GetEndpointMetrics(Request request)
        {
            return _StatisticEngine.GetEndpointMetrics(request);
        }

        [HttpGet]
        public EndpointMetricsResponseDTO[] GetEndpointMetricsbyCompany(UserRequest userRequest)
        {
            return _StatisticEngine.GetEndpointMetricsbyCompany(userRequest);
        }

        [HttpGet]
        public EndpointMetricsResponseDTO[] GetEndpointMetricsbyUserandCompany(EndpointRequest endpointRequest)
        {
            return _StatisticEngine.GetEndpointMetricsbyUserandCompany(endpointRequest);
        }
    }
}
