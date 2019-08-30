using System.Web.Http;
using UserAnalytics.Business.Contract;
using UserAnalytics.Service.Entities;

namespace UserAnalytics.Controllers
{
    public class HistogramController : ApiController
    {
        private readonly IHistogramEngine _HistogramEngine;

        public HistogramController(IHistogramEngine histogramEngine)
        {
            _HistogramEngine = histogramEngine;
        }

        [HttpGet]
        public HistogramResponseDTO[] GetResponseTimesHistogram(HistogramRequest histogramRequest)
        {
            return _HistogramEngine.GetResponseTimesHistogram(histogramRequest);
        }
    }
}
