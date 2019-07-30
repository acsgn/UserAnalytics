using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class MetricController : ApiController
    {
        private readonly IMetricEngine _MetricEngine;

        public MetricController(IMetricEngine metricEngine)
        {
            _MetricEngine = metricEngine;
        }

        [HttpGet]
        public EndpointMetricsResponseDTO[] GetEndpointMetrics(MetricRequest request)
        {
            return _MetricEngine.GetEndpointMetrics(request);
        }

        [HttpGet]
        public CompanyMetricsResponseDTO[] GetCompanyMetrics(MetricRequest request)
        {
            return _MetricEngine.GetCompanyMetrics(request);
        }

        [HttpGet]
        public UserMetricsResponseDTO[] GetUserMetrics(MetricRequest request)
        {
            return _MetricEngine.GetUserMetrics(request);
        }
    }
}
