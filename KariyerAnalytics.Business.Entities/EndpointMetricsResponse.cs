namespace KariyerAnalytics.Business.Entities
{
    public class EndpointMetricsResponse
    {
        public string Endpoint { get; set; }
        public long NumberOfRequests { get; set; }
        public double MinResponseTime { get; set; }
        public double AverageResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
    }
}
