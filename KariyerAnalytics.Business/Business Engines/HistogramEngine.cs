using System.Linq;
using KariyerAnalytics.Business.Contract;
using KariyerAnalytics.Data.Contract;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business
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
            var result = _HistogramRepository.GetResponseTimesHistogram(histogramRequest.Interval, histogramRequest.After, histogramRequest.Before);
            return (from r in result
                    select new HistogramResponseDTO
                    {
                        Average = r.Average,
                        NumberOfRequests = r.NumberOfRequests,
                        Timestamp = r.Timestamp
                    }).ToArray();
        }

        public HistogramResponseDTO[] GetResponseTimesHistogramByEndpoint(HistogramRequest histogramRequest)
        {
            var result = _HistogramRepository.GetResponseTimesHistogram(histogramRequest.Interval, histogramRequest.After, histogramRequest.Before, histogramRequest.Endpoint);
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
