using System;

namespace KariyerAnalytics.Service.Entities
{
    public class HistogramRequest : Request
    {
        public TimeSpan Interval { get; set; }
        public string Endpoint { get; set; }
    }
}
