namespace KariyerAnalytics.Service.Entities
{
    public class MetricsResponseDTO
    {
        public string Key { get; set; }
        public long NumberOfRequests { get; set; }
        public double MinResponseTime { get; set; }
        public double AverageResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
    }
}
