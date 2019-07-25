using System;

namespace KariyerAnalytics.Service.Entities
{
    public class ResponseTimeHistogramRequest : Request
    {
        public string Endpoint { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
