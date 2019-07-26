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
        public HistogramResponseDTO[] GetResponseTimesHistogram(ResponseTimesHistogramRequest histogramRequest)
        {
            return _HistogramEngine.GetResponseTimesHistogram(histogramRequest);
        }

        [HttpGet]
        public HistogramResponseDTO[] GetResponseTimesHistogramByEndpoint(ResponseTimesHistogramRequest histogramRequest)
        {
            return _HistogramEngine.GetResponseTimesHistogramByEndpoint(histogramRequest);
        }

    }
}
