using System;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IHistogramRepository
    {
        HistogramResponse[] GetResponseTimesHistogram(TimeSpan interval, DateTime after, DateTime before);
        HistogramResponse[] GetResponseTimesHistogramByEndpoint(string endpoint, TimeSpan interval, DateTime after, DateTime before);
    }
}
