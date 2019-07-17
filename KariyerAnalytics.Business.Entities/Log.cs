using System;

namespace KariyerAnalytics.Business.Entities
{
    public class Log
    {
        public string CompanyName { get; set; }
        public string Username { get; set; }
        public string URL { get; set; }
        public string Endpoint { get; set; }
        public string IP { get; set; }
        public DateTime Timestamp { get; set; }
        public int ResponseTime { get; set; }
    }
}