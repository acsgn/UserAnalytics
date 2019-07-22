using System;

namespace KariyerAnalytics.Service.Entities
{
    public class CompanyDetailRequest
    {
        public DateTime After { get; set; }
        public DateTime Before { get; set; }
        public string CompanyName { get; set; }
    }
}
