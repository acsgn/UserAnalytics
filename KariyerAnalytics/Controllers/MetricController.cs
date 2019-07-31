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
        public MetricsResponseDTO[] GetEndpointMetrics(MetricRequest request)
        {
            return _MetricEngine.GetEndpointMetrics(request);
        }

        [HttpGet]
        public MetricsResponseDTO[] GetCompanyMetrics(MetricRequest request)
        {
            return _MetricEngine.GetCompanyMetrics(request);
        }
    }
}
