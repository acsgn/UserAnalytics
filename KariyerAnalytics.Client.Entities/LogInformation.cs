using System;

namespace KariyerAnalytics.Service.Entities
{
    public class LogInformation
    {
        public string CompanyName { get; set; } = "Login Screen";
        public string Username { get; set; }
        public string URL { get; set; }
        public string Endpoint { get; set; }
        public string IP { get; set; }
        public DateTime Timestamp { get; set; }
        public int ResponseTime { get; set; }
    }
}
