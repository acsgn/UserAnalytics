using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IHistogramEngine : IEngine
    {
        HistogramResponseDTO[] GetResponseTimesHistogram(ResponseTimesHistogramRequest histogramRequest);
        HistogramResponseDTO[] GetResponseTimesHistogramByEndpoint(ResponseTimesHistogramRequest histogramRequest);
    }
}
