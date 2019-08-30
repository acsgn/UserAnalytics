using System.Linq;
using UserAnalytics.Business.Contract;
using UserAnalytics.Data.Contract;
using UserAnalytics.Service.Entities;

namespace UserAnalytics.Business
{
    public class HistogramEngine : IHistogramEngine
    {
        private readonly IHistogramRepository _HistogramRepository;

        public HistogramEngine(IHistogramRepository histogramRepository)
        {
            _HistogramRepository = histogramRepository;
        }

        public HistogramResponseDTO[] GetResponseTimesHistogram(HistogramRequest histogramRequest)
        {
            var result = _HistogramRepository.GetResponseTimesHistogram(histogramRequest.Endpoint, histogramRequest.Interval, histogramRequest.After, histogramRequest.Before);
            return (from r in result
                    select new HistogramResponseDTO
                    {
                        Average = r.Average,
                        NumberOfRequests = r.NumberOfRequests,
                        Timestamp = r.Timestamp
                    }).ToArray();
        }
    }
}
