namespace KariyerAnalytics.Service.Entities
{
    public class EndpointAbsoluteMetricResponseDTO
    {
        public string Endpoint { get; set; }
        public long NumberOfRequests { get; set; }
        public double AverageResponseTime { get; set; }
    }
}
