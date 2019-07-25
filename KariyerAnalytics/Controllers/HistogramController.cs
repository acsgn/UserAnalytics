using System.Web.Http;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Controllers
{
    public class HistogramController : ApiController
    {
        private readonly IHistogramEngine _HistogramEngine;

        public HistogramController(IHistogramEngine histogramEngine)
        {
            _HistogramEngine = histogramEngine;
        }

        [HttpGet]
        public HistogramResponseDTO[] GetResponseTimesHistogram(ResponseTimesHistogramRequest responseTimeRequest)
        {
            return _HistogramEngine.GetResponseTimesHistogram(responseTimeRequest);
        }

        [HttpGet]
        public HistogramResponseDTO[] GetResponseTimesHistogramByEndpoint(EndpointResponseTimesHistogramRequest endpointHistogramRequest)
        {
            return _HistogramEngine.GetResponseTimesHistogramByEndpoint(endpointHistogramRequest);
        }

    }
}
