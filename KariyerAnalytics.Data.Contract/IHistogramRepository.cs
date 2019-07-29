using System;
using KariyerAnalytics.Business.Entities;

namespace KariyerAnalytics.Data.Contract
{
    public interface IHistogramRepository
    {
        HistogramResponse[] GetResponseTimesHistogram(string endpoint, TimeSpan interval, DateTime after, DateTime before, );
    }
}
