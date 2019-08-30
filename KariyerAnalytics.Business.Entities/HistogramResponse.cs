using System;

namespace UserAnalytics.Business.Entities
{
    public class HistogramResponse
    {
        public DateTime Timestamp { get; set; }
        public long NumberOfRequests { get; set; }
        public double Average { get; set; }
    }
}
