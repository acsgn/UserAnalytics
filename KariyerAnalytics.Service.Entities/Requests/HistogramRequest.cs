using System;

namespace KariyerAnalytics.Service.Entities
{
    public class HistogramRequest : Request
    {
        public TimeSpan Interval { get; set; } = new TimeSpan(1, 0, 0, 0);
        public string Endpoint { get; set; }
    }
}
