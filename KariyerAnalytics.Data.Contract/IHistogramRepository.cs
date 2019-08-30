using System;
using UserAnalytics.Business.Entities;

namespace UserAnalytics.Data.Contract
{
    public interface IHistogramRepository
    {
        HistogramResponse[] GetResponseTimesHistogram(string endpoint, TimeSpan interval, DateTime? after, DateTime? before);
    }
}
