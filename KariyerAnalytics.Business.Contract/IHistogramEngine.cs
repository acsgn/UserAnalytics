using UserAnalytics.Service.Entities;

namespace UserAnalytics.Business.Contract
{
    public interface IHistogramEngine : IEngine
    {
        HistogramResponseDTO[] GetResponseTimesHistogram(HistogramRequest histogramRequest);
    }
}
