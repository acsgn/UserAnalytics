using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IHistogramEngine
    {
        HistogramResponseDTO[] GetResponseTimesHistogram(ResponseTimesHistogramRequest histogramRequest);
        HistogramResponseDTO[] GetResponseTimesHistogramByEndpoint(EndpointResponseTimesHistogramRequest endpointHistogramRequest);
    }
}
