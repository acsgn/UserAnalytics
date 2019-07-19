using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerAnalytics.Service.Entities;

namespace KariyerAnalytics.Business.Contract
{
    public interface IStatisticEngine
    {
        MetricResponse GetBestResponseTime(Request request);
        MetricResponse GetWorstResponseTime(Request request);
        string[] GetEndpoints(Request request);
        int[] GetResponseTimes(string endpoint, Request request);
    }
}
