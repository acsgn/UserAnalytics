namespace KariyerAnalytics.Service.Entities
{
    public class MetricRequest : Request
    {
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string Endpoint { get; set; }
        public int? Size { get; set; }
        public bool? Ascending { get; set; }
    }
}
