using System;

namespace KariyerAnalytics.Service.Entities
{
    public class ResponseTimeRequest
    {
        public DateTime After { get; set; }
        public DateTime Before { get; set; }
        public string Endpoint { get; set; }
    }
}
