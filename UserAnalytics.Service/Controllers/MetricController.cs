using System.Web.Http;
using UserAnalytics.Business.Contract;
using UserAnalytics.Service.Entities;

namespace UserAnalytics.Controllers
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

        [HttpGet]
        public MetricsResponseDTO GetSingleMetric(MetricRequest request)
        {
            return _MetricEngine.GetSingleMetric(request);
        }
    }
}
