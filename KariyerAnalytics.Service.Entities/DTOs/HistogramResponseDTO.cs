using System;

namespace KariyerAnalytics.Service.Entities
{
    public class HistogramResponseDTO
    {
        public DateTime Timestamp { get; set; }
        public long NumberOfRequests { get; set; }
        public double Average { get; set; }
    }
}
